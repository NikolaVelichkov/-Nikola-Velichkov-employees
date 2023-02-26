using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeesUI.Client.Contracts
{
    public interface IUserHttpClient
    {
        Task<HttpResponseMessage> GetRequestAsync(string url);
        Task<bool> PostRequestUploadAsync(string url, string name, string fileName);
    }
}