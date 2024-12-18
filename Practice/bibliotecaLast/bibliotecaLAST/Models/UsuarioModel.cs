namespace bibliotecaLAST.Models
{
    public class Usuarios
    {
        public int Usuario { get; set; }
        public string Nombre { get; set; }
        public string TipoUsuario { get; set; }

        public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
