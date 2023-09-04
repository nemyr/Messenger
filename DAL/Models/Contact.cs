using System.Diagnostics.CodeAnalysis;

namespace DAL.Models
{
    public class Contact : BaseEntity<ulong>
    {
        [NotNull]
        public User User { get; set; }
        public string? DisplayedName { get; set; } 
    }
}
