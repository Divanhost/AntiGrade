using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AntiGrade.Shared.Models.Identity;

namespace AntiGrade.Shared.Models
{
    public class Employee: IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(70)]
        public string FirstName {get;set;}

        [MaxLength(70)]

        public string LastName {get;set;}
        
        [MaxLength(70)]
        public string Patronymic {get;set;}
        public int UserId {get;set;}
        public int EmployeePositionId {get;set;}
        public bool IsFired {get;set;}
        public virtual EmployeePosition EmployeePosition {get;set;}
        public virtual User User {get;set;}
        public virtual List<SubjectEmployee> SubjectEmployees {get;set;}
    }
}