using bibliotecaLAST.Models;
using bibliotecaLAST.Models;

namespace bibliotecaLAST.Services
{
    public class ProfesorService : UsuarioService
    {

        // public override bool PrestarMaterial(UsuarioModel profesor, LibroModel libro)
        // {
        //     if (!libro.Disponible)
        //     {
        //         return false;
        //     }

        //     if (profesor.LibrosPrestados.Count >= profesor.MaxLibrosPrestados)
        //     {
        //         return false;
        //     }

        //     profesor.LibrosPrestados.Add(libro);
        //     libro.Disponible = false;

        //     return true;
        // }

    }
}
