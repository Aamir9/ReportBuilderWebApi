using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using ReportBuilder.VIewModels;
using BusinessServices;
using BusinessEntities;
using System.Collections;
using System.Collections.Specialized;

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

            var employeeList = _unitOfWork.EmployeRepository.Get();
            var departmentList = _unitOfWork.DepartmentRepositroy.Get();
            var citylist = _unitOfWork.CityRepository.Get();
            var Countrylist = _unitOfWork.CountryRepository.Get();

            //var prop =   typeof(ReportInfoVM).GetProperties().Where(a => a.Name == "EmployeName");
            //   typeof(ReportInfoVM).GetProperties().Where(a => a.Name == "EmployeId").Select(n => n.Name).FirstOrDefault()

            IList< ReportInfoVM> data = (from Emp in employeeList join Dpt in
            departmentList on Emp.EmployeId equals Dpt.EmployeId join cty in 
            citylist on Emp.EmployeId equals cty.EmployeId join cuntry in 
            Countrylist on Emp.EmployeId equals cuntry.EmployeId
            select new  ReportInfoVM{

                EmployeId = Emp.EmployeId,
                EmployeName = Emp.EmployeName,
                EmployeDescription = Emp.EmployeDescription,
                JoinDate = Emp.JoinDate.ToString("dd-MM-yyyy"),
                DepartmentId = Dpt.DepartmentId,
                DepartmentName = Dpt.DepartmentName,
                CityId = cty.CityId,
                CityName = cty.CityName,
                CountryId = cuntry.CountryId,
                CountryName = cuntry.CountryName,
            }).ToList();

            List<object> result = new List<object>();
            ListDictionary PropWithValues = new ListDictionary();
            foreach (var item in data)
            {
                foreach (var propName in body)
                {
                    var v = item.GetType().GetProperties().
                    Where(a => a.Name == propName).Select(p => p.GetValue(item)).FirstOrDefault();
                    PropWithValues.Add(propName, v);
                    
                }
                result.Add(PropWithValues);
                PropWithValues = new ListDictionary();
            }
             return new JsonResult(new { code = 1, message = "success" , data = result });
        }

        //post api/RepportBuilder/GetColumnsNames
       [HttpPost]
        public JsonResult GetColumnsNames()
        {
            Employe empEntity = new Employe();
            List<string> employeeCols = _unitOfWork.EmployeRepository.GetColumnsName(empEntity);
         
            Department deptEntity = new Department();
            List<string> departmentCols = _unitOfWork.DepartmentRepositroy.GetColumnsName(deptEntity);

            City cityEntity = new City();
            List<string> cityCols = _unitOfWork.CityRepository.GetColumnsName(cityEntity);

            Country countryEntity = new Country();
            List<string> countryCols = _unitOfWork.CountryRepository.GetColumnsName(countryEntity);

            return new JsonResult(new 
            {  code = 1,
               message = "success",
               employeeCols = employeeCols,
               departmentCols = departmentCols,
               cityCols= cityCols,
               countryCols = countryCols,
            }
            );
        }

    }
}
