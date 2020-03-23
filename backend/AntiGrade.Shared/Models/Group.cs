using System.Collections.Generic;

namespace AntiGrade.Shared.Models
{
    public class Group:IEntityWithId<int>
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public bool IsDeleted {get;set;}
        public virtual List<Student> Students {get;set;}
        public virtual List<SubjectGroup> SubjectGroups {get;set;}
    }
}