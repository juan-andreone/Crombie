using bibliotecaLAST.Models;

namespace bibliotecaLAST.Services
{
    public class EstudianteService : UsuarioService
    {
        // public override bool PrestarMaterial(UsuarioModel estudiante, LibroModel libro)
        // {
        //     if (!libro.Disponible)
        //     {
        //         return false;
        //     }

        //     if (estudiante.LibrosPrestados.Count >= estudiante.MaxLibrosPrestados)
        //     {
        //         return false;
        //     }

        //     estudiante.LibrosPrestados.Add(libro);
        //     libro.Disponible = false;

        //     return true;
        // }
    }
}
