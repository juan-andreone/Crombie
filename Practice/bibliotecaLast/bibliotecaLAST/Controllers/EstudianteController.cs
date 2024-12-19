using bibliotecaLAST.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bibliotecaLAST.Models;


namespace bibliotecaLAST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IUsuarioDBService _usuarioService;

        public EstudianteController(IUsuarioDBService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> ObtenerEstudiantes()
        {
            try
            {
                var estudiantes = await _usuarioService.ObtenerEstudiantesAsync();
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los estudiantes: {ex.Message}");
            }
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CrearEstudiante([FromBody] EstudianteModel nuevoEstudiante)
        {
            if (nuevoEstudiante == null) return BadRequest("El usuario no puede ser nulo.");

            await _usuarioService.CreateUserAsync(nuevoEstudiante.Usuario, nuevoEstudiante.Nombre, "Estudiante");

            return CreatedAtAction(nameof(ObtenerEstudiantes), new { id = nuevoEstudiante.Usuario }, nuevoEstudiante);
        }
    }
}
