using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Chat : BaseEntity<Guid>
    {
        public List<User> Users { get; set; } = null!;
    }
}
