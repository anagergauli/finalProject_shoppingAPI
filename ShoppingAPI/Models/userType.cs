#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class userType
    {
        public userType()
        {
            userAccounts = new HashSet<userAccount>();
        }

        public int typeID { get; set; }
        public string typeName { get; set; }
        public string description { get; set; }

        public virtual ICollection<userAccount> userAccounts { get; set; }
    }
}