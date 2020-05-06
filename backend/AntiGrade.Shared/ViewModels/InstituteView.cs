using System.Collections.Generic;

namespace AntiGrade.Shared.ViewModels
{
    public class InstituteView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DepartmentView> Departments {get;set;}
    }
}