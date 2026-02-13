using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSemi.Domain.Entities;

namespace AppSemi.Domain.Interfaces
{
    public interface ISemiRepository
    {
        Task<IEnumerable<Patients>> GetPatients();
    }
}
