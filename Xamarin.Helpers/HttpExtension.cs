using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


public static class HttpExtension
{
    public static async Task<ServiceResponse<T>> PostAsync<T>(this HttpClient httpClient, string address, object dto)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

            var response = await httpClient.PostAsync(
                address,
                content);

            return await GetResponse<T>(response);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<T>(
                HttpStatusCode.InternalServerError,
                ex);
        }
    }

    public static async Task<ServiceResponse<T>> GetAsync<T>(this HttpClient httpClient, string address)
    {
        try
        {
            var response = await httpClient.GetAsync(address);
            return await GetResponse<T>(response);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<T>(
                HttpStatusCode.InternalServerError,
                ex);
        }
    }

    public static async Task<ServiceResponse<T>> GetAsync<T>(this HttpClient httpClient, string address,
        Dictionary<string, string> values)
    {
        try
        {
            var builder = new StringBuilder(address);
            builder.Append("?");

            foreach (var pair in values)
            {
                builder.Append($"{pair.Key}={pair.Value}&amp;");
            }

            var response = await httpClient.GetAsync(builder.ToString());
            return await GetResponse<T>(response);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<T>(
                HttpStatusCode.InternalServerError,
                ex);
        }
    }

    private static async Task<ServiceResponse<T>> GetResponse<T>(HttpResponseMessage response)
    {
        var returnResponse = new ServiceResponse<T>(response.StatusCode);

        try
        {
            returnResponse.Content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return returnResponse;
            }

            //  returnResponse.Content = await response.Content.ReadAsStringAsync();

            returnResponse.Value = JsonConvert.DeserializeObject<T>(returnResponse.Content);
        }
        catch (Exception ex)
        {
            returnResponse.Error = ex;
        }

        return returnResponse;
    }


}
