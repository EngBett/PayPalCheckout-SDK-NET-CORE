using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayPalCheckout.Controllers;
using PayPalCheckout.Models.Payload;

namespace ConsoleTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter clienId:");
            var clientId = Console.ReadLine();

            Console.Write("Enter SecretKey:");
            var secret = Console.ReadLine();


            var model = new PayPalCreateOrderModel
            {
                intent = "SALE",
                purchase_units = new List<PurchaseUnit>
                    {
                        new PurchaseUnit
                        {
                            reference_id = Guid.NewGuid().ToString(),
                            description = "Test paypal",
                            amount = new Amount
                            {
                                currency = "USD",
                                total = $"10",
                                details = new Details
                                {
                                    subtotal = $"10",
                                    shipping = "0",
                                    tax = $"0"
                                }
                            },
                            items = new List<Item>
                            {
                                new Item
                                {
                                    currency = "USD",
                                    name = $"Test Item",
                                    price = $"10",
                                    tax = "0",
                                    quantity = "1"
                                }
                            },
                        }
                    },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = "",
                    return_url = ""
                }
            };


            var paypal = new PayPal(clientId,secret, "Sandbox");
            await paypal.CreatePayment(model);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
