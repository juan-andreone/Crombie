using bibliotecaLAST.Models;

namespace bibliotecaLAST.Models
{
    public abstract class UsuarioModel
    {
        public string Nombre { get; set; }
        public string ID { get; set; }
        public List<LibroModel> LibrosPrestados { get; set; } = new List<LibroModel>();

        public abstract int MaxLibrosPrestados { get;  }
    }
}
