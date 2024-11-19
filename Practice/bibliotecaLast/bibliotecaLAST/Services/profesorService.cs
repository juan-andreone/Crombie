using LibraryCrombie.Models;

namespace LibraryCrombie.Services
{
    public class ProfesorService : UsuarioService
    {
        public override bool PrestarMaterial(libroModel libro)
        {
            if (LibrosPrestados.Count < MaxLibrosPrestados && libro.Disponible)
            {
                LibrosPrestados.Add(libro);
                libro.Disponible = false;
                return true;
            }
            return false;

        }
    }
}

