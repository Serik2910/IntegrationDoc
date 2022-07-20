using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace IntegrationDoc.NSI
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<nsi_org>()
                .Property(e => e.qg)
                .IsFixedLength();
        }
    }
}
