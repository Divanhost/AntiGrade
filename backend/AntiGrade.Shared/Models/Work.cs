using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class Work:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        
        [MaxLength(100)]
        public string Name {get;set;}

        [Column(TypeName = "decimal(18,5)")]
        public decimal MaxPoints {get;set;}

        [ForeignKey(nameof(Subject))]
        public int SubjectId {get;set;}

        [ForeignKey(nameof(WorkType))]
        public int WorkTypeId {get;set;}
        public virtual Subject Subject {get;set;}
        public virtual List<Criteria> Criterias {get;set;}
        public virtual WorkType WorkType {get;set;}
        public virtual List<StudentWork> StudentWorks {get;set;}

    }
}