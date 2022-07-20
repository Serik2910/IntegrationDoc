using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc.DocSender
{

    public partial class DocumentResponse
    {
        private qujatGatewayDelivered dataField_;
        [System.Xml.Serialization.XmlElementAttribute(Order = 1, ElementName = "data")]
        [System.Runtime.Serialization.DataMember]
        public qujatGatewayDelivered data_
        {
            get
            {
                return dataField_;
            }
            set
            {
                dataField_ = value;
                //dataField = dataField_;
            }
        }
    }
    /*public partial class DocumentRequest
    {
        private stateDelivered dataField_;
        [System.Xml.Serialization.XmlElementAttribute(Order = 3, ElementName = "data")]
        [System.Runtime.Serialization.DataMember]
        public stateDelivered data_
        {
            get
            {
                return dataField_;
            }
            set
            {
                dataField_ = value;
                //dataField = dataField_;
            }
        }


    }*/

}