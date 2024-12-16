using Microsoft.AspNetCore.Mvc;
using bibliotecaLAST.Models;
using bibliotecaLAST.Services;
using System.Threading.Tasks;

namespace bibliotecaLAST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpPost("tomar")]
        public async Task<IActionResult> TomarPrestado([FromQuery] int usuarioID, [FromQuery] int libroID)
        {
            await _prestamoService.TomarPrestadoAsync(usuarioID, libroID);
            return Ok("El libro ha sido tomado prestado.");
        }

        [HttpPost("devolver")]
        public async Task<IActionResult> DevolverLibro([FromQuery] int libroID)
        {
            await _prestamoService.DevolverLibroAsync(libroID);
            return Ok("El libro ha sido devuelto.");
        }

        [HttpGet("historial")]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var historial = await _prestamoService.ObtenerHistorialAsync();
            return Ok(historial);
        }
    }
}
