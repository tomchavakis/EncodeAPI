using MySql.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Encode.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class EncodeContext : DbContext
    {
        public EncodeContext() : base("name=EncodeContext")
        {
            Database.SetInitializer(new EncodeInitializer());
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerContact> CustomerContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerContact>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerContact>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerContact>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
