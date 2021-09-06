using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmCategory
    {
        public DmCategory()
        {
            DmOrders = new HashSet<DmOrder>();
            DmProducts = new HashSet<DmProduct>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<DmOrder> DmOrders { get; set; }
        public virtual ICollection<DmProduct> DmProducts { get; set; }
    }
}
