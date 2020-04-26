using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class Status:IEntityWithId<int>
    {
        [Key]
        public int Id {get;set;}
        [MaxLength(70)]
        public string Name {get;set;}
    }
}