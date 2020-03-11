using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class WorkType:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        [MaxLength(50)]
        public int Name {get;set;}
    }
}