using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmOrder
    {
        public DmOrder()
        {
            DmOrderProducts = new HashSet<DmOrderProduct>();
        }

        public int Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? OrderStatusId { get; set; }
        public int? OrderArchievumId { get; set; }
        public string SourceOfOrder { get; set; }
        public int? FileWzId { get; set; }
        public int? SizeId { get; set; }
        public bool? IsRemoved { get; set; }
        public DateTime? ImplementationDate { get; set; }
        public int SenderId { get; set; }
        public int? RecieverId { get; set; }
        public int? Amount { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }

        public virtual DmCategory Category { get; set; }
        public virtual DmFileWz FileWz { get; set; }
        public virtual DmOrderArchievum OrderArchievum { get; set; }
        public virtual DmUser Reciever { get; set; }
        public virtual DmUser Sender { get; set; }
        public virtual DmSize Size { get; set; }
        public virtual ICollection<DmOrderProduct> DmOrderProducts { get; set; }
    }
}
