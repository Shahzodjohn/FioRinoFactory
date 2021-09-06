using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmWzMagazyn
    {
        public int Id { get; set; }
        public int FileWzId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrderStatusesId { get; set; }
        public int ProductId { get; set; }
        public bool IsRemoved { get; set; }
        public int Amount { get; set; }
        public int? OrderProductId { get; set; }
        public int? SenderId { get; set; }
        public int? RecieverId { get; set; }

        public virtual DmOrderProduct OrderProduct { get; set; }
        public virtual DmOrderStatus OrderStatuses { get; set; }
        public virtual DmProduct Product { get; set; }
        public virtual DmUser Reciever { get; set; }
        public virtual DmUser Sender { get; set; }
    }
}
