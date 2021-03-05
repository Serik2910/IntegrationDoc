namespace IntegrationDoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dictionaries.doc_typeBaiterek")]
    public partial class doc_typeBaiterek
    {
        public int ID { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public int? Code { get; set; }
    }
}
