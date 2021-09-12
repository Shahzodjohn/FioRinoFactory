using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class DmPosition
    {
        public DmPosition()
        {
            DmUsers = new HashSet<DmUser>();
        }

        public int Id { get; set; }
        public string PositionName { get; set; }

        public virtual ICollection<DmUser> DmUsers { get; set; }
    }
}
