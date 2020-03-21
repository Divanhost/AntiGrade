using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class StudentWork:IEntityWithId<int>
    {
        public int Id {get;set;}
        public int StudentId {get;set;}
        public int WorkId {get;set;}
        public virtual Student Student {get;set;}
        public virtual Work Work {get;set;}
        [Column(TypeName = "decimal(18,5)")]
        public decimal SumOfPoints {get;set;}
    }
}