using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBuilder
{
  public  class CountryVM
    {

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int EmployeId { get; set; }
        public virtual EmployeVM Employe { get; set; }
    }
}
