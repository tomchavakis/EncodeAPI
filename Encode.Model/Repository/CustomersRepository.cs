using Encode.Models;
using Encode.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encode.Repository
{
    public class CustomersRepository
    {
        private readonly EncodeContext db;

        public CustomersRepository(EncodeContext dbContext)
        {
            db = dbContext;

            if (Settings.Default.DumpSQL)
                dbContext.Database.Log = m => log.Info(m);
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<List<Customer>> GetCustomers()
        {
            return await db.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await db.Customers.FindAsync(id);
        }


        public void CreateCustomer(Customer customer)
        {
            db.Customers.Add(customer);
        }

    }
}
