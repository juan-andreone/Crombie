using bibliotecaLAST.Models;

namespace bibliotecaLAST.Models
{
    public class UsuarioModel
    {
        public string Nombre { get; set; }
        public string ID { get; set; }
        public List<libroModel> LibrosPrestados { get; set; } = new List<libroModel>();

    }
}
