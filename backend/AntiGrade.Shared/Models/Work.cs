using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class Work:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        
        [MaxLength(50)]
        public int Name {get;set;}

         [Column(TypeName = "decimal(18,5)")]
        public decimal Points {get;set;}

        [ForeignKey(nameof(Subject))]
        public int SubjectId {get;set;}
        public virtual Subject Subject {get;set;}
        public virtual List<Criteria> Criterias {get;set;}
        public List<StudentWork> StudentWorks {get;set;}
        public WorkType WorkType {get;set;}
    }
}