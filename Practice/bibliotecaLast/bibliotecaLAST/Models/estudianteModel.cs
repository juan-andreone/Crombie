namespace LibraryCrombie.Models
{
    public class estudianteModel : usuarioModel
    {
        private const int MaxLibrosPrestados = 3;

        public override bool PrestarMaterial(libroModel libro)
        {
            if (LibrosPrestados.Count < MaxLibrosPrestados && libro.Disponible)
            {
                LibrosPrestados.Add(libro);
                libro.Disponible = false;
                return true;
            }
            return false;
        }
    }
}
