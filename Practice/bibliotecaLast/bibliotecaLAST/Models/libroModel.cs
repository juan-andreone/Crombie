namespace bibliotecaLAST.Models
{
    public class LibroModel
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public bool Disponible { get; set; } = true;
    }
}
