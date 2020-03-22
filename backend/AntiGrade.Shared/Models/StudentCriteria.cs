using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class StudentCriteria:IEntityWithId<int>
    {
        public int Id {get;set;}
        public int StudentId {get;set;}
        public int CriteriaId {get;set;}
        public virtual Student Student {get;set;}
        public virtual Criteria Criteria {get;set;}
        [Column(TypeName = "decimal(18,5)")]
        public decimal TotalPoints {get;set;}
        public bool Touched {get;set;}

    }
}