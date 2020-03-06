using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class EmployeePosition : IEntityWithId<int>
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}