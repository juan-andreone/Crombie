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
        private readonly UsuarioService _usuarioService;

        public LibrosController()
        {
            _libroService = new LibroService();
            _usuarioService = new UsuarioService();
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
            }
            else
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
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{isbn}")]
        public IActionResult DeleteBook(string id)
        {
            bool resultado = _libroService.EliminarLibro(id);

            if (resultado)
            {
                return Ok($"Libro con ID {id} eliminado correctamente.");
            }
            else
            {
                return NotFound($"Libro con ID {id} no encontrado.");
            }
        }


        [HttpPost("prestar")]
        public IActionResult PrestarLibro(string isbn, string idUsuario)
        {
            var usuario = _usuarioService.ObtenerUsuario(idUsuario);
            var libro = _libroService.ObtenerLibros().FirstOrDefault(l => l.ISBN == isbn && l.Disponible);
            if (usuario != null && libro != null)
            {
                var prestado = _usuarioService.PrestarMaterial(usuario, libro);
                if (prestado)
                {
                    return Ok("Libro prestado correctamente.");


                }
                else
                {
                    return BadRequest("Maximo excedido.");
                }

            }
            return BadRequest("Error al prestar libro.");
        }

        [HttpPost("devolver")]
        public IActionResult DevolverLibro(string isbn, string idUsuario)
        {
            var usuario = _usuarioService.ObtenerUsuario(idUsuario);
            var libro = _libroService.ObtenerLibros().FirstOrDefault(l => l.ISBN == isbn);
            if (usuario != null && libro != null)
            {
                _usuarioService.DevolverMaterial(usuario, libro);
                return Ok("Libro devuelto correctamente.");
            }
            return BadRequest("Error al devolver libro.");
        }


        //[HttpGet("libros")] ESTA REPETIDO
        //public IActionResult VerLibros() => Ok(_libroService.ObtenerLibros());
    }
}
