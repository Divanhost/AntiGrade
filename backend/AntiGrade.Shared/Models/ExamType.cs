using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class ExamType : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name {get;set;}
    }
}