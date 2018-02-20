﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Encode.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for Customers.
    /// </summary>
    public static partial class CustomersExtensions
    {
            /// <summary>
            /// Get All the Customer
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static object GetCustomers(this ICustomers operations)
            {
                return Task.Factory.StartNew(s => ((ICustomers)s).GetCustomersAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get All the Customer
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetCustomersAsync(this ICustomers operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCustomersWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Create a New Customer
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='customer'>
            /// Customer
            /// </param>
            public static Customer PostCustomer(this ICustomers operations, Customer customer)
            {
                return Task.Factory.StartNew(s => ((ICustomers)s).PostCustomerAsync(customer), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Create a New Customer
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='customer'>
            /// Customer
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Customer> PostCustomerAsync(this ICustomers operations, Customer customer, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostCustomerWithHttpMessagesAsync(customer, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get a Specific Customer
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of the Customer
            /// </param>
            public static Customer GetCustomer(this ICustomers operations, int id)
            {
                return Task.Factory.StartNew(s => ((ICustomers)s).GetCustomerAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get a Specific Customer
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of the Customer
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Customer> GetCustomerAsync(this ICustomers operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCustomerWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Update a specific Customer
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of the Customer
            /// </param>
            /// <param name='customer'>
            /// Updated Customer
            /// </param>
            public static void PutCustomer(this ICustomers operations, int id, Customer customer)
            {
                Task.Factory.StartNew(s => ((ICustomers)s).PutCustomerAsync(id, customer), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Update a specific Customer
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id of the Customer
            /// </param>
            /// <param name='customer'>
            /// Updated Customer
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PutCustomerAsync(this ICustomers operations, int id, Customer customer, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PutCustomerWithHttpMessagesAsync(id, customer, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// Delete a Customer by ID
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id Of the Customer
            /// </param>
            public static Customer DeleteCustomer(this ICustomers operations, int id)
            {
                return Task.Factory.StartNew(s => ((ICustomers)s).DeleteCustomerAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Delete a Customer by ID
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Id Of the Customer
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Customer> DeleteCustomerAsync(this ICustomers operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.DeleteCustomerWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
