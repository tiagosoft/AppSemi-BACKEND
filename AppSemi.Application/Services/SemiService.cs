using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSemi.Application.Interfaces;
using AppSemi.Domain.Entities;
using AppSemi.Domain.Interfaces;

namespace AppSemi.Application.Services
{
    public class SemiService : ISemiService
    {
        private readonly ISemiRepository _iSemiRepository;

        public SemiService(ISemiRepository iSemiRepository)
        {
            _iSemiRepository = iSemiRepository;
        }
        public async Task<IEnumerable<Patients>> GetPatients()
        {

            try
            {
                var patients = await _iSemiRepository.GetPatients();
                return patients;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
                throw new ApplicationException("Ocurrió un error al consultar el paciente intente nuevamente.");
            }
        }



    }
}
