using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc
{
    public class BaiterekMessageFiles
    {
        public string id { get; set; }
        public Files[] files { get; set; }
        public class Files
        {
            public string name { get; set; }
            public string data { get; set; }
        }
        public bool redirection_specified { get; set; }
    }
    public class BaiterekMessageFilesRequest
    {
        public string id { get; set; }
        public Files[] files { get; set; }
        public class Files
        {
            public string id { get; set; }
        }
    }
    public class BaiterekMessageResponse
    {
        public string error { get; set; }
        public string o_unid { get; set; }
        public attach[] file_id { get; set; }
        public bool redirection_specified { get; set; }
    }
    public class attach
    {
        private string fileIdentifierField;
        public string fileId
        {
            get
            {
                return this.fileIdentifierField;
            }
            set
            {
                this.fileIdentifierField = value;
            }
        }

    }
}