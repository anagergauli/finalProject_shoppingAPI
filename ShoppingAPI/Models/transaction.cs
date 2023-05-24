#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class transaction
    {
        public int transactionID { get; set; }
        public string transactionType { get; set; }
        public string description { get; set; }
        public int? userID { get; set; }
        public DateTime? date { get; set; }

        public virtual userAccount user { get; set; }
    }
}