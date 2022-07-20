using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationDoc
{
    class StringWriter_ : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
        public StringWriter_(StringBuilder builder) : base(builder) { }
    }
    
}
