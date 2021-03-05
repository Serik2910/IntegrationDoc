namespace IntegrationDoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dictionaries.nsi_actions")]
    public partial class nsi_actions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(10)]
        public string code { get; set; }

        public bool? is_marked_to_delete { get; set; }

        [StringLength(200)]
        public string name_kz { get; set; }

        [StringLength(200)]
        public string name_ru { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? update_date { get; set; }

        public int? guid { get; set; }
    }
}
