namespace bibliotecaLAST.Models
{
    public class Libro
    {
        public int ID { get; set; }
        public string Nombre_Autor { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public bool Disponible { get; set; }
    }
}
