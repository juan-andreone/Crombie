using bibliotecaLAST.Models;
using System.Collections.Generic;




namespace bibliotecaLAST.Services
{
    public class LibroService
    {
        private static List<LibroModel> _libros = new List<LibroModel>();
        
        

        public bool AgregarLibro(LibroModel libro)
        {
            bool duplicated = _libros.Exists(l => l.ISBN == libro.ISBN);

            if (!duplicated)
            {
                _libros.Add(libro);
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
        public bool EliminarLibro(string isbn)
        {
            LibroModel libro = _libros.FirstOrDefault(u => u.ISBN == isbn);

            if (libro != null)
            {
                _libros.Remove(libro);
                return true;
            }
            return false;
        }

        

    }
}
