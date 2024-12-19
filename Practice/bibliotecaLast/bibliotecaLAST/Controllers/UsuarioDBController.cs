using Microsoft.AspNetCore.Mvc;
using bibliotecaLAST.Services;
using System.Threading.Tasks;
using bibliotecaLAST.Models;


namespace bibliotecaLAST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioDBController : ControllerBase
    {
        private readonly IUsuarioDBService _databaseService;

        public UsuarioDBController(IUsuarioDBService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNames()
        {
            var names = await _databaseService.GetNamesAsync();
            return Ok(names);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var name = await _databaseService.GetNameByIdAsync(id);
            if (string.IsNullOrEmpty(name))  
                return NotFound();

            return Ok(name); 
        }


        [HttpDelete("Borrar/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _databaseService.DeleteUserByIdAsync(id);
            return NoContent(); 
        }



        [HttpPost("Crear")]
        public async Task<IActionResult> CreateUser([FromBody] Usuarios nuevoUsuario)
        {
            
            await _databaseService.CreateUserAsync(nuevoUsuario.Usuario, nuevoUsuario.Nombre, nuevoUsuario.TipoUsuario);

            
            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.Usuario }, nuevoUsuario);
        }


        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Usuarios usuarioActualizado)
        {
            
            if (id != usuarioActualizado.Usuario)
            {
                return BadRequest("El ID del usuario no coincide.");
            }

            
            await _databaseService.UpdateUserAsync(usuarioActualizado.Usuario, usuarioActualizado.Nombre, usuarioActualizado.TipoUsuario);

            return NoContent();  
        }

        [HttpGet("VerPrestamosDeUsuario/{id}")]
        public async Task<IActionResult> GetUsuarioConPrestamos(int id)
        {
            var usuario = await _databaseService.GetUsuarioConPrestamosAsync(id);

            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            return Ok(usuario);
        }
        [HttpPost("CrearProfesor")]
        public async Task<IActionResult> CrearProfesor([FromBody] ProfesorModel nuevoProfesor)
        {
            if (nuevoProfesor == null) return BadRequest("El usuario no puede ser nulo.");

            await _databaseService.CreateUserAsync(nuevoProfesor.Usuario, nuevoProfesor.Nombre, "Profesor");

            return CreatedAtAction(nameof(GetById), new { id = nuevoProfesor.Usuario }, nuevoProfesor);
        }

        [HttpPost("CrearEstudiante")]
        public async Task<IActionResult> CrearEstudiante([FromBody] EstudianteModel nuevoEstudiante)
        {
            if (nuevoEstudiante == null) return BadRequest("El usuario no puede ser nulo.");

            await _databaseService.CreateUserAsync(nuevoEstudiante.Usuario, nuevoEstudiante.Nombre, "Estudiante");

            return CreatedAtAction(nameof(GetById), new { id = nuevoEstudiante.Usuario }, nuevoEstudiante);
        }
        [HttpGet("Estudiantes")]
        public async Task<IActionResult> ObtenerEstudiantes()
        {
            try
            {
                var estudiantes = await _databaseService.ObtenerEstudiantesAsync();
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los estudiantes: {ex.Message}");
            }
        }

        [HttpGet("Profesores")]
        public async Task<IActionResult> ObtenerProfesores()
        {
            try
            {
                var profesores = await _databaseService.ObtenerProfesoresAsync();
                return Ok(profesores);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los profesores: {ex.Message}");
            }
        }


    }






}
