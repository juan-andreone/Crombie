using bibliotecaLAST.Models;
using bibliotecaLAST.Services;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaLAST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BibliotecaController : ControllerBase
    {
        private readonly LibroService _libroService;
        private readonly UsuarioService _usuarioService;

        public BibliotecaController()
        {
            _libroService = new LibroService();
            _usuarioService = new UsuarioService();
        }

        //[HttpPost("libros")] 
        //public IActionResult AgregarLibro(LibroModel libro)
        //{
        //    _libroService.AgregarLibro(libro);
        //    return Ok("Libro agregado correctamente.");
        //}

        

        

        //[HttpPost("prestar")]
        //public IActionResult PrestarLibro(string isbn, string idUsuario)
        //{
        //    var usuario = _usuarioService.ObtenerUsuario(idUsuario);
        //    var libro = _libroService.ObtenerLibros().FirstOrDefault(l => l.ISBN == isbn && l.Disponible);
        //    if (usuario != null && libro != null && usuario.PrestarMaterial(libro))
        //    {
        //        return Ok("Libro prestado correctamente.");
        //    }
        //    return BadRequest("Error al prestar libro.");
        //}

        //[HttpPost("devolver")]
        //public IActionResult DevolverLibro(string isbn, string idUsuario)
        //{
        //    var usuario = _usuarioService.ObtenerUsuario(idUsuario);
        //    var libro = _libroService.ObtenerLibros().FirstOrDefault(l => l.ISBN == isbn);
        //    if (usuario != null && libro != null)
        //    {
        //        usuario.DevolverMaterial(libro);
        //        return Ok("Libro devuelto correctamente.");
        //    }
        //    return BadRequest("Error al devolver libro.");
        //}

        //[HttpGet("libros")]
        //public IActionResult VerLibros() => Ok(_libroService.ObtenerLibros());
    }
}
