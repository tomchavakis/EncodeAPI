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

            HttpResponseMessage response = _client.CreateCustomer(basePath, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.Created);
        }


        [Test]
        [Order(5)]
        [Category("CreateCustomer")]

        public void CreateCustomer_InvalidNumberOfEmployees_ReturnNonBadRequest()
        {
            Customer customer = new Customer()
            {
                NumberOfEmployees = 50000,
                Title = "Test - Non Valid Customer"
            };

            HttpResponseMessage response = _client.CreateCustomer(basePath, customer).Result;
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

            HttpResponseMessage response = _client.CreateCustomer(basePath, customer).Result;
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
            HttpResponseMessage response = _client.CreateCustomer(basePath, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.That(response.ReasonPhrase.Contains("Customer Cannot be null"));
        }

        [Test]
        [Order(8)]
        [Category("UpdateCustomer")]
        public void UpdateCustomer_Valid_ReturnNoContent()
        {
            int updateId = 6;

            Customer customer = new Customer
            {
                Title = "Updated Id",
                NumberOfEmployees = 500
            };
            HttpResponseMessage response = _client.UpdateCustomer(basePath, updateId, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }

        [Test]
        [Order(9)]
        [Category("UpdateCustomer")]
        public void UpdateCustomer_NotExist_ReturnNotFoundAndInvalidId()
        {
            int updateId = 250;

            Customer customer = new Customer
            {
                Title = "Updated Id2",
                NumberOfEmployees = 500
            };
            HttpResponseMessage response = _client.UpdateCustomer(basePath, updateId, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
            Assert.That(response.ReasonPhrase.Contains("Invalid Id"));
        }


        [Test]
        [Order(10)]
        [Category("UpdateCustomer")]
        public void UpdateCustomer_Null_ReturnBadRequest()
        {
            int updateId = 6;

            Customer customer = null;
            HttpResponseMessage response = _client.UpdateCustomer(basePath, updateId, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.That(response.ReasonPhrase.Contains("Customer Cannot be null"));
        }


        [Test]
        [Order(11)]
        [Category("UpdateCustomer")]
        public void UpdateCustomer_InvalidNumberOfEmployees_ReturnNonBadRequest()
        {
            int updateId = 6;

            Customer customer = new Customer()
            {
                NumberOfEmployees = 50000,
                Title = "Test - Non Valid Customer"
            };

            HttpResponseMessage response = _client.UpdateCustomer(basePath, updateId, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.That(response.ReasonPhrase.Contains("Value for NumberOfEmployees must be between 0 and 10000."));
        }

        [Test]
        [Order(12)]
        [Category("CreateCustomer")]

        public void UpdateCustomer_InvalidTitle_ReturnBadRequest()
        {
            int updateId = 6;
            string invalidTitle = new string('a', 500);

            Customer customer = new Customer()
            {
                NumberOfEmployees = 200,
                Title = invalidTitle
            };

            HttpResponseMessage response = _client.UpdateCustomer(basePath, updateId, customer).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.That(response.ReasonPhrase.Contains("Title must be 200 characters or less"));
        }

        [Test]
        [Order(13)]
        [Category("DeleteCustomer")]
        public void DeleteCustomer_ValidId_ReturnDeleted()
        {
            int deleteId = 10;
            HttpResponseMessage response = _client.DeleteCustomer(basePath, deleteId).Result;
            Assert.NotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Test]
        [Order(14)]
        [Category("DeleteCustomer")]
        public void DeleteCustomer_InvalidId_ReturnNotFound()
        {
            int deleteId = 500;
            HttpResponseMessage response = _client.DeleteCustomer(basePath, deleteId).Result;
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
            Assert.That(response.ReasonPhrase.Contains("Invalid Id"));
        }

    }
}
