using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IntegrationDoc
{
    public class Trans
    {
        [System.Xml.Serialization.XmlElementAttribute("tempStorageRequest", IsNullable = false)]
        public TempStorageRequest tempStorageRequest { get; set; }
    }

}