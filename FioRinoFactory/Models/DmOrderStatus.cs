using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmOrderStatus
    {
        public DmOrderStatus()
        {
            DmOrderArchievums = new HashSet<DmOrderArchievum>();
            DmWzMagazyns = new HashSet<DmWzMagazyn>();
        }

        public int Id { get; set; }
        public string OrderStatusName { get; set; }

        public virtual ICollection<DmOrderArchievum> DmOrderArchievums { get; set; }
        public virtual ICollection<DmWzMagazyn> DmWzMagazyns { get; set; }
    }
}
