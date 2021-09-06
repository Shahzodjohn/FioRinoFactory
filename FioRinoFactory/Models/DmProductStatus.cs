using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmProductStatus
    {
        public DmProductStatus()
        {
            DmOrderProducts = new HashSet<DmOrderProduct>();
            DmProducts = new HashSet<DmProduct>();
        }

        public int Id { get; set; }
        public string StatusDescription { get; set; }
        public string StatusColor { get; set; }

        public virtual ICollection<DmOrderProduct> DmOrderProducts { get; set; }
        public virtual ICollection<DmProduct> DmProducts { get; set; }
    }
}
