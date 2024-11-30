using System.ComponentModel.DataAnnotations;

namespace PresentationApp.ModelsDto
{
    public class LoginDto
    {
        [Required]
        public string Email {  get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsCheck { get; set; }
    }
}
