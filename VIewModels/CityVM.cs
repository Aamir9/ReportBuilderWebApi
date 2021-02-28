
namespace ReportBuilder
{
   public class CityVM
    {
        public int CityId { get; set; }
        public string CityName { get; set; }


        public int EmployeId { get; set; }
        public virtual EmployeVM Employe { get; set; }
    }
}
