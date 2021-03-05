using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegrationDoc
{
    [Serializable]
    [XmlRoot(ElementName ="sbapi")]
    public class Sbapi
    {
        [XmlElement]
        public Header header { get; set; }
        [XmlElement]
        public Body body { get; set; }
    }
    public class Body
    {
        [XmlElement(elementName:"object")]
        public Object_ object_ { get; set; }
    }
    public class Object_
    {
        [XmlAttribute]
        public string text { get; set; }
        [XmlAttribute]
        public string id { get; set; }
        //[XmlAttribute]
        //public string state { get; set; }
    }
    public class Header
    {
        [XmlElement(elementName: "interface")]
        public Interface Interface_ { get; set; }
        [XmlElement]
        public Message message { get; set; }
        [XmlElement]
        public Error error { get; set; }
    }
    public class Error
    {
        [XmlAttribute]
        public string id { get; set; }
        [XmlAttribute]
        public string text { get; set; }
    }
    public class Interface
    {
        [XmlAttribute]
        public string id { get; set; }
        [XmlAttribute]
        public string version { get; set; }
    }
    public class Message
    {
        [XmlAttribute]
        public string id { get; set; }
        [XmlAttribute]
        public string ignore_id { get; set; }
        [XmlAttribute]
        public string type { get; set; }
        [XmlAttribute]
        public string created { get; set; }
        [XmlAttribute]
        public string parent { get; set; }
    }
}
