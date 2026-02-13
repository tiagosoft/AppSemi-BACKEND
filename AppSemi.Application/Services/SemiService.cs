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

        public async Task<int> CreateOrder(OrdersDTO request)
        {
            try
            {
                var orderId = await _iSemiRepository.CreateOrder(request);
                return orderId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
                throw new ApplicationException("Ocurrió un error al crear la orden intente nuevamente.");
            }
        }

        public async Task<IEnumerable<Exams>> GetExams()
        {
            try
            {
                var exams = await _iSemiRepository.GetExams();
                return exams;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
                throw new ApplicationException("Ocurrió un error al consultar los examenes intente nuevamente.");
            }
        }

        public async Task<IEnumerable<Orders>> GetOrders()
        {
            try
            {
                var orders = await _iSemiRepository.GetOrders();
                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
                throw new ApplicationException("Ocurrió un error al consultar las ordenes intente nuevamente.");
            }
        }

        public async Task<IEnumerable<OrdersDetail>> GetOrdersDetail(OrdersDetailDTO request)
        {
            try
            {
                var ordersDetail = await _iSemiRepository.GetOrdersDetail(request);
                return ordersDetail;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
                throw new ApplicationException("Ocurrió un error al consultar los pacientes intente nuevamente.");
            }
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
