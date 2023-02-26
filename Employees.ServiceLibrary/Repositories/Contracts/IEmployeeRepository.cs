using Employees.ServiceLibrary.Entities;
using Microsoft.AspNetCore.Http;

namespace Employees.ServiceLibrary.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        Task<bool> ReadCsvValue(Stream fileStream);
        Task<MaxDaysEntity?> GetMostworkedPair();
        Task<List<CommonProjectEntity>?> GetCommonProjectPairs();
    }
}