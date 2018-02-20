using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Encode.Models
{
    /// <summary>
    /// Initialize the Database with Sample Data
    /// </summary>
    public class EncodeInitializer : CreateDatabaseIfNotExists<EncodeContext>
    {
        public static void DatabaseInitialization()
        {
            using (EncodeContext db = new EncodeContext())
            {
                db.Database.Initialize(true);
            }
        }

        protected override void Seed(EncodeContext context)
        {
            AddData(context);
        }

        public void AddData(EncodeContext context)
        {
            IList<Customer> customers = new List<Customer>();

            customers.Add(new Customer { Title = "KLM", NumberOfEmployees = 8000 });
            customers.Add(new Customer { Title = "Swiss", NumberOfEmployees = 8000 });
            customers.Add(new Customer { Title = "Aegean", NumberOfEmployees = 3000 });
            customers.Add(new Customer { Title = "Ryanair", NumberOfEmployees = 4000 });
            customers.Add(new Customer { Title = "Easy Jet", NumberOfEmployees = 5000 });
            customers.Add(new Customer { Title = "Lufthansa", NumberOfEmployees = 7000 });
            customers.Add(new Customer { Title = "Emirates", NumberOfEmployees = 8000 });
            customers.Add(new Customer { Title = "British Airlines", NumberOfEmployees = 8000 });
            customers.Add(new Customer { Title = "Etihad", NumberOfEmployees = 7000 });
            customers.Add(new Customer { Title = "Luxair", NumberOfEmployees = 8000 });


            foreach (Customer customer in customers)
                context.Customers.Add(customer);

            IList<CustomerContact> customerContacts = new List<CustomerContact>();

            customerContacts.Add(new CustomerContact { FirstName = "Pieter", LastName = "Elbers", Email = "pieterElbers@gmail.com", Customer = customers[0] });
            customerContacts.Add(new CustomerContact { FirstName = "Thomas", LastName = "Klühr", Email = "ThomasKluhr@gmail.com", Customer = customers[1] });
            customerContacts.Add(new CustomerContact { FirstName = "Dimitrios", LastName = "Gerogiannis", Email = "dimitriosgerogiannis@gmail.com", Customer = customers[2] });
            customerContacts.Add(new CustomerContact { FirstName = "Michael", LastName = "O'Leary", Email = "michaeloleary@gmail.com", Customer = customers[3] });
            customerContacts.Add(new CustomerContact { FirstName = "Johan", LastName = "Lundgren", Email = "johanlundgren@gmail.com", Customer = customers[4] });
            customerContacts.Add(new CustomerContact { FirstName = "Carsten", LastName = "Spohr", Email = "carstenspohr@gmail.com", Customer = customers[5] });
            customerContacts.Add(new CustomerContact { FirstName = "Ahmed bin", LastName = "Saeed Al Maktoum", Email = "ahmed@gmail.com", Customer = customers[6] });
            customerContacts.Add(new CustomerContact { FirstName = "Alex", LastName = "Cruz", Email = "alexcruz@gmail.com", Customer = customers[7] });
            customerContacts.Add(new CustomerContact { FirstName = "Peter", LastName = "Baumgartner", Email = "peterbaumgartner@gmail.com", Customer = customers[8] });
            customerContacts.Add(new CustomerContact { FirstName = "Paul", LastName = "Helminger", Email = "paulhelminger@gmail.com", Customer = customers[9] });

            foreach (CustomerContact contact in customerContacts)
                context.CustomerContacts.Add(contact);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}