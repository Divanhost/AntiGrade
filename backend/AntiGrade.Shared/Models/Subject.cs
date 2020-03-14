using System.Collections.Generic;
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
        public virtual List<Group> Groups{get;set;}
        public virtual List<Employee> Teachers {get;set;}
    }
}