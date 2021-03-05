namespace IntegrationDoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dictionaries.OrgTypeMap")]
    public partial class OrgTypeMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member { get; set; }

        public int Code { get; set; }
    }
}
