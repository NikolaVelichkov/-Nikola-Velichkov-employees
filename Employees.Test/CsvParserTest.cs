using Employees.ServiceLibrary.Domain;
using Employees.ServiceLibrary.Entities;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace Employees.Test
{
    public class CsvParserTest
    {
        public CsvParser parser { get; set; }

        [SetUp]
        public void Setup()
        {
            parser = new CsvParser();
        }

        [Test]
        public void TestReadValue()
        {
            //Arrange
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Test");
            stringBuilder.AppendLine("Case");

            //Act
            using (var test_Stream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString())))
            {
                var result = parser.ReadValue(test_Stream);

                // Assert    
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result[0], Is.EqualTo("Test"));
                Assert.That(result[1], Is.EqualTo("Case"));

            }


            Assert.Pass();
        }

        [Test]
        public void TestParseValue()
        {
            string[] csvLines = { "EmpID, ProjectID, DateFrom, DateTo", "143,12,1.11.2013,5.1.2014", "218,10,6.5.2012," };
            EmployeeEntity entity = new EmployeeEntity()
            {
                EmpID = 143,
                ProjectID = 12,
                DateFrom = Convert.ToDateTime("1.11.2013"),
                DateTo = Convert.ToDateTime("5.1.2014")
            };
            List<EmployeeEntity> employeeEntities = parser.ParseValue(csvLines);

            Assert.That(employeeEntities.Count(), Is.EqualTo(2));
            EmployeeEntity employeeEntity = employeeEntities[0];
            Assert.That(employeeEntity, Is.Not.Null);
            EmployeeEntity employeeEntity2 = employeeEntities[1];
            Assert.That(employeeEntity2, Is.Not.Null);
            var expectedJson = JsonConvert.SerializeObject(entity);
            var actualJson = JsonConvert.SerializeObject(employeeEntity);
            Assert.That(expectedJson, Is.EqualTo(actualJson));
        }
    }
}