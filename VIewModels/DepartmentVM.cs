using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBuilder
{
   public class DepartmentVm
    {

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }


        public int EmployeId { get; set; }
        public virtual EmployeVM Employe { get; set; }
    }
}
