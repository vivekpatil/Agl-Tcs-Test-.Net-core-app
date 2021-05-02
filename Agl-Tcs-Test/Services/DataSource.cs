using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Agl_Tcs_Test.Interfaces;

namespace Agl_Tcs_Test.Services
{
    public class DataSource : IDataSource
    {
        
       async Task<string> IDataSource.GetDataAsync()
       {
           var returnValue = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://agl-developer-test.azurewebsites.net/people.json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    returnValue = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }

            return returnValue;
       }
    }
}
