using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class Institute : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name {get;set;}
        public virtual List<Department> Departments {get;set;}
    }
}