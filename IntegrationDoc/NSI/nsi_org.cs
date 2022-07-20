namespace IntegrationDoc.NSI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dictionaries.nsi_org")]
    public partial class nsi_org : IHasGuid
    {
        public int id { get; set; }

        [StringLength(150)]
        public string abbr_kz { get; set; }

        [StringLength(150)]
        public string abbr_ru { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member { get; set; }

        [StringLength(500)]
        public string name_kz { get; set; }

        [StringLength(500)]
        public string name_ru { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? update_date { get; set; }

        public bool? is_marked_to_delete { get; set; }

        [StringLength(50)]
        public string parent { get; set; }

        [StringLength(10)]
        public string is_esedo { get; set; }

        public int? childs_count { get; set; }

        [StringLength(10)]
        public string is_ready { get; set; }

        public int? guid { get; set; }

        [StringLength(50)]
        public string qg { get; set; }
    }
}
