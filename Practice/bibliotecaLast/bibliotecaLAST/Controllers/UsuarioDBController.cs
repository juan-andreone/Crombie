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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _databaseService.DeleteUserByIdAsync(id);
            return NoContent(); 
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Usuarios nuevoUsuario)
        {
            
            await _databaseService.CreateUserAsync(nuevoUsuario.Usuario, nuevoUsuario.Nombre, nuevoUsuario.TipoUsuario);

            
            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.Usuario }, nuevoUsuario);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Usuarios usuarioActualizado)
        {
            
            if (id != usuarioActualizado.Usuario)
            {
                return BadRequest("El ID del usuario no coincide.");
            }

            
            await _databaseService.UpdateUserAsync(usuarioActualizado.Usuario, usuarioActualizado.Nombre, usuarioActualizado.TipoUsuario);

            return NoContent();  
        }


    }

   




}
