using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesUI.Constants
{
    public class Endpoints
    {
        public static readonly string UploadFile = "https://localhost:7276/api/Employees/UploadFile";
        public static readonly string GetMostWorkedHours = "https://localhost:7276/api/Employees/MostWorkedPair";
        public static readonly string GetCommonProjectpairs = "https://localhost:7276/api/Employees/CommonProjectPairs";
    }
}
