using System.Collections.Generic;
using AntiGrade.Shared.Enums;

namespace AntiGrade.Shared.Models
{
    public class Subject : IEntityWithId<int>
    {
        public int Id { get; set; }
        public string Name {get;set;}
        public GradeType Type {get;set;}
        public virtual List<Group> Groups{get;set;}
    }
}