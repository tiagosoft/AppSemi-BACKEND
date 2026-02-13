using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSemi.Domain.Entities;

namespace AppSemi.Application.Interfaces
{
    public interface ISemiService
    {
        Task<IEnumerable<Patients>> GetPatients();
    }
}
