﻿using Encode.API.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Encode.API.Controllers
{
    /// <summary>
    /// Customer Class (GRUD) with API
    /// </summary>
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private CustomerContext db = new CustomerContext();

        /// <summary>
        /// Get All the Customer
        /// </summary>
        /// <returns>List of Customers</returns>
        // GET: api/Customers
        [Route("")]
        public IQueryable<Customer> GetCustomers()
        {
            log.Debug(string.Format("GetCustomers()"));

            return db.Customers;
        }

        /// <summary>
        /// Get a Specific Customer
        /// </summary>
        /// <param name="id">Id of the Customer</param>
        /// <returns>Customer - 200</returns>
        // GET: api/Customers/5
        [Route("{id}")]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            log.Debug(string.Format("GetCustomer({0})", JsonConvert.SerializeObject(id, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                log.Debug(string.Format("GetCustomer({0}):{1}", id, "NotFound"));
                return NotFound();
            }

            log.Debug(string.Format("GetCustomer()='{0}'", JsonConvert.SerializeObject(customer, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

            return Ok(customer);
        }

        /// <summary>
        /// Create a New Customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Created Response with the Specified Values - 201</returns>
        // POST: api/Customers
        [Route("")]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            log.Debug(string.Format("PostCustomer({0})", JsonConvert.SerializeObject(customer, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

            if (!ModelState.IsValid)
            {
                log.Debug(string.Format("PostCustomer()= BadRequest{0}", JsonConvert.SerializeObject(ModelState, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Debug(string.Format("PostCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return InternalServerError(ex);
            }

            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Update a specific Customer
        /// </summary>
        /// <param name="id">Id of the Customer</param>
        /// <param name="customer">Updated Customer</param>
        /// <returns>NoContent Status Code - 204</returns>
        // PUT: api/Customers/5
        [HttpPut]
        [HttpPatch]
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomer(int id, Customer customer)
        {
            log.Debug(string.Format("PutCustomer({0})", JsonConvert.SerializeObject(customer, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

            if (!ModelState.IsValid)
            {
                log.Debug(string.Format("PutCustomer()= BadRequest{0}", JsonConvert.SerializeObject(ModelState, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return BadRequest(ModelState);
            }

            if (!CustomerExists(id))
            {
                log.Debug(string.Format("PutCustomer()= NotFound{0}", JsonConvert.SerializeObject(id, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return NotFound();
            }

            //MySql - Entity Framework update all the rows of the table so I use T-SQL Commands for the updating
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                string query = string.Format("UPDATE {0} SET Title = '{1}', NumberOfEmployees={2} WHERE Id = {3}", "customers.customer", customer.Title, customer.NumberOfEmployees, id);
                cmd.CommandText = query;
                try
                {
                    cn.Open();
                    int numRowsUpdated = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    log.Debug(string.Format("PutCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                    return InternalServerError(ex);
                }

                log.Debug(string.Format("PutCustomer({0})", JsonConvert.SerializeObject("Modified", Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return StatusCode(HttpStatusCode.NoContent);
            }
        }


        /// <summary>
        /// Delete a Customer by ID
        /// </summary>
        /// <param name="id">Id Of the Customer</param>
        /// <returns>Ok Message</returns>
        // DELETE: api/Customers/5
        [Route("{id}")]
        [HttpDelete]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            log.Debug(string.Format("DeleteCustomer({0})", JsonConvert.SerializeObject(id, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            //MySql - Entity Framework update all the rows of the table so I use T-SQL Commands for the updating
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                string query = string.Format("DELETE FROM {0} WHERE Id = {1}", "customers.customer", id);
                cmd.CommandText = query;
                try
                {
                    cn.Open();
                    int numRowsUpdated = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    log.Debug(string.Format("DeleteCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                    return InternalServerError(ex);
                }
            }


            log.Debug(string.Format("DeleteCustomer({0})=", JsonConvert.SerializeObject(customer, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

            return Ok(customer);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.Id == id) > 0;
        }

    }
}
