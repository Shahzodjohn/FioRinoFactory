using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmProduct
    {
        public DmProduct()
        {
            DmOrderProducts = new HashSet<DmOrderProduct>();
            DmWzMagazyns = new HashSet<DmWzMagazyn>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string Gtin { get; set; }
        public string Skunumber { get; set; }
        public int? CategoryId { get; set; }
        public int SizeId { get; set; }
        public int? ProductStatusesId { get; set; }

        public virtual DmCategory Category { get; set; }
        public virtual DmProductStatus ProductStatuses { get; set; }
        public virtual DmSize Size { get; set; }
        public virtual ICollection<DmOrderProduct> DmOrderProducts { get; set; }
        public virtual ICollection<DmWzMagazyn> DmWzMagazyns { get; set; }
    }
}
