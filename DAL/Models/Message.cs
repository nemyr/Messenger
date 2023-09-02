namespace DAL.Models
{
    public class Message : BaseEntity<long>
    {
        public User User { get; set; } = null!;
        public Chat Chat { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
