using Employees.ServiceLibrary.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ServiceLibrary.Domain
{
    public class CsvParser : FileParserBase
    {
       
        public override List<EmployeeEntity> ParseValue(string[] lines)
        {
            List<EmployeeEntity> values = lines
                                          .Skip(1)
                                          .Select(v => FromCsv(v))
                                          .ToList();
            return values;
        }

        public override string[] ReadValue(IFormFile formFile)
        {
            using (var reader = new StreamReader(formFile.OpenReadStream()))
            {
                List<string> lines = new List<string>();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    lines.Add(line);
                }

                return lines.ToArray();
            }
        }

        private EmployeeEntity FromCsv(string csvLine)
        {            
            string[] values = csvLine.Split(',');
            EmployeeEntity dailyValues = new EmployeeEntity();
            dailyValues.EmpID = Convert.ToInt32(values[0]); 
            dailyValues.ProjectID = Convert.ToInt32(values[1]);
            dailyValues.DateFrom = Convert.ToDateTime(values[2]);
            DateTime dateTimeTo;
            dailyValues.DateTo = DateTime.TryParse(values[3], out dateTimeTo) ? dateTimeTo : DateTime.Now;

            return dailyValues;
        }
    }
}
