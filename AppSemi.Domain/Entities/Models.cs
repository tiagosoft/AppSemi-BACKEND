using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSemi.Domain.Entities
{
    public class Patients
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
