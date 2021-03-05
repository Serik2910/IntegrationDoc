using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc
{
    public class Exceptions_ESEDO_Receive:Exception
    {
        public Exceptions_ESEDO_Receive(String message):base(message){}
    }
    public class Exceptions_ESEDO_Simbase : Exception
    {
        public Exceptions_ESEDO_Simbase(String message) : base(message) { }
    }
}