using Microsoft.AspNetCore.Mvc;
using bibliotecaLAST.Models;
using System.Threading.Tasks;
using bibliotecaLAST.Services.Interfaces;

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
        [HttpPost("Préstamo")]
        public async Task<IActionResult> TomarPrestado([FromQuery] int usuarioID, [FromQuery] int libroID)
        {
            try
            {
                await _prestamoService.TomarPrestadoAsync(usuarioID, libroID);
                return Ok("Libro prestado correctamente.");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

      

        [HttpPost("Devolución")]
        public async Task<IActionResult> DevolverLibro([FromQuery] int libroID)
        {
            await _prestamoService.DevolverLibroAsync(libroID);
            return Ok("Libro devuelto correctamente.");
        }

        [HttpGet("Historial")]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var historial = await _prestamoService.ObtenerHistorialAsync();
            return Ok(historial);
        }

        [HttpDelete("Devolver-Todos")]
        public async Task<IActionResult> EliminarRegistros()
        {
            try
            {
                await _prestamoService.DevolverTodos();
                return Ok("Libros prestados devueltos correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al realizar la operación: {ex.Message}");
            }
        }

    }
}
