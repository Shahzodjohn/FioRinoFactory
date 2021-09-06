using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmSize
    {
        public DmSize()
        {
            DmOrderProducts = new HashSet<DmOrderProduct>();
            DmOrders = new HashSet<DmOrder>();
            DmProducts = new HashSet<DmProduct>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }

        public virtual ICollection<DmOrderProduct> DmOrderProducts { get; set; }
        public virtual ICollection<DmOrder> DmOrders { get; set; }
        public virtual ICollection<DmProduct> DmProducts { get; set; }
    }
}
