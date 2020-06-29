﻿using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Models
{
    public partial class Location
    {
        public Location()
        {
            Inventory = new HashSet<Inventory>();
            OrderHistory = new HashSet<OrderHistory>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
