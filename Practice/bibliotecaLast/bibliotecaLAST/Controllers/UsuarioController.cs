using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bibliotecaLAST.Models;
using bibliotecaLAST.Services.Interfaces;


namespace bibliotecaLAST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _databaseService;

        public UsuarioController(IUsuarioService databaseService)
        {
            _databaseService = databaseService;
        }


        [HttpDelete("Borrar/{id}")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            await _databaseService.DeleteUserByIdAsync(id);
            return NoContent(); 
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

        [HttpGet("VerUsuarioPorID/{id}")]
        public async Task<IActionResult> GetUsuarioConPrestamos(int id)
        {
            var usuario = await _databaseService.GetUsuarioConPrestamosAsync(id);

            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            return Ok(usuario);
        }


        [HttpGet("VerLista")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usuarios = await _databaseService.GetAllUsersAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("No se encontraron usuarios.");
            }

            return Ok(usuarios);
        }


    }






}
