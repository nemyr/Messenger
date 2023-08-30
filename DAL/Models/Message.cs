using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    internal class Message
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
