using EmployeesUI.Client.Contracts;
using EmployeesUI.Constants;
using EmployeesUI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeesUI.Client
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IUserHttpClient _httpClient;
        public RequestHandler(IUserHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UploadRequest(string fileName)
        {
            bool status = await _httpClient.PostRequestUploadAsync(Endpoints.UploadFile, "formFile", fileName);

            if (!status)
            {
                MessageBox.Show("Upload failed");
                return false;
            }

            MessageBox.Show("Upload success");
            return status;
        }

        public async Task<MaxDaysEntity?> GetMostworkedPairAsync()
        {
            var resposne = await _httpClient.GetRequestAsync(Endpoints.GetMostWorkedHours);

            if (!resposne.IsSuccessStatusCode)
            {
                MessageBox.Show("Request failed");
                return null;
            }

            return JsonConvert.DeserializeObject<MaxDaysEntity>(await resposne.Content.ReadAsStringAsync());


        }

        public async Task<List<CommonProjectEntity>?> GetListCommonProjectPairsAsync()
        {
            var resposne = await _httpClient.GetRequestAsync(Endpoints.GetCommonProjectpairs);

            if (!resposne.IsSuccessStatusCode)
            {
                MessageBox.Show("Request failed");
                return null;
            }

            return JsonConvert.DeserializeObject<List<CommonProjectEntity>?>(await resposne.Content.ReadAsStringAsync());

        }
    }
}
