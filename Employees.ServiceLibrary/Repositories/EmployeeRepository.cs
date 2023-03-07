using Employees.ServiceLibrary.DbContext;
using Employees.ServiceLibrary.Domain;
using Employees.ServiceLibrary.Domain.Contracts;
using Employees.ServiceLibrary.Entities;
using Employees.ServiceLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ServiceLibrary.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IFileParserBase _fileParserBase;
        private readonly IEmployeeManager _employeeManager;
        private EmployeesDbContext EmployeesDbContext = new EmployeesDbContext();
        public EmployeeRepository(IFileParserBase fileParserBase, IEmployeeManager employeeManager)
        {
            _fileParserBase = fileParserBase;
            _employeeManager = employeeManager;
        }
        public Task<bool> ReadCsvValue(Stream fileStream)
        {
            try
            {
                string[] listOfLines = _fileParserBase.ReadValue(fileStream);
                EmployeesDbContext.EmployeeEntities = _fileParserBase.ParseValue(listOfLines);
            }
            catch
            {

                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<MaxDaysEntity?> GetMostworkedPair()
        {
            EmployeesDbContext.CommonProjectEntities = _employeeManager.GetLongestWorkingEmployeePairs(EmployeesDbContext.EmployeeEntities);
            EmployeesDbContext.maxDaysEntity = _employeeManager.FindPairWithLogestWorkTime(EmployeesDbContext.CommonProjectEntities);
            return Task.FromResult(EmployeesDbContext.maxDaysEntity);
                
        }

        public Task<List<CommonProjectEntity>?> GetCommonProjectPairs()
        {           
            return Task.FromResult(_employeeManager.FindAllCommonProjectsOfaPair(EmployeesDbContext.maxDaysEntity, EmployeesDbContext.CommonProjectEntities));

        }
    }
}
