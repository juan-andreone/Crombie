using bibliotecaLAST.Models;

namespace bibliotecaLAST.Models
{
    public class EstudianteModel : UsuarioModel
    {
        public override int MaxLibrosPrestados => 3;
    }
}
