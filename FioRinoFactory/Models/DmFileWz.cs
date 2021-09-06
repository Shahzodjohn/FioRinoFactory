using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmFileWz
    {
        public DmFileWz()
        {
            DmOrderArchievums = new HashSet<DmOrderArchievum>();
            DmOrderProducts = new HashSet<DmOrderProduct>();
            DmOrders = new HashSet<DmOrder>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public int FileType { get; set; }
        public decimal FileSize { get; set; }
        public int UserId { get; set; }

        public virtual DmUser User { get; set; }
        public virtual ICollection<DmOrderArchievum> DmOrderArchievums { get; set; }
        public virtual ICollection<DmOrderProduct> DmOrderProducts { get; set; }
        public virtual ICollection<DmOrder> DmOrders { get; set; }
    }
}
