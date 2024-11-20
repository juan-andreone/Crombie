using bibliotecaLAST.Models;

namespace bibliotecaLAST.Services
{
    public class EstudianteService : UsuarioService
    {
        public override bool PrestarMaterial(estudianteModel estudiante, libroModel libro)
        {

            if (estudiante.LibrosPrestados.Count < estudiante.MaxLibrosPrestados && libro.Disponible)
            {
                estudiante.LibrosPrestados.Add(libro);
                libro.Disponible = false;
                return true;
            }
            return false;
        }
    }
}
