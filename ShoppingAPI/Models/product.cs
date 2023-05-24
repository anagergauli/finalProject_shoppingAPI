#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class product
    {
        public product()
        {
            Payments = new HashSet<Payment>();
            carts = new HashSet<cart>();
        }

        public int productID { get; set; }
        public int? categoryID { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public int? count { get; set; }
        public string description { get; set; }
        public int? userID { get; set; }

        public virtual category category { get; set; }
        public virtual userAccount user { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<cart> carts { get; set; }
    }
}