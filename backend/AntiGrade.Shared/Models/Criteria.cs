using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class Criteria:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}

        [MaxLength(50)]
        public int Name {get;set;}

        [Column(TypeName = "decimal(18,5)")]
        public decimal Points {get;set;}
        [ForeignKey(nameof(Work))]
        public int WorkId {get;set;}
        public virtual Work Work {get;set;}
    }
}