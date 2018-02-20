using Encode.Models;
using Encode.Repository;
using Encode.API.Properties;
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;

namespace Encode.API.Controllers
{
    /// <summary>
    /// Customer Class (GRUD) with API
    /// </summary>
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get All the Customer
        /// </summary>
        /// <returns>List of Customers</returns>
        // GET: api/Customers
        [Route("")]
        public async Task<HttpResponseMessage> GetCustomers()
        {
            log.Debug(string.Format("GetCustomers()"));
            try
            {
                using (EncodeContext db = new EncodeContext())
                {
                    CustomersRepository customersRepository = new CustomersRepository(db);

                    List<Customer> list = await customersRepository.GetCustomers();

                    log.Debug(string.Format("GetCustomers()='{0}'", JsonConvert.SerializeObject(list.Count, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                    return Request.CreateResponse<List<Customer>>(HttpStatusCode.OK, list);
                }
            }
            catch (Exception ex)
            {
                log.Debug(string.Format("GetCustomers()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get a Specific Customer
        /// </summary>
        /// <param name="id">Id of the Customer</param>
        /// <returns>Customer - 200</returns>
        // GET: api/Customers/5
        [Route("{id}")]
        [ResponseType(typeof(Customer))]
        public async Task<HttpResponseMessage> GetCustomer(int id)
        {
            log.Debug(string.Format("GetCustomer({0})", JsonConvert.SerializeObject(id, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
            try
            {
                using (EncodeContext db = new EncodeContext())
                {
                    CustomersRepository customersRepository = new CustomersRepository(db);
                    Customer customer = await customersRepository.GetCustomerById(id);

                    if (customer == null)
                    {
                        log.Debug(string.Format("GetCustomer({0}):{1}", id, "NotFound"));
                        return this.Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Id");
                    }

                    log.Debug(string.Format("GetCustomer()='{0}'", JsonConvert.SerializeObject(customer, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

                    return Request.CreateResponse<Customer>(HttpStatusCode.OK, customer);
                }
            }
            catch (Exception ex)
            {
                log.Debug(string.Format("GetCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }


        }

        /// <summary>
        /// Create a New Customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Created Response with the Specified Values - 201</returns>
        // POST: api/Customers
        [Route("")]
        [ResponseType(typeof(Customer))]
        public async Task<HttpResponseMessage> PostCustomer(Customer customer)
        {
            log.Debug(string.Format("PostCustomer({0})", JsonConvert.SerializeObject(customer, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

            if (customer == null)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Customer Cannot be null");
            }

            if (!ModelState.IsValid)
            {
                log.Debug(string.Format("PostCustomer()= BadRequest{0}", JsonConvert.SerializeObject(ModelState, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                using (EncodeContext db = new EncodeContext())
                {
                    CustomersRepository customersRepository = new CustomersRepository(db);
                    customersRepository.CreateCustomer(customer);

                    await db.SaveChangesAsync();

                    if (customer != null)
                    {
                        return this.Request.CreateResponse<Customer>(HttpStatusCode.Created, customer);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug(string.Format("PostCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Data");
        }

        /// <summary>
        /// Update a specific Customer
        /// </summary>
        /// <param name="id">Id of the Customer</param>
        /// <param name="customer">Updated Customer</param>
        /// <returns>NoContent Status Code - 204</returns>
        // PUT: api/Customers/5
        [HttpPut]
        //[HttpPatch]
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<HttpResponseMessage> PutCustomer(int id, Customer customer)
        {
            log.Debug(string.Format("PutCustomer({0})", JsonConvert.SerializeObject(customer, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));

            if (customer == null)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Customer Cannot be null");
            }

            if (!ModelState.IsValid)
            {
                log.Debug(string.Format("PutCustomer()= BadRequest{0}", JsonConvert.SerializeObject(ModelState, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (!CustomerExists(id))
            {
                log.Debug(string.Format("PutCustomer()= NotFound{0}", JsonConvert.SerializeObject(id, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return this.Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Id");
            }
            try
            {

                using (EncodeContext db = new EncodeContext())
                {
                    if (Settings.Default.DumpSQL)
                        db.Database.Log = m => log.Info(m);

                    Customer item = await db.Customers.FindAsync(id);

                    if (item != null)
                    {
                        item.NumberOfEmployees = customer.NumberOfEmployees;
                        item.Title = customer.Title;

                        await db.SaveChangesAsync();
                        log.Debug(string.Format("PutCustomer({0})", JsonConvert.SerializeObject("Modified", Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                        return this.Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    return this.Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Id");
                }

                #region ' Faulted MySQL Provider '
                //MySql - Entity Framework update all the rows of the table so I use T-SQL Commands for the updating
                //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EncodeContext"].ConnectionString;
                //using (MySqlConnection cn = new MySqlConnection(connectionString))
                //{
                //    MySqlCommand cmd = new MySqlCommand();
                //    cmd.Connection = cn;
                //    string query = string.Format("UPDATE {0} SET Title = '{1}', NumberOfEmployees={2} WHERE Id = {3}", "customers.customer", customer.Title, customer.NumberOfEmployees, id);
                //    cmd.CommandText = query;
                //    try
                //    {
                //        cn.Open();
                //        int numRowsUpdated = cmd.ExecuteNonQuery();
                //        cmd.Dispose();
                //    }
                //    catch (MySql.Data.MySqlClient.MySqlException ex)
                //    {
                //        log.Debug(string.Format("PutCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                //        return InternalServerError(ex);
                //    }

                //    log.Debug(string.Format("PutCustomer({0})", JsonConvert.SerializeObject("Modified", Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                //    return StatusCode(HttpStatusCode.NoContent);
                //}
                #endregion

            }
            catch (Exception ex)
            {
                log.Debug(string.Format("PutCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
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
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            log.Debug(string.Format("DeleteCustomer({0})", JsonConvert.SerializeObject(id, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
            try
            {
                using (EncodeContext db = new EncodeContext())
                {
                    Customer customer = db.Customers.Find(id);
                    if (customer == null)
                    {
                        return NotFound();
                    }

                    db.Customers.Remove(customer);
                    await db.SaveChangesAsync();

                    #region ' Faulted MySQL Provider '
                    //MySql - Entity Framework update all the rows of the table so I use T-SQL Commands for the updating
                    //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EncodeContext"].ConnectionString;
                    //using (MySqlConnection cn = new MySqlConnection(connectionString))
                    //{
                    //    MySqlCommand cmd = new MySqlCommand();
                    //    cmd.Connection = cn;
                    //    string query = string.Format("DELETE FROM {0} WHERE Id = {1}", "customers.customer", id);
                    //    cmd.CommandText = query;
                    //    try
                    //    {
                    //        cn.Open();
                    //        int numRowsUpdated = cmd.ExecuteNonQuery();
                    //        cmd.Dispose();
                    //    }
                    //    catch (MySql.Data.MySqlClient.MySqlException ex)
                    //    {
                    //        log.Debug(string.Format("DeleteCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                    //        return InternalServerError(ex);
                    //    }
                    //}
                    #endregion

                    log.Debug(string.Format("DeleteCustomer({0})=", JsonConvert.SerializeObject(customer, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                    return Ok(customer);
                }
            }
            catch (Exception ex)
            {
                log.Debug(string.Format("DeleteCustomer()= Exception{0}", JsonConvert.SerializeObject(ex, Encode.API.Properties.Settings.Default.Tracing ? Formatting.Indented : Formatting.None)));
                return InternalServerError(ex);
            }


        }

        private bool CustomerExists(int id)
        {
            using (EncodeContext db = new EncodeContext())
            {
                return db.Customers.Count(e => e.Id == id) > 0;
            }
        }

    }
}
