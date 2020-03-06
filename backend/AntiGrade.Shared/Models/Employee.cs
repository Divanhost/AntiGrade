using System.ComponentModel.DataAnnotations;
using AntiGrade.Shared.Models.Identity;

namespace AntiGrade.Shared.Models
{
    public class Employee: IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public int UserId {get;set;}
        public int EmployeePositionId {get;set;}
        public bool IsFired {get;set;}
        public virtual EmployeePosition EmployeePosition {get;set;}
        public virtual User User {get;set;}
    }
}