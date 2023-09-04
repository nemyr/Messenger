using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserChat : BaseEntity<ulong>
    {
        [Key]
        public Chat Chat { get; set; }
        [Key]
        public User User { get; set; }
        public string DisplayedName { get; set; }
    }
}
