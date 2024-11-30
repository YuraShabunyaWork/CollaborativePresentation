using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace СollaborativePresentationSoftware.Models
{
    [Index("Email", IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime Created {  get; set; } = DateTime.Now;
        public DateTime LastLogin { get; set; } = DateTime.Now;
        public Presentation? Presentation { get; set; }
    }
}
