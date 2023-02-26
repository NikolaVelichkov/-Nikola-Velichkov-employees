using Employees.ServiceLibrary.Domain.Contracts;
using Employees.ServiceLibrary.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ServiceLibrary.Domain
{
    public abstract class FileParserBase : IFileParserBase
    {       
        abstract public string[] ReadValue(Stream fileStream);
        abstract public List<EmployeeEntity> ParseValue(string[] lines);
    }

}

