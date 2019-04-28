using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.Wish.Model
{
    public class SyncWishOrdersReturn
    {
        public SyncWishOrdersObjectReturn Order { get; set; }
    }

    public class SyncWishOrdersObjectReturn
    {
        public bool is_fbw { get; set; }
        public DateTime last_updated { get; set; }
        public DateTime order_time { get; set; }
        public string order_id { get; set; }
        public decimal price { get; set; }
        public string variant_id { get; set; }
        public string[] fine_ids { get; set; }
        public DateTime date_shipping_carrier_confirmed_delivery { get; set; }
        public string wish_express_tier { get; set; }
        public SyncWishOrdersShippingDetailRetuurn ShippingDetail { get; set; }
        public DateTime shipped_date { get; set; }
        public DateTime released_to_merchant_time { get; set; }
        public decimal? shipping_cost { get; set; }
        public bool is_wish_express { get; set; }
        public bool requires_delivery_confirmation { get; set; }
        public string product_image_url { get; set; }
        public string size { get; set; }
        public string sku { get; set; }
        public bool is_wish_express_late_arrival { get; set; }
        public string shipping_provider { get; set; }
        public decimal order_total { get; set; }
        public string product_id { get; set; }
        public DateTime we_required_delivery_date { get; set; }
        public bool tracking_confirmed { get; set; }
        public string state { get; set; }
        public decimal shipping { get; set; }
        public string tracking_number { get; set; }
        public decimal cost { get; set; }
        public string confirmed_delivery { get; set; }
        public bool is_combined_order { get; set; }
        public DateTime tracking_confirmed_date { get; set; }
        public string product_name { get; set; }
        public string transaction_id { get; set; }
        public int quantity { get; set; }
    }

    public class SyncWishOrdersShippingDetailRetuurn
    {
        public string phone_number { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string street_address1 { get; set; }
        public string street_address2 { get; set; }
    }
}
