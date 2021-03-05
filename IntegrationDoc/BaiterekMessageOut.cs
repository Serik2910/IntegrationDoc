using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc
{
    public class BaiterekMessageOut
    {
        public string o_uuid { get; set; }
        public string o_reg { get; set; }
        public string o_doc_author { get; set; }
        public string o_doc_page_num { get; set; }
        public string o_in_amount { get; set; }
        public bool o_id_portal_spicified { get; set; }
        public bool o_docol_specified { get; set; }
        public bool o_useruin_specified { get; set; }
        public bool o_docNoR_specified { get; set; }
        public string o_id_portal { get; set; }
        public long? o_delivery_org_esedo { get; set; }
        public string o_useruin { get; set; }
        public string o_doc_NoR { get; set; }
        public string o_description { get; set; }
        public string o_doc_date { get; set; }
        public long o_doc_type { get; set; }
        public long o_message_type { get; set; }
        public string o_doc_rec_post { get; set; }
        public string o_doc_to_number { get; set; }
        public string o_document_receiver { get; set; }
        public string o_execution_date { get; set; }
        public string o_employee_phone { get; set; }
        public string o_exec { get; set; }
        public string o_date_event { get; set; }
        public string o_resolution_text { get; set; }
        public string o_sign_object_2 { get; set; }

        public Files[] files { get; set; }
        public class Files
        {
            public string name { get; set; }
            public string data { get; set; }
        }
    }
    public class BaiterekMessageOutNew
    {
        public string o_uuid { get; set; }
        public string o_reg { get; set; }
        public string o_doc_author { get; set; }
        public string o_doc_page_num { get; set; }
        public string o_in_amount { get; set; }

        public long? o_delivery_org_esedo { get; set; }

        public string o_description { get; set; }
        public string o_doc_date { get; set; }
        public long o_doc_type { get; set; }
        public long o_message_type { get; set; }
        public string o_doc_rec_post { get; set; }
        public string o_doc_to_number { get; set; }
        public string o_document_receiver { get; set; }
        public string o_execution_date { get; set; }
        public string o_employee_phone { get; set; }
        public string o_exec { get; set; }
        public string o_date_event { get; set; }
        public string o_resolution_text { get; set; }
        public string o_sign_object_2 { get; set; }

        public Files[] files { get; set; }
        public class Files
        {
            public string id { get; set; }
        }
        public bool o_id_portal_specified { get; set; }
        public bool o_docol_specified { get; set; }
        public bool o_useruin_specified { get; set; }
        public bool o_docNoR_specified { get; set; }
        public string o_useruin { get; set; }
        public string o_doc_NoR { get; set; }
        public string o_id_portal { get; set; }
    }

}