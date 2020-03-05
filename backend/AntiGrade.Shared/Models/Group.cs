using System.Collections.Generic;

namespace AntiGrade.Shared.Models
{
    public class Group:IEntityWithId<int>
    {
        public int Id {get;set;}
        public int Name {get;set;}
        public virtual List<Student> Students {get;set;}
    }
}