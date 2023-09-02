namespace DAL.Models
{
    public class User : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
