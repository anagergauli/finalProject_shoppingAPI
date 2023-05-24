#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class Payment
    {
        public int paymentID { get; set; }
        public int? productID { get; set; }
        public string quantity { get; set; }
        public int? amount { get; set; }
        public DateTime? date { get; set; }
        public int? userID { get; set; }

        public virtual product product { get; set; }
        public virtual userAccount user { get; set; }
    }
}