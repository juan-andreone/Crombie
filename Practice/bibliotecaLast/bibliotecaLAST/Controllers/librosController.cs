using bibliotecaLAST.Models;
using bibliotecaLAST.Services;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaLAST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly LibroService _libroService;

        public LibrosController()
        {
            _libroService = new LibroService();
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            List<LibroModel> allBooks = _libroService.ObtenerLibros();

            return Ok(allBooks);
        }

        [HttpPost()]
        public IActionResult AgregarLibro(LibroModel libro)
        {
            bool resultado = _libroService.AgregarLibro(libro);

            if (resultado)
            {
                return Ok(libro);
            } else
            {
                return BadRequest("Error al agregar libro. Libro duplicado.");
            }
        }

        [HttpGet("{isbn}")]
        public IActionResult GetSingleBook(string isbn)
        {
            LibroModel? book = _libroService.ObtenerLibroIndividual(isbn);

            if (book != null)
            {
                return Ok(book);
            } else
            {
                return NotFound();
            }
        }

        // [HttpPost("usuarios/estudiante")]
        // public IActionResult RegistrarEstudiante(estudianteModel estudiante)
        // {
        //     _usuarioService.RegistrarUsuario(estudiante);
        //     return Ok("Estudiante registrado correctamente.");
        // }

        // [HttpPost("usuarios/profesor")]
        // public IActionResult RegistrarProfesor(ProfesorModel profesor)
        // {
        //     _usuarioService.RegistrarUsuario(profesor);
        //     return Ok("Profesor registrado correctamente.");
        // }

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

        // [HttpGet("libros")]
        // public IActionResult VerLibros() => Ok(_libroService.ObtenerLibros());
    }
}
