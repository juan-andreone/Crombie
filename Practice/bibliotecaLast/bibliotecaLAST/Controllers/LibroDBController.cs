using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bibliotecaLAST.Models;
using bibliotecaLAST.Services;

namespace bibliotecaLAST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroDBController : ControllerBase
    {
        private readonly ILibroDBService _libroDBService;

        public LibroDBController(ILibroDBService libroDBService)
        {
            _libroDBService = libroDBService;
        }



       


        [HttpDelete("Borrar/{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            await _libroDBService.DeleteLibroByIdAsync(id);
            return NoContent();
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CreateLibro([FromBody] Libro nuevoLibro)
        {
            await _libroDBService.CreateLibroAsync(nuevoLibro.ID, nuevoLibro.Nombre_Autor, nuevoLibro.Titulo, nuevoLibro.ISBN, nuevoLibro.Disponible);
            return CreatedAtAction(nameof(GetLibroDetallesById), new { id = nuevoLibro.ID }, nuevoLibro);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> UpdateLibro(int id, [FromBody] Libro libroActualizado)
        {
            if (id != libroActualizado.ID)
            {
                return BadRequest("El ID del libro no coincide.");
            }

            await _libroDBService.UpdateLibroAsync(libroActualizado.ID, libroActualizado.Nombre_Autor, libroActualizado.Titulo, libroActualizado.ISBN, libroActualizado.Disponible);
            return NoContent();
        }



        [HttpGet("VerLibroPorID/{id}")]
        public async Task<IActionResult> GetLibroDetallesById(int id)
        {
            var libro = await _libroDBService.GetLibroByIdAsync(id);

            if (libro == null)
            {
                return NotFound("Libro no encontrado.");
            }

            return Ok(libro);
        }
    


    [HttpGet("VerLista")]
        public async Task<IActionResult> GetAllLibros()
        {
            var libros = await _libroDBService.GetAllLibrosAsync();
            if (libros == null || !libros.Any())
            {
                return NotFound("No se encontraron libros.");
            }

            return Ok(libros);
        }


    }
}