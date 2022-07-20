using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc
{
    public class BaiterekMessageIn
    {
        public string o_date_event { get; set; }
        public string o_outgoing_number { get; set; }
        public string o_user_corr { get; set; }
        public string o_phone { get; set; }
        public string o_signatory { get; set; }
        public string o_description { get; set; }
        public string o_language { get; set; }
        public long o_doc_kind { get; set; }
        public int o_correspondent { get; set; }
        public string o_date_end { get; set; }
        public string o_unid { get; set; }
        public Files[] files { get; set; }
        public string o_control_type_outer_code { get; set; }
        public object o_control_type_outer_name { get; set; }
        public string o_author_name { get; set; }
        public string o_doc_to_number { get; set; }
        public string o_out_time { get; set; }
        public string o_prepared_date { get; set; }
        public long o_character { get; set; }

        public string o_docSection { get; set; }
        public string o_docSectionId { get; set; }


        //public BaiterekMessage() { }

        public class Files
        {
            public string name { get; set; }
            public string data { get; set; }
        }
    }
}