using Employees.ServiceLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ServiceLibrary.DbContext
{
    internal class EmployeesDbContext
    {
        public List<EmployeeEntity>? EmployeeEntities { get; set; }

        public List<CommonProjectEntity>? CommonProjectEntities { get; set; }

        public MaxDaysEntity? maxDaysEntity { get; set; }
    }
}
