using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class Student:IEntityWithId<int>
    {
        public int Id {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}

        [ForeignKey(nameof(Group))]
        public int GroupId {get;set;}

        public virtual Group Group {get;set;}
    }
}