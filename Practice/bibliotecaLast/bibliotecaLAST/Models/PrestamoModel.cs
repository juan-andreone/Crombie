namespace bibliotecaLAST.Models
{
    public class Prestamo
    {
        public int ID { get; set; } // ID del registro del préstamo
        public int UsuarioID { get; set; } // Usuario que tomó el libro
        public int LibroID { get; set; } // Libro tomado
        public DateTime FechaPrestamo { get; set; } // Fecha del préstamo
        public DateTime? FechaDevolucion { get; set; } // Fecha de devolución (nullable)
    }
}
