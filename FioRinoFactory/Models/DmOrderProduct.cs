using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmOrderProduct
    {
        public DmOrderProduct()
        {
            DmWzMagazyns = new HashSet<DmWzMagazyn>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public int? SizesId { get; set; }
        public int? ProductStatusId { get; set; }
        public int? SenderId { get; set; }
        public int? RecieverId { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FileWzId { get; set; }

        public virtual DmFileWz FileWz { get; set; }
        public virtual DmOrder Order { get; set; }
        public virtual DmProduct Product { get; set; }
        public virtual DmProductStatus ProductStatus { get; set; }
        public virtual DmUser Reciever { get; set; }
        public virtual DmUser Sender { get; set; }
        public virtual DmSize Sizes { get; set; }
        public virtual ICollection<DmWzMagazyn> DmWzMagazyns { get; set; }
    }
}
