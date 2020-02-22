using System.ComponentModel.DataAnnotations;
using AntiGrade.BusinessObjects.Models.Identity;

namespace AntiGrade.BusinessObjects.Models
{
    public class Teacher: IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        public int UserId {get;set;}
        public virtual User User {get;set;}
    }
}