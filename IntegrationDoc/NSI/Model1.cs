namespace IntegrationDoc
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model12")
        {
        }

        public virtual DbSet<doc_typeBaiterek> doc_typeBaiterek { get; set; }
        public virtual DbSet<DocTypeMap> DocTypeMap { get; set; }
        public virtual DbSet<nsi_actions> nsi_actions { get; set; }
        public virtual DbSet<nsi_applicant> nsi_applicant { get; set; }
        public virtual DbSet<nsi_characters> nsi_characters { get; set; }
        public virtual DbSet<nsi_control_type> nsi_control_type { get; set; }
        public virtual DbSet<nsi_doc_type> nsi_doc_type { get; set; }
        public virtual DbSet<nsi_exres_type> nsi_exres_type { get; set; }
        public virtual DbSet<nsi_org> nsi_org { get; set; }
        public virtual DbSet<nsi_org_types> nsi_org_types { get; set; }
        public virtual DbSet<nsi_pos_type> nsi_pos_type { get; set; }
        public virtual DbSet<nsi_reasons> nsi_reasons { get; set; }
        public virtual DbSet<nsi_regions> nsi_regions { get; set; }
        public virtual DbSet<OrgTypeMap> OrgTypeMap { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
