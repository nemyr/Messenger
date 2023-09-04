namespace DAL.Models
{
    public class User : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public List<Contact> Contacts { get; set; }
        public List<UserChat> Chats { get; set; }
    }
}
