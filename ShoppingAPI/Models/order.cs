#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class order
    {
        public int orderID { get; set; }
        public int? userID { get; set; }
        public int? cartID { get; set; }
        public DateTime? date { get; set; }

        public virtual cart cart { get; set; }
        public virtual userAccount user { get; set; }
    }
}