using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    internal interface IHasTimestamps
    {
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
