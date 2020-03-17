using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class WorkType:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        [MaxLength(80)]
        public string Name {get;set;}
    }
}