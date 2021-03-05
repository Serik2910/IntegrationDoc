using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IntegrationDoc.HedReference
{

    public partial class RequestData
    {
        private Trans dataField_;
       
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1, ElementName ="data")]
        public Trans data_
        {
            get { return dataField_; }
            set { dataField_ = value; }
        }
    }
    public partial class ResponseData
    {
        private Trans2 dataField_;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0, ElementName = "data")]
        public Trans2 data_
        {
            get
            {
                return dataField_;
            }
            set
            {
                dataField_ = value;
                dataField = dataField_;
            }
        }
    }
}