using System.Collections.Generic;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using BusinessEntities;
using BusinessServices;

namespace ReportBuilder.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RepportBuilderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
         

        public RepportBuilderController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
            
        }

        //post  api/RepportBuilder/GetFieldsData
        [HttpPost] 
        public JsonResult GetFieldsData(List<string> body)
        {
            TablesDataInfo tablesData = new TablesDataInfo(_unitOfWork);
            var result = tablesData.GetData(body);
            if(result != null)
                return new JsonResult(new { code = 1, message = "success" , data = result });
            else
                return new JsonResult(new { code = 0, message = "Internal Server Error", data = result });
        }

        //post api/RepportBuilder/GetColumnsNames
       [HttpPost]
        public JsonResult GetColumnsNames()
        {
            Employe empEntity = new Employe();
            Department deptEntity = new Department();
            City cityEntity = new City();
            Country countryEntity = new Country();

            return new JsonResult(new 
            {  code = 1,
               message = "success",
               employeeCols = _unitOfWork.EmployeRepository.GetColumnsName(empEntity),
               departmentCols = _unitOfWork.DepartmentRepositroy.GetColumnsName(deptEntity),
               cityCols= _unitOfWork.CityRepository.GetColumnsName(cityEntity),
               countryCols = _unitOfWork.CountryRepository.GetColumnsName(countryEntity),
            }
            );
        }

    }
}
