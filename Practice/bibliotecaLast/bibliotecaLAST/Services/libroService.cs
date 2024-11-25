using bibliotecaLAST.Models;

namespace LibraryCrombie.Services
{
    public class LibroService
    {
        private readonly List<libroModel> libros = new List<libroModel>();

        public void AgregarLibro(libroModel libro)
        {
            if (!libros.Exists(l => l.ISBN == libro.ISBN))
            {
                libros.Add(libro);
            }
        }

        public List<libroModel> ObtenerLibros()
        {
            return libros;
        }
    }
}
