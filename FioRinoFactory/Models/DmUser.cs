using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmUser
    {
        public DmUser()
        {
            DmFileWzs = new HashSet<DmFileWz>();
            DmOrderArchievumRecievers = new HashSet<DmOrderArchievum>();
            DmOrderArchievumSenders = new HashSet<DmOrderArchievum>();
            DmOrderProductRecievers = new HashSet<DmOrderProduct>();
            DmOrderProductSenders = new HashSet<DmOrderProduct>();
            DmOrderRecievers = new HashSet<DmOrder>();
            DmOrderSenders = new HashSet<DmOrder>();
            DmWzMagazynRecievers = new HashSet<DmWzMagazyn>();
            DmWzMagazynSenders = new HashSet<DmWzMagazyn>();
            InversePosition = new HashSet<DmUser>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int? RoleId { get; set; }
        public int? PositionId { get; set; }

        public virtual DmUser Position { get; set; }
        public virtual DmRole Role { get; set; }
        public virtual ICollection<DmFileWz> DmFileWzs { get; set; }
        public virtual ICollection<DmOrderArchievum> DmOrderArchievumRecievers { get; set; }
        public virtual ICollection<DmOrderArchievum> DmOrderArchievumSenders { get; set; }
        public virtual ICollection<DmOrderProduct> DmOrderProductRecievers { get; set; }
        public virtual ICollection<DmOrderProduct> DmOrderProductSenders { get; set; }
        public virtual ICollection<DmOrder> DmOrderRecievers { get; set; }
        public virtual ICollection<DmOrder> DmOrderSenders { get; set; }
        public virtual ICollection<DmWzMagazyn> DmWzMagazynRecievers { get; set; }
        public virtual ICollection<DmWzMagazyn> DmWzMagazynSenders { get; set; }
        public virtual ICollection<DmUser> InversePosition { get; set; }
    }
}
