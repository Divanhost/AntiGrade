using System.ComponentModel.DataAnnotations;

namespace AntiGrade.Shared.Models
{
    public class TokenCouple : IEntityWithId<int>
    {

        [Key]
        public int Id { get; set; }
        public string Jwt { get; set; }
        public string Refresh { get; set; }
    }
}
