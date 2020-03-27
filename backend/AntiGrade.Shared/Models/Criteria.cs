using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace AntiGrade.Shared.Models
{
    public class Criteria:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}

        [MaxLength(100)]
        public string Name {get;set;}

        [Column(TypeName = "decimal(18,5)")]
        public decimal MaxPoints {get;set;}

        [ForeignKey(nameof(Work))]
        public int WorkId {get;set;}
        public virtual Work Work {get;set;}
        public virtual List<StudentCriteria> StudentCriterias {get;set;}
    }
}