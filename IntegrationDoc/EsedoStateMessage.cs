using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc
{
    public class EsedoStateMessage
    {
        public string o_date_start { get; set; }
        public string o_reg_num { get; set; }

        public int o_correspondent { get; set; }
        public string o_userUin { get; set; }
        public bool o_userUin_specified { get; set; }
        public bool o_docOL_specified { get; set; }
        public string o_id_portal { get; set; }
        public bool o_id_portal_specified { get; set; }

        public string o_unid { get; set; } //id ESEDO
        public string o_cms_base64 { get; set; }
        public string o_com { get; set; } // причина отклонения
        public string o_date_end { get; set; }
        public string o_phone { get; set; }
        public string o_performer { get; set; }
        public string o_signatory { get; set; }
        public string o_real_date { get; set; }
        public string o_result_code { get; set; }
        public string o_comment { get; set; }
    }
}