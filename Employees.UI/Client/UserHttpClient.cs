using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EmployeesUI.Client.Contracts;

namespace EmployeesUI.Client
{
    public class UserHttpClient : IUserHttpClient
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<bool> PostRequestUploadAsync(string url, string name, string fileName)
        {
            byte[] jsonByteArray;

            try
            {
                jsonByteArray = File.ReadAllBytes(fileName);

            }
            catch
            {

                return false;

            }

            try
            {

                using (var content = new MultipartFormDataContent("Upload File"))
                {

                    content.Add(new StreamContent(new MemoryStream(jsonByteArray)), name, fileName);

                    using (var responseMessage = await client.PostAsync(url, content))
                    {
                        if (!responseMessage.IsSuccessStatusCode)
                        {
                            return false;
                        }
                        
                    }
                }
            }
            catch
            {
                return false;            
            }

            return true;
        }


        public async Task<HttpResponseMessage> GetRequestAsync(string url)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync(url).ConfigureAwait(false);                
                var responseString = await response.Content.ReadAsStringAsync();                

                return response;

            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

        }






    }
}
