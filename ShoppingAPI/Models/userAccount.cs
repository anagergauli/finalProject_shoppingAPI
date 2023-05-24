#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class userAccount
    {
        public userAccount()
        {
            Payments = new HashSet<Payment>();
            carts = new HashSet<cart>();
            deliveries = new HashSet<delivery>();
            orders = new HashSet<order>();
            products = new HashSet<product>();
            transactions = new HashSet<transaction>();
        }

        public int userID { get; set; }
        public int? typeID { get; set; }
        public string name { get; set; }
        public int? age { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string contactNumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public virtual userType type { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<cart> carts { get; set; }
        public virtual ICollection<delivery> deliveries { get; set; }
        public virtual ICollection<order> orders { get; set; }
        public virtual ICollection<product> products { get; set; }
        public virtual ICollection<transaction> transactions { get; set; }
    }
}