using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Encode.API.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class CustomerContext : DbContext
    {
        public CustomerContext() : base("CustomerContext")
        {
            Database.SetInitializer(new CustomerInitializater());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            //Code First Mapping to Insert / Update / Delete Stored Procedures
            modelBuilder.Entity<Customer>().MapToStoredProcedures();
            modelBuilder.Entity<CustomerContact>().MapToStoredProcedures();

        }
    }
}