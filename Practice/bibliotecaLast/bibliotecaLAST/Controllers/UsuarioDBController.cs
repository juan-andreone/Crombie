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
        

        
        

    }






}
