using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetNombres()
        {
            var nombres = await _libroDBService.GetNombresAsync();
            return Ok(nombres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var nombre = await _libroDBService.GetNombreByIdAsync(id);
            if (string.IsNullOrEmpty(nombre))
                return NotFound();

            return Ok(nombre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            await _libroDBService.DeleteLibroByIdAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLibro([FromBody] Libro nuevoLibro)
        {
            await _libroDBService.CreateLibroAsync(nuevoLibro.ID, nuevoLibro.Nombre_Autor, nuevoLibro.Titulo, nuevoLibro.ISBN, nuevoLibro.Disponible);
            return CreatedAtAction(nameof(GetById), new { id = nuevoLibro.ID }, nuevoLibro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibro(int id, [FromBody] Libro libroActualizado)
        {
            if (id != libroActualizado.ID)
            {
                return BadRequest("El ID del libro no coincide.");
            }

            await _libroDBService.UpdateLibroAsync(libroActualizado.ID, libroActualizado.Nombre_Autor, libroActualizado.Titulo, libroActualizado.ISBN, libroActualizado.Disponible);
            return NoContent();
        }


        

    }

    public class Libro
    {
        public int ID { get; set; }
        public string Nombre_Autor { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public bool Disponible { get; set; }
    }
}
