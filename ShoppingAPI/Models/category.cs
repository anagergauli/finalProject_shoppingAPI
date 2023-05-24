#nullable disable
using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    public partial class category
    {
        public category()
        {
            products = new HashSet<product>();
        }

        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }

        public virtual ICollection<product> products { get; set; }
    }
}