using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Xamarin.Helpers.Sample.Services
{
    public class SampleService
    {
        private static HttpClient _httpClient;
        public SampleService()
        {
            _httpClient = new HttpClient() {BaseAddress = new Uri(EndPoints.RestApi)};
        }
        
        public async Task<List<Model.Todo>> GetTodos()
        {
            try
            {

                //GetAsync Return with Object
                var response = await _httpClient.GetAsync<List<Model.Todo>>("todos");
           
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Value;
                }
                else
                {
                    throw new Exception(
                        $"HttpStatusCode: {response.StatusCode.ToString()} Message: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}