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

        [ForeignKey(nameof(ExamType))]
        public int TypeId {get;set;}
        
        [ForeignKey(nameof(Group))]
        public int GroupId {get;set;}
        public bool IsDeleted {get;set;}
        public virtual ExamType Type {get;set;}
        public virtual Group Group {get;set;}
        public virtual List<Work> Works {get;set;}
        public virtual List<SubjectEmployee> SubjectEmployees {get;set;}
    }
}