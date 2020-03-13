using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class Student:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        [MaxLength(50)]
        public string FirstName {get;set;}
        [MaxLength(50)]
        public string LastName {get;set;}

        [ForeignKey(nameof(Group))]
        public int GroupId {get;set;}

        public virtual Group Group {get;set;}
        public List<StudentCriteria> StudentCriterias {get;set;}
    }
}