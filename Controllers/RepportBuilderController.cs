using System.Collections.Generic;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using BusinessServices;
using BusinessEntities;

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
        public JsonResult GetFieldsData(FieldsInfoVM data)
        {
            
           
            TablesDataInfo tablesData = new TablesDataInfo(_unitOfWork);
            var result = tablesData.GetData(data);
            if (result != null)
                return new JsonResult(new { code = 1, message = "success", data = result });
            else
                return new JsonResult(new { code = 0, message = "Internal Server Error", data = result });

            return null;
        }
        //post api/RepportBuilder/GetColumnsNames
        [HttpPost]
        public JsonResult GetColumnsNames()
        {
          
            ColumnsInfo columnsInfo = new ColumnsInfo(_unitOfWork);
            return columnsInfo.GetColumnsNames();
        }

        //post api/RepportBuilder/GetOperators/xyz..
      
        [HttpPost("{body}")]
        public JsonResult GetOperators( string body)
        {
            Operators opt = new Operators(_unitOfWork);
            if(body != "" && body != null)
                return opt.GetOperators(body);
            else
                return new JsonResult(new
                {
                    code = 0,
                    message = "something wrong ",
                   

                });
        }

    }
}
