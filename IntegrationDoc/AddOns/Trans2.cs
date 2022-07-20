using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace IntegrationDoc
{
    [XmlRoot(Namespace = "http://egov.bee.kz/eds/tempstorage/v2/", ElementName = "data")]
    [DataContract]
    public class Trans2
    {
        [XmlElement(Namespace = "http://egov.bee.kz/eds/tempstorage/v2/")]
        [DataMember]
        public TempStorageResponse tempStorageResponse { get; set; }
    }
}