using Encode.Client;
using Encode.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Encode.UnitTests
{
    [TestFixture]
    public class CustomersUnitTest
    {
        const string basePath = "http://localhost:50213/api";
        private CustomersClient _client = null;

        [SetUp]
        public void Setup()
        {
            _client = new CustomersClient();
        }


        [Test]
        [Order(1)]
        [Category("GetCustomer")]

        public void Customers_GetAllCustomers_ReturnList()
        {
            List<Customer> result = _client.GetCustomers(basePath).Result;

            Assert.IsNotNull(result);
            Assert.That(result.Count > 0);
            Console.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Test]
        [Order(2)]
        [Category("GetCustomer")]

        public void GetCustomer_CustomerExists_ReturnCustomerFromDB()
        {
            Customer customer = _client.GetCustomerById(basePath, 1).Result;

            Assert.IsNotNull(customer);
            Console.WriteLine(JsonConvert.SerializeObject(customer, Formatting.Indented));

            Assert.That(customer.Id > 0);
            Assert.That(customer.Title, Is.EqualTo("KLM"));
            Assert.That(customer.NumberOfEmployees, Is.EqualTo(8000));

        }

        [Test]
        [Order(3)]
        [Category("GetCustomer")]
        public void GetCustomer_InvalidId_ReturnNotFound()
        {
            HttpResponseMessage response = _client.GetRMCustomerById(basePath, 300).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }


        [Test]
        [Order(4)]
        [Category("CreateCustomer")]
        public void CreateCustomer_Valid_ReturnCustomer()
        {
            Customer customer = new Customer()
            {
                NumberOfEmployees = 5000,
                Title = "Test - Customer"
            };

            Customer response = _client.CreateCustomer(basePath, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.NumberOfEmployees, Is.EqualTo(5000));
            Assert.That(response.Title, Is.EqualTo("Test - Customer"));

        }


        [Test]
        [Order(5)]
        [Category("CreateCustomer")]

        public void CreateCustomer_NumberOfEmployeesError_ReturnNonBadRequest()
        {
            Customer customer = new Customer()
            {
                NumberOfEmployees = 50000,
                Title = "Test - Non Valid Customer"
            };

            HttpResponseMessage response = _client.CreateRMCustomer(basePath, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.That(response.ReasonPhrase.Contains("Value for NumberOfEmployees must be between 0 and 10000."));
        }

        [Test]
        [Order(6)]
        [Category("CreateCustomer")]

        public void CreateCustomer_InvalidTitle_ReturnBadRequest()
        {
            string invalidTitle = new string('a', 500);

            Customer customer = new Customer()
            {
                NumberOfEmployees = 200,
                Title = invalidTitle
            };

            HttpResponseMessage response = _client.CreateRMCustomer(basePath, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.That(response.ReasonPhrase.Contains("Title must be 200 characters or less"));
        }

        [Test]
        [Order(7)]
        [Category("CreateCustomer")]

        public void CreateCustomer_Null_ReturnBadRequest()
        {
            Customer customer = null;
            HttpResponseMessage response = _client.CreateRMCustomer(basePath, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.That(response.ReasonPhrase.Contains("Customer Cannot be null"));
        }









    }
}
