using System.Collections.Generic;

namespace PayPalCheckout.Models.Payload
{
    public class PayPalCreateOrderModel    {
        public string intent { get; set; } 
        public ApplicationContext application_context { get; set; } 
        public List<PurchaseUnit> purchase_units { get; set; } 
        public RedirectUrls redirect_urls { get; set; } 
    }
    public class ApplicationContext    {
        public string locale { get; set; } 
        public string brand_name { get; set; } 
        public string landing_page { get; set; } 
        public string shipping_preference { get; set; } 
        public string user_action { get; set; } 
    }

    public class Details    {
        public string subtotal { get; set; } 
        public string shipping { get; set; } 
        public string tax { get; set; } 
    }

    public class Amount    {
        public string currency { get; set; } 
        public string total { get; set; } 
        public Details details { get; set; } 
    }

    public class Payee    {
        public string email { get; set; } 
    }

    public class Item    {
        public string currency { get; set; } 
        public string name { get; set; } 
        public string price { get; set; } 
        public string tax { get; set; } 
        public string quantity { get; set; } 
        public string sku { get; set; } 
    }

    public class ShippingAddress    {
        public string recipient_name { get; set; } 
        public string line1 { get; set; } 
        public string line2 { get; set; } 
        public string city { get; set; } 
        public string country_code { get; set; } 
        public string postal_code { get; set; } 
        public string state { get; set; } 
        public string phone { get; set; } 
    }

    public class PurchaseUnit    {
        public string reference_id { get; set; } 
        public string description { get; set; } 
        public Amount amount { get; set; } 
        public Payee payee { get; set; } 
        public List<Item> items { get; set; } 
        public ShippingAddress shipping_address { get; set; } 
        public string shipping_method { get; set; } 
        public string invoice_number { get; set; } 
        public string payment_descriptor { get; set; } 
    }

    public class RedirectUrls    {
        public string return_url { get; set; } 
        public string cancel_url { get; set; } 
    }
}