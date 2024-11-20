using bibliotecaLAST.Models;
using bibliotecaLAST.Models;

namespace bibliotecaLAST.Services
{
    public class ProfesorService : UsuarioService
    {

        public override bool PrestarMaterial(ProfesorModel profesor, libroModel libro)
        {
            if (profesor.LibrosPrestados.Count < profesor.MaxLibrosPrestados && libro.Disponible)
            {
                profesor.LibrosPrestados.Add(libro);
                libro.Disponible = false;
                return true;
            }
            return false;
        }

    }
}
