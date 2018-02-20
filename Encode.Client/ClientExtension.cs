using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Encode.Client
{
    public static class ClientExtension
    {
        #region ' Get Methods '

        public static async Task<HttpResponseMessage> MethodGet(this HttpClient client, string baseAddress, string apiPath)
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, apiPath);

            HttpResponseMessage response = null;
            string responseText = null;
            try
            {
                response = await client.SendAsync(req);
                responseText = await response.Content.ReadAsStringAsync();

                return response;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(response.ReasonPhrase) { Source = responseText };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<TViewModel> MethodGet<TViewModel>(this HttpClient client, string baseAddress, string apiPath)
        {
            TViewModel result = default(TViewModel);

            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, apiPath);

            HttpResponseMessage response = null;
            string responseText = null;
            try
            {
                response = await client.SendAsync(req);
                responseText = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                result = JsonConvert.DeserializeObject<TViewModel>(responseText);

                return result;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(response.ReasonPhrase) { Source = responseText };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ' Post Methods'

        /// <summary>
        /// HttpClient Extension, Post Method  with one Parameter 
        /// </summary>
        /// <param name="client">HttpClient</param>
        /// <param name="baseAddress">base Address</param>
        /// <param name="apiPath">Api Path</param>
        /// <param name="id">Value of the Parameter</param>
        /// <typeparam name="TViewModel">Response View Model</typeparam>
        /// <returns>ViewModel</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public static async Task<TViewModel> MethodPost<TViewModel>(this HttpClient client, string baseAddress,
            string apiPath, string id)
        {
            TViewModel result = default(TViewModel);

            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, apiPath);

            List<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>();
            nameValueCollection.Add(new KeyValuePair<string, string>("id", id));
            req.Content = new FormUrlEncodedContent(nameValueCollection);

            HttpResponseMessage response = null;
            string responseText = null;
            try
            {
                response = await client.SendAsync(req);
                responseText = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                result = JsonConvert.DeserializeObject<TViewModel>(responseText);

                return result;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(response.ReasonPhrase) { Source = responseText };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task<TViewModel> MethodPost<TViewModel, TBindingModel>(this HttpClient client,
            string baseAddress, string apiPath, TBindingModel model)
        {
            TViewModel result = default(TViewModel);
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, apiPath);
            req.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            string responseText = null;
            try
            {
                response = await client.SendAsync(req);
                responseText = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                result = JsonConvert.DeserializeObject<TViewModel>(responseText);

                return result;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(response.ReasonPhrase) { Source = responseText };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task<HttpResponseMessage> MethodPost<TBindingModel>(this HttpClient client, string baseAddress, string apiPath, TBindingModel model)
        {

            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, apiPath);
            req.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            string responseText = null;
            try
            {
                response = await client.SendAsync(req);
                responseText = await response.Content.ReadAsStringAsync();
                response.ReasonPhrase = responseText;
                return response;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(response.ReasonPhrase) { Source = responseText };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion
    }
}