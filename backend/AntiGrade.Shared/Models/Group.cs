using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class Group:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        [MaxLength(50)]
        public string Name {get;set;}
        public bool IsDeleted {get;set;}
        public virtual List<Student> Students {get;set;}
        public virtual List<Subject> Subjects {get;set;}
    }
}