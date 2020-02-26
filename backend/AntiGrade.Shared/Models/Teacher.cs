using System.ComponentModel.DataAnnotations;
using AntiGrade.Shared.Models.Identity;

namespace AntiGrade.Shared.Models
{
    public class Teacher: IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        public int UserId {get;set;}
        public virtual User User {get;set;}
    }
}