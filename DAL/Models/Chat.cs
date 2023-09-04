namespace DAL.Models
{
    public class Chat : BaseEntity<ulong>
    {
        public List<UserChat> UserChats { get; set; } = null!;
    }
}
