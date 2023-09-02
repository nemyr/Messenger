namespace DAL.Models
{
    public class Account : BaseEntity<Guid>
    {
        public User User { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime LastVisit { get; set; }
        public DateTime RegisteredDate { get; set;} = DateTime.Now;
    }
}
