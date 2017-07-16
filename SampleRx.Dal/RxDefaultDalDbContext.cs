using System.Data.Entity;

namespace SampleRx.Dal
{
    public class RxDefaultDalDbContext : DbContext
    {
        public RxDefaultDalDbContext() :
            base("name=RxDemoDb")
        {
            Configuration.ProxyCreationEnabled = false;
        }
        public virtual DbSet<RxCompany> RxCompanies { get; set; }
        public virtual DbSet<RxCompanyLogin> RxCompanyLogins { get; set; }
        public virtual DbSet<RxDataEntry> RxDataEntries { get; set; }
        public virtual DbSet<RxDrug> RxDrugs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RxCompany>()
                .Property(e => e.RxCompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<RxCompany>()
                .Property(e => e.RxCompanyAccessKey)
                .IsUnicode(false);

            modelBuilder.Entity<RxCompany>()
                .HasMany(e => e.RxDataEntries)
                .WithRequired(e => e.RxCompany)
                .HasForeignKey(e => e.EntryCompanyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RxCompanyLogin>()
                .Property(e => e.RxLoginEmail)
                .IsUnicode(false);

            modelBuilder.Entity<RxCompanyLogin>()
                .Property(e => e.RxLoginHash)
                .IsFixedLength();

            modelBuilder.Entity<RxDrug>()
                .Property(e => e.RxDrugName)
                .IsUnicode(false);

            modelBuilder.Entity<RxDrug>()
                .HasMany(e => e.RxDataEntries)
                .WithRequired(e => e.RxDrug)
                .HasForeignKey(e => e.EntryDrugId)
                .WillCascadeOnDelete(false);
        }
    }
}