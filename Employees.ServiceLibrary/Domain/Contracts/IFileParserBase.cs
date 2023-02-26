using Employees.ServiceLibrary.Entities;
using Microsoft.AspNetCore.Http;

namespace Employees.ServiceLibrary.Domain.Contracts
{
    public interface IFileParserBase
    {
        public List<EmployeeEntity> ParseValue(string[] lines);
        string[] ReadValue(Stream fileStream);
    }
}