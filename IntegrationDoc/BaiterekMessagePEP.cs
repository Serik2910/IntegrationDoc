using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc
{
    public class BaiterekMessagePEP
    {
        public string o_address { get; set; }
        public string o_correspondent { get; set; }
        public long o_character { get; set; }
        public string o_description { get; set; }
        public string o_date_out { get; set; }
        public string o_id_portal { get; set; }
        public string o_in_amount { get; set; }
        public string o_delivery_date { get; set; }
        public string o_doc_dateR { get; set; }
        public string o_doc_NoR { get; set; }
        public string o_control_type_outer_code { get; set; }
        public long o_doc_kind { get; set; }
        public object o_control_type_outer_name { get; set; }
        public string o_contact_phone { get; set; }
        public string o_email { get; set; }
        public string o_signatory { get; set; }
        public string o_subject { get; set; }
        public int o_type { get; set; }
        public int o_doc_lang { get; set; }
        public int o_doc_type { get; set; }
        public string o_doc_number { get; set; }
        public int o_country { get; set; }
        public int o_locality { get; set; }
        public string o_date_end { get; set; }
        public string o_useruin { get; set; }
        public string o_surname { get; set; }
        public string o_firstname { get; set; }
        public string o_middlename { get; set; }
        public string o_legal { get; set; }
        public string o_unid { get; set; }
        public string o_author_name { get; set; }
        public string o_canc_sign { get; set; }
        public string o_portal_sign { get; set; }
        public string o_prepared_date { get; set; }
        public string[] files { get; set; }
    }
    public class BaiterekMessagePEPOut
    {
        public string o_uuid { get; set; }
        public long? o_delivery_org_esedo { get; set; }
        public string o_address { get; set; }
        public string o_correspondent { get; set; }
        public long o_character { get; set; }
        public string o_description { get; set; }
        public string o_date_out { get; set; }
        public string o_id_portal { get; set; }
        public string o_in_amount { get; set; }
        public string o_delivery_date { get; set; }
        public string o_doc_dateR { get; set; }
        public string o_doc_NoR { get; set; }
        public string o_control_type_outer_code { get; set; }
        public long o_doc_kind { get; set; }
        public object o_control_type_outer_name { get; set; }
        public string o_contact_phone { get; set; }
        public string o_email { get; set; }
        public string o_signatory { get; set; }
        public string o_subject { get; set; }
        public int o_type { get; set; }
        public int o_doc_lang { get; set; }
        public int o_doc_type { get; set; }
        public string o_doc_number { get; set; }
        public int o_country { get; set; }
        public int o_locality { get; set; }
        public string o_date_end { get; set; }
        public string o_useruin { get; set; }
        public string o_surname { get; set; }
        public string o_firstname { get; set; }
        public string o_middlename { get; set; }
        public string o_legal { get; set; }
        public string o_unid { get; set; }
        public string o_author_name { get; set; }
        public string o_portal_sign { get; set; }
        public string o_canc_sign { get; set; }
        public string o_prepared_date { get; set; }
        public Files[] files { get; set; }
        public class Files
        {
            public string id { get; set; }
        }
        //public BaiterekMessage() { }

        //public class Files
        //{
        //    public string name { get; set; }
        //    public string data { get; set; }
        //}
    }
    public class BaiterekMessagePEPOutOld
    {

        public string o_uuid { get; set; }
        public long? o_delivery_org_esedo { get; set; }
        public string o_address { get; set; }
        public string o_correspondent { get; set; }
        public long o_character { get; set; }
        public string o_description { get; set; }
        public string o_date_out { get; set; }
        public string o_id_portal { get; set; }
        public string o_in_amount { get; set; }
        public string o_delivery_date { get; set; }
        public string o_doc_dateR { get; set; }
        public string o_doc_NoR { get; set; }
        public string o_control_type_outer_code { get; set; }
        public long o_doc_kind { get; set; }
        public object o_control_type_outer_name { get; set; }
        public string o_contact_phone { get; set; }
        public string o_email { get; set; }
        public string o_signatory { get; set; }
        public string o_subject { get; set; }
        public int o_type { get; set; }
        public int o_doc_lang { get; set; }
        public int o_doc_type { get; set; }
        public string o_doc_number { get; set; }
        public int o_country { get; set; }
        public int o_locality { get; set; }
        public string o_date_end { get; set; }
        public string o_useruin { get; set; }
        public string o_surname { get; set; }
        public string o_firstname { get; set; }
        public string o_middlename { get; set; }
        public string o_legal { get; set; }
        public string o_unid { get; set; }
        public string o_author_name { get; set; }
        public string o_portal_sign { get; set; }
        public string o_canc_sign { get; set; }
        public string o_prepared_date { get; set; }
        public Files[] files { get; set; }
        //public class Files
        //{
        //    public string id { get; set; }
        //}
        //public BaiterekMessage() { }

        public class Files
        {
            public string name { get; set; }
            public string data { get; set; }
        }
    }
}