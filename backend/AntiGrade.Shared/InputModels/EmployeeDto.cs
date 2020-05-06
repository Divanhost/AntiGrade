using AntiGrade.Shared.Models;

namespace AntiGrade.Shared.InputModels 
{
    public class EmployeeDto
    {
        public int Id {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Patronymic {get;set;}
        public int? UserId {get;set;}
        public int DepartmentId {get;set;}
    }
}