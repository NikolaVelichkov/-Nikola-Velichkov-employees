using Employees.ServiceLibrary.DbContext;
using Employees.ServiceLibrary.Domain.Contracts;
using Employees.ServiceLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ServiceLibrary.Domain
{
    public class EmployeeManager : IEmployeeManager
    {       
        public List<CommonProjectEntity>? GetLongestWorkingEmployeePairs(List<EmployeeEntity> employeeEntities)
        {            
            if (employeeEntities is null)
            {
                return null;
            }

            Dictionary<string, List<EmployeeEntity>> projectEmployees = GroupByProjectId(employeeEntities);
            List<CommonProjectEntity> commonProjectEntities = FindWorkingEmployeePairs(projectEmployees);

            if (projectEmployees.Count == 0 || commonProjectEntities.Count == 0)
            {
                return null;
            }

            return commonProjectEntities;
        }

        private Dictionary<string, List<EmployeeEntity>> GroupByProjectId(List<EmployeeEntity> employeeEntities)
        {
            Dictionary<string, List<EmployeeEntity>> projectEmployees = new Dictionary<string, List<EmployeeEntity>>();

            foreach (EmployeeEntity employeeProject in employeeEntities)
            {
                string projectKey = employeeProject.ProjectID.ToString();
                if (!projectEmployees.ContainsKey(projectKey))
                {
                    projectEmployees[projectKey] = new List<EmployeeEntity>();
                }
                projectEmployees[projectKey].Add(employeeProject);
            }

            return projectEmployees;
        }

        private List<CommonProjectEntity> FindWorkingEmployeePairs(Dictionary<string, List<EmployeeEntity>> projectEmployees)
        {
            List<CommonProjectEntity> commonProjectEntities = new List<CommonProjectEntity>();

            foreach (KeyValuePair<string, List<EmployeeEntity>> projectGroup in projectEmployees)
            {
                List<EmployeeEntity> employees = projectGroup.Value;
                for (int i = 0; i < employees.Count - 1; i++)
                {
                    for (int j = i + 1; j < employees.Count; j++)
                    {
                        EmployeeEntity emp1 = employees[i];
                        EmployeeEntity emp2 = employees[j];
                        if (emp1.DateFrom <= emp2.DateTo && emp2.DateFrom <= emp1.DateTo)
                        {
                            double timeWorked = emp1.DateTo > emp2.DateTo ? (emp2.DateTo - emp1.DateFrom).TotalDays : (emp1.DateTo - emp2.DateFrom).TotalDays;
                            commonProjectEntities.Add(new CommonProjectEntity { FirstEmployeeID = emp1.EmpID, SecondEmployeeID = emp2.EmpID, ProjectID = emp1.ProjectID, TimeWorked = timeWorked });
                        }
                    }
                }
            }

            return commonProjectEntities;
        }

        public MaxDaysEntity? FindPairWithLogestWorkTime(List<CommonProjectEntity> commonProjectEntities)
        {
            var longestPairs = commonProjectEntities.GroupBy(x => new { x.FirstEmployeeID, x.SecondEmployeeID })
                .Select(g => new MaxDaysEntity
                {
                    FirstEmployeeID = g.Key.FirstEmployeeID,
                    SecondEmployeeID = g.Key.SecondEmployeeID,
                    TimeWorked = g.Sum(x => x.TimeWorked)
                })
                .OrderByDescending(x => x.TimeWorked)
                .ToList();            

            return longestPairs.FirstOrDefault();
        }

        public List<CommonProjectEntity>? FindAllCommonProjectsOfaPair(MaxDaysEntity longestPair, List<CommonProjectEntity> commonProjectEntities)
        {
            List<CommonProjectEntity> CommonProjectPairs = commonProjectEntities
                .Where(x => x.FirstEmployeeID == longestPair.FirstEmployeeID
                && x.SecondEmployeeID == longestPair.SecondEmployeeID).
                OrderByDescending(x => x.TimeWorked)
                .ToList();            

            return CommonProjectPairs;
        }

    }
}
