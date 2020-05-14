using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class Department : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name {get;set;}
        [ForeignKey(nameof(Institute))]
        public int InstituteId {get;set;}
        public virtual List<Employee> Employees {get; set;}
        public virtual Institute Institute {get;set;}
    }
}