namespace bibliotecaLAST.Models
{
    public class Prestamo
    {
        public int ID { get; set; } 
        public int UsuarioID { get; set; } 
        public int LibroID { get; set; } 
        public DateTime FechaPrestamo { get; set; } 
        public DateTime? FechaDevolucion { get; set; }

        public Libro Libro { get; set; }
    }
}
