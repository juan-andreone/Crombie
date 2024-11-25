using bibliotecaLAST.Models;
using System.Collections.Generic;




namespace bibliotecaLAST.Services
{
    public class LibroService
    {
        private List<LibroModel> _libros;
        
        public LibroService() {
            _libros = new List<LibroModel>();

        }

        public bool AgregarLibro(LibroModel libro)
        {
            bool duplicated = _libros.Exists(l => l.ISBN == libro.ISBN);

            if (!duplicated)
            {
                this._libros.Add(libro);
                Console.WriteLine(_libros.Count);
                Console.WriteLine("Libro agregado", libro);
                return true;
            }


            return false;
        }

        public List<LibroModel> ObtenerLibros()
        {
            return _libros;
        }

        public LibroModel? ObtenerLibroIndividual(string isbn)
        {
            return _libros.Find(l => l.ISBN == isbn);
        }
    }
}
