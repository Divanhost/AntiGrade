using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class Mode:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        public int WorkMode {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}