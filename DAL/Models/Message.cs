
namespace DAL.Models
{
    public class Message : BaseEntity<ulong>, IHasTimestamps
    {
        public User User { get; set; } = null!;
        public Chat Chat { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
    }
}
