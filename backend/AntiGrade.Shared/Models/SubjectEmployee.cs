using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class SubjectEmployee:IEntityWithId<int>
    {
        public int Id {get;set;}
        public int SubjectId {get;set;}
        public int EmployeeId {get;set;}
        public virtual Subject Subject {get;set;}
        public virtual Employee Employee {get;set;}
        public string Status {get;set;}
    }
}