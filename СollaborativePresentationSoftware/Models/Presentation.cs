namespace PresentationApp.Models
{
    public class Presentation
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<int> UsersEditorId { get; set; } = new();
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
