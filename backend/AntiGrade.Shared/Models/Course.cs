using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace AntiGrade.Shared.Models
{
    public class Course:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}

        [MaxLength(100)]
        public string Name {get;set;}
        public virtual List<Group> Groups {get;set;}
    }
}