namespace IntegrationDoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dictionaries.DocTypeMap")]
    public partial class DocTypeMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GUID { get; set; }

        public int Code { get; set; }
    }
}
