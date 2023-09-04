namespace DAL.Models
{
    public class Account : BaseEntity<Guid>, IHasTimestamps
    {
        public User User { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpireRefreshToken { get; set; } = DateTime.MinValue;
        public DateTime LastVisit { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
    }
}
