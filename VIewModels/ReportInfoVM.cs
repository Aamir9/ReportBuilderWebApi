using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportBuilder.VIewModels
{
    public class ReportInfoVM
    {
        public int? EmployeId { get; set; }

        public string EmployeName { get; set; }

        public string EmployeDescription { get; set; }

        public string JoinDate { get; set; }

        public int? CityId { get; set; }
        public string CityName { get; set; }


        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int? CountryId { get; set; }

        public string CountryName { get; set; }





    }
}
