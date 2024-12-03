using bibliotecaLAST.Models;
using bibliotecaLAST.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaLAST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController()
        {
            _usuarioService = new UsuarioService();
        }

        [HttpGet]
        public IActionResult GetUser(string IdUsuario)
        {
            UsuarioModel User = _usuarioService.ObtenerUsuario(IdUsuario);

            return Ok(User);
        }

        



        [HttpGet("Todos")]
        public IActionResult GetAllUsers()
        {
            List<UsuarioModel> allUsers = _usuarioService.ObtenerUsuarios();

            return Ok(allUsers);
        }



        [HttpPost("Usuario/Estudiante")]
        public IActionResult RegistrarEstudiante(EstudianteModel estudiante)
        {
            _usuarioService.RegistrarEstudiante(estudiante);
            return Ok("Estudiante registrado correctamente.");
        }

        [HttpPost("Usuario/Profesor")]
        public IActionResult RegistrarProfesor(ProfesorModel profesor)
        {
            _usuarioService.RegistrarProfesor(profesor);
            return Ok("Profesor registrado correctamente.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            bool resultado = _usuarioService.EliminarUsuario(id);

            if (resultado)
            {
                return Ok($"Usuario con ID {id} eliminado correctamente.");
            }
            else
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }
        }



        



    }
}
