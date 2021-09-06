using System;
using System.Collections.Generic;

#nullable disable

namespace FioRinoFactory.Models
{
    public partial class LinksIdent
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string ReferenceTable { get; set; }
        public string ReferenceField { get; set; }
        public string Ident { get; set; }
        public byte? ReferenceType { get; set; }
    }
}
