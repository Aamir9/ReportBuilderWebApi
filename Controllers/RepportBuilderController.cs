
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using ReportBuilder.VIewModels;

namespace ReportBuilder.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RepportBuilderController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;


       
        public RepportBuilderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //post  api/RepportBuilder/GetFieldsData
        [HttpPost]
        public JsonResult GetFieldsData()
        {
            var employeeList = _unitOfWork.EmployeRepository.Get();
            var departmentList = _unitOfWork.DepartmentRepositroy.Get();
            var citylist = _unitOfWork.CityRepository.Get();
            var Countrylist = _unitOfWork.CountryRepository.Get();

            var data = (from Emp in employeeList join Dpt in
             departmentList on Emp.EmployeId equals Dpt.EmployeId join cty in 
             citylist on Emp.EmployeId equals cty.EmployeId join cuntry in 
             Countrylist on Emp.EmployeId equals cuntry.EmployeId
                        select new ReportInfoVM
                        {
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

           
            //GetPropertyName((ReportInfoVM u) => u.EmployeName).ToLower())
            
  




            
            return new JsonResult(new { code = 1, message = "success" , data = data });
        }



        //public static string GetPropertyName<T>(System.Linq.Expressions.Expression<Func<T, object>> property)
        //{
        //    System.Linq.Expressions.LambdaExpression lambda = (System.Linq.Expressions.LambdaExpression)property;
        //    System.Linq.Expressions.MemberExpression memberExpression;

        //    if (lambda.Body is System.Linq.Expressions.UnaryExpression)
        //    {
        //        System.Linq.Expressions.UnaryExpression unaryExpression = (System.Linq.Expressions.UnaryExpression)(lambda.Body);
        //        memberExpression = (System.Linq.Expressions.MemberExpression)(unaryExpression.Operand);
        //    }
        //    else
        //    {
        //        memberExpression = (System.Linq.Expressions.MemberExpression)(lambda.Body);
        //    }

        //    return ((PropertyInfo)memberExpression.Member).Name;
        //}

    }
}
