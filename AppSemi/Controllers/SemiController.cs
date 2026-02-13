using AppSemi.Application.Interfaces;
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



    }
}
