using Employees.ServiceLibrary.Entities;

namespace Employees.ServiceLibrary.Domain.Contracts
{
    public interface IEmployeeManager
    {
        //MaxDaysEntity? FindMaxDays();
        List<CommonProjectEntity> GetLongestWorkingEmployeePairs(List<EmployeeEntity> employeeEntities);
        MaxDaysEntity? FindPairWithLogestWorkTime(List<CommonProjectEntity> commonProjectEntities);
        List<CommonProjectEntity>? FindAllCommonProjectsOfaPair(MaxDaysEntity longestPair, List<CommonProjectEntity> commonProjectEntities);
    }
}