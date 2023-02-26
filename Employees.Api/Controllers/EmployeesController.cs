using Employees.ServiceLibrary.Entities;
using Employees.ServiceLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employees.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeRepository _employeerepository;
        public EmployeesController(IEmployeeRepository employeerepository)
        {
            _employeerepository = employeerepository;
        }
        [HttpGet("MostWorkedPair")] 
        public async Task<IActionResult> GetMostworkedPairAsync()            
        {
            MaxDaysEntity maxDaysEntity = await _employeerepository.GetMostworkedPair();
            if (maxDaysEntity is null)
            {
                return BadRequest();
            }
            return Ok(maxDaysEntity);
        }

        [HttpGet("CommonProjectPairs")]
        public async Task<IActionResult> GetListCommonProjectPairsAsync()
        {
            List<CommonProjectEntity>? CommonProjectPairs = await _employeerepository.GetCommonProjectPairs();
            if (CommonProjectPairs is null)
            {
                return BadRequest();
            }
            return Ok(CommonProjectPairs);
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFileAsync(IFormFile formFile)
        {          
            if (await _employeerepository.ReadCsvValue(formFile))
            {
                return Ok(formFile);

            }
            return BadRequest(formFile);
        }
    }
}
