#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class cart
    {
        public cart()
        {
            orders = new HashSet<order>();
        }

        public int cartID { get; set; }
        public int? productID { get; set; }
        public int? userID { get; set; }
        public int? Count { get; set; }

        public virtual product product { get; set; }
        public virtual userAccount user { get; set; }
        public virtual ICollection<order> orders { get; set; }
    }
}