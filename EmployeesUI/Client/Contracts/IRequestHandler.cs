using EmployeesUI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesUI.Client.Contracts
{
    public interface IRequestHandler
    {
        Task<bool> UploadRequest(string fileName);
        Task<MaxDaysEntity?> GetMostworkedPairAsync();
        Task<List<CommonProjectEntity>?> GetListCommonProjectPairsAsync();
    }
}