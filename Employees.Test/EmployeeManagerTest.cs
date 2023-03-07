using Employees.ServiceLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Test
{
    public class EmployeeManagerTest
    {
        public EmployeeManager manager{ get; set; }
        [SetUp]
        public void Setup()
        {
           manager = new EmployeeManager();
        }

        [Test]
        public void GetLongestWorkingEmployeePairsTest()
        {
            Assert.Pass();
        }
    }
}
