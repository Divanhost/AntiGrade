using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class Semester:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        [MaxLength(50)]
        public string Name {get;set;}
        public bool isFirstHalf {get;set;}
        public int Year {get;set;}
        public virtual List<Subject> Subjects {get;set;}
    }
}