using System.ComponentModel.DataAnnotations;

namespace sitoAutenticazioneFrau.Models
{
    public class UserActionLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public DateTime ActionDate { get; set; } = DateTime.Now;
    }
}
