using Encode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Encode.Client
{
    public class CustomersClient
    {

        /// <summary>
        /// Method for getting the Customers from the Database
        /// </summary>
        /// <param name="basePath">api base path</param>
        /// <returns>List<Customer></returns>
        public async Task<List<Customer>> GetCustomers(string basePath)
        {
            string apiPath = "{0}/customers";

            string methodUrl = string.Format(apiPath, basePath);
            using (HttpClient client = new HttpClient())
            {
                return await client.MethodGet<List<Customer>>(basePath, methodUrl);
            }
        }

        public async Task<HttpResponseMessage> GetRMCustomerById(string basePath, int id)
        {
            string apiPath = "{0}/customers/{1}";
            string methodUrl = string.Format(apiPath, basePath, id);
            using (HttpClient client = new HttpClient())
            {
                return await client.MethodGet(basePath, methodUrl);
            }
        }

        public async Task<Customer> GetCustomerById(string basePath, int id)
        {
            string apiPath = "{0}/customers/{1}";
            string methodUrl = string.Format(apiPath, basePath, id);
            using (HttpClient client = new HttpClient())
            {
                return await client.MethodGet<Customer>(basePath, methodUrl);
            }
        }
        
        public async Task<HttpResponseMessage> CreateCustomer(string basePath, Customer customer)
        {
            string apiPath = "{0}/customers";
            string methodUrl = string.Format(apiPath, basePath);
            using (HttpClient client = new HttpClient())
            {
                return await client.MethodPost<Customer>(basePath, methodUrl, customer);
            }
        }

        public async Task<HttpResponseMessage> UpdateCustomer(string basePath, int id, Customer customer)
        {
            string apiPath = "{0}/customers/{1}";
            string methodUrl = string.Format(apiPath, basePath, id);
            using (HttpClient client = new HttpClient())
            {
                return await client.MethodPut<Customer>(basePath, methodUrl, customer);
            }
        }

        public async Task<HttpResponseMessage> DeleteCustomer(string basePath, int id)
        {
            string apiPath = "{0}/customers/{1}";
            string methodUrl = string.Format(apiPath, basePath, id);
            using (HttpClient client = new HttpClient())
            {
                return await client.MethodDelete(basePath, methodUrl);
            }
        }


    }
}
