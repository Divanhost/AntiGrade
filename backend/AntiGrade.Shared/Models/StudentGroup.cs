using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class SubjectGroup:IEntityWithId<int>
    {
        public int Id {get;set;}
        public int SubjectId {get;set;}
        public int GroupId {get;set;}
        public virtual Subject Subject {get;set;}
        public virtual Group Group {get;set;}
    }
}