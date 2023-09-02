using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Models
{
    public class BaseEntity <T>
    {
        [Key]
        [NotNull]
        public T Id { get; set; }
    }
}
