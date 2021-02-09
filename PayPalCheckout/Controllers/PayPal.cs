using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PayPalCheckout.Models;
using PayPalCheckout.Models.Payload;
using PayPalCheckout.Models.Response;

namespace PayPalCheckout.Controllers
{
    public class PayPal
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _clientId;
        private readonly string _secret;
        private readonly string _mode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientFactory">IHttpClientFactory interface instance</param>
        /// <param name="clientId">PayPal Client Id</param>
        /// <param name="secret">PayPal Secret</param>
        /// <param name="mode">Environment type 'Sandbox' or 'Live' </param>
        public PayPal(IHttpClientFactory clientFactory, string clientId, string secret, string mode)
        {
            _clientFactory = clientFactory;
            _clientId = clientId;
            _secret = secret;
            _mode = mode;
        }

        public async Task<PayPalCreateOrderResponse> CreatePayment(PayPalCreateOrderModel orderModel)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
                var request = new HttpRequestMessage(HttpMethod.Post, _GetCheckoutEndpoint());

                request.Headers.Add("Accept", "application/json");

                var getToken = await _GetToken();

                if (getToken == null)
                {
                    return null;
                }
                
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", getToken.access_token);
                
                
                //pass in data specifying the encoding type and media type passed in
                request.Content = new StringContent(JsonSerializer.Serialize(orderModel), Encoding.UTF8, "application/json");

                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    var res = await JsonSerializer.DeserializeAsync<PayPalCreateOrderResponse>(responseStream);

                    return res;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///  Gets An Access token using the client id and secret
        /// </summary>
        /// <returns>AccessToken object</returns>
        private async Task<AccessToken> _GetToken()
        {
            try
            {
                var appKeySecret = $"{_clientId}:{_secret}";
                var data = Encoding.ASCII.GetBytes(appKeySecret);
                var auth = Convert.ToBase64String(data);

                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
                var request = new HttpRequestMessage(HttpMethod.Post, _GetAccessTokenEndpoint());

                request.Headers.Add("Accept", "application/json");
                request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);


                var formData = new KeyValuePair<string, string>("grant_type", "client_credentials");
                request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> {formData});

                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode) return null;
                
                await using var responseStream = await response.Content.ReadAsStreamAsync();

                var tokenData =await JsonSerializer.DeserializeAsync<AccessToken>(responseStream);

                return tokenData;

            }
            catch (Exception)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Gets the access token url depending on the environment mode
        /// </summary>
        /// <returns>URL</returns>
        private string _GetAccessTokenEndpoint() => _mode.ToLower() == "live"
            ? ""
            : "https://api-m.sandbox.paypal.com/v1/oauth2/token";

        /// <summary>
        /// Gets the create paypal order checkout url depending on the environment mode
        /// </summary>
        /// <returns>URL</returns>
        private string _GetCheckoutEndpoint() => _mode.ToLower() == "live"
            ? ""
            : "https://api-m.sandbox.paypal.com/v1/checkout/orders";
    }
}