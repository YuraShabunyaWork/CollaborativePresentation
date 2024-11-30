using System.ComponentModel.DataAnnotations;

namespace PresentationApp.ModelsDto
{
    public class SinginDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
