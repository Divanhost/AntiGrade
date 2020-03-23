using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AntiGrade.Shared.Enums;

namespace AntiGrade.Shared.Models
{
    public class Subject : IEntityWithId<int>
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name {get;set;}
        public int TypeId {get;set;}
        public ExamType Type {get;set;}
        public bool IsDeleted {get;set;}
        [DefaultValue(false)]
        public bool HasPlan {get;set;}
        public virtual List<SubjectGroup> SubjectGroups {get;set;}
        public virtual List<Employee> Teachers {get;set;}
    }
}