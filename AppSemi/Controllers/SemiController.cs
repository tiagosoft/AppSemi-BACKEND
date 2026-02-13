using AppSemi.Application.Interfaces;
using AppSemi.Domain.Entities;
using AppSemi.Infrastructure.Data;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppSemi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SemiController : ControllerBase
    {
        private readonly ISemiService _iSemiService;

        public SemiController(ISemiService iSemiService)
        {
            _iSemiService = iSemiService;
        }

        [HttpGet("GetPatients")]
        public async Task<IActionResult> GetPatients() {
                Console.WriteLine($"[Request] Método: {Request.Method}, Ruta: {Request.Path}");

                try
                {
                    var patients = await _iSemiService.GetPatients();

                    return Ok(patients);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
        }


        [HttpGet("GetExams")]
        public async Task<IActionResult> GetExams()
        {
            Console.WriteLine($"[Request] Método: {Request.Method}, Ruta: {Request.Path}");

            try
            {
                var exams = await _iSemiService.GetExams();

                return Ok(exams);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            Console.WriteLine($"[Request] Método: {Request.Method}, Ruta: {Request.Path}");

            try
            {
                var orders = await _iSemiService.GetOrders();

                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrdersDTO request)
        {
            Console.WriteLine($"[Request] Método: {Request.Method}, Ruta: {Request.Path}");

            try
            {
                var orderId = await _iSemiService.CreateOrder(request);

                return Ok(new { Message = "Orden creada correctamente", OrderId = orderId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error inesperado" });
            }
        }

        [HttpPost("GetOrdersDetail")]
        public async Task<IActionResult> GetOrdersDetail([FromBody] OrdersDetailDTO request)
        {
            Console.WriteLine($"[Request] Método: {Request.Method}, Ruta: {Request.Path}");

            try
            {
                var ordersDetail = await _iSemiService.GetOrdersDetail(request);

                return Ok(ordersDetail);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error inesperado." });
            }
        }



    }
}
