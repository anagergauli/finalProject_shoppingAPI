#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class delivery
    {
        public int deliveryID { get; set; }
        public int? userID { get; set; }
        public DateTime? date { get; set; }

        public virtual userAccount user { get; set; }
    }
}