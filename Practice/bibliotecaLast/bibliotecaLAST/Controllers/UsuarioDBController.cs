using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
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
            if (string.IsNullOrEmpty(name)) // Si no encuentra el nombre, devuelve 404
                return NotFound();

            return Ok(name); // Devuelve el nombre si se encuentra
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _databaseService.DeleteUserByIdAsync(id);
            return NoContent(); // Respuesta 204: El usuario se eliminó correctamente
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Usuarios nuevoUsuario)
        {
            // Llamamos al servicio para crear el usuario
            await _databaseService.CreateUserAsync(nuevoUsuario.Usuario, nuevoUsuario.Nombre, nuevoUsuario.TipoUsuario);

            // Devuelve el estado 201 y la URL donde se puede acceder al usuario recién creado
            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.Usuario }, nuevoUsuario);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Usuarios usuarioActualizado)
        {
            // Verifica que el ID del usuario en la URL coincida con el ID en el cuerpo de la solicitud
            if (id != usuarioActualizado.Usuario)
            {
                return BadRequest("El ID del usuario no coincide.");
            }

            // Llama al servicio para actualizar el usuario
            await _databaseService.UpdateUserAsync(usuarioActualizado.Usuario, usuarioActualizado.Nombre, usuarioActualizado.TipoUsuario);

            // Devuelve una respuesta 204 No Content, indicando que la actualización fue exitosa
            return NoContent();  // NoContent es un código 204, que indica que la actualización fue exitosa, pero no hay contenido para devolver
        }


    }

    public class Usuarios
    {
        public int Usuario { get; set; }
        public string Nombre { get; set; }
        public string TipoUsuario { get; set; }
        
    }




}
