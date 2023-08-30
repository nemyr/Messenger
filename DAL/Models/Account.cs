using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public User User { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime LastVisit { get; set; }
        public DateTime RegisteredDate { get; set;} = DateTime.Now;

    }
}
