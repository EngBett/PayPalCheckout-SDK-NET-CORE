using System;
using System.Collections.Generic;
using System.Linq;

namespace PayPalCheckout.Models.Response
{
    public class PayPalCreateOrderResponse
    {
        public string id { get; set; } 
        public GrossTotalAmount gross_total_amount { get; set; } 
        public List<PurchaseUnit> purchase_units { get; set; } 
        public RedirectUrls redirect_urls { get; set; } 
        public DateTime create_time { get; set; } 
        public List<Link> links { get; set; } 
        public string status { get; set; }

        public string PaymentId() => id;
        public string ApprovalLink()=>links.FirstOrDefault(l => l.rel == "approval_url")?.href;
    }
   public class GrossTotalAmount    {
        public string value { get; set; } 
        public string currency { get; set; } 
    }

    public class Details    {
        public string subtotal { get; set; } 
        public string shipping { get; set; } 
        public string tax { get; set; } 
    }

    public class Amount    {
        public string currency { get; set; } 
        public Details details { get; set; } 
        public string total { get; set; } 
        public string value { get; set; } 
    }

    public class Payee    {
        public string email { get; set; } 
    }

    public class Item    {
        public string name { get; set; } 
        public string sku { get; set; } 
        public string price { get; set; } 
        public string currency { get; set; } 
        public int quantity { get; set; } 
    }

    public class ShippingAddress    {
        public string recipient_name { get; set; } 
        public bool default_address { get; set; } 
        public bool preferred_address { get; set; } 
        public bool primary_address { get; set; } 
        public bool disable_for_transaction { get; set; } 
        public string line1 { get; set; } 
        public string line2 { get; set; } 
        public string city { get; set; } 
        public string country_code { get; set; } 
        public string postal_code { get; set; } 
        public string state { get; set; } 
        public string phone { get; set; } 
    }

    public class Receiver    {
        public string email { get; set; } 
    }

    public class PartnerFeeDetails    {
        public Receiver receiver { get; set; } 
        public Amount amount { get; set; } 
    }

    public class PurchaseUnit    {
        public string reference_id { get; set; } 
        public string description { get; set; } 
        public Amount amount { get; set; } 
        public Payee payee { get; set; } 
        public List<Item> items { get; set; } 
        public ShippingAddress shipping_address { get; set; } 
        public string shipping_method { get; set; } 
        public PartnerFeeDetails partner_fee_details { get; set; } 
        public int payment_linked_group { get; set; } 
        public string custom { get; set; } 
        public string invoice_number { get; set; } 
        public string payment_descriptor { get; set; } 
        public string status { get; set; } 
    }

    public class RedirectUrls{
        public string return_url { get; set; } 
        public string cancel_url { get; set; } 
    }

    public class Link    {
        public string href { get; set; } 
        public string rel { get; set; } 
        public string method { get; set; } 
    }
}