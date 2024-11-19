namespace LibraryCrombie.Models
{
    public abstract class usuarioModel
    {
        public string Nombre { get; set; }
        public string ID { get; set; }
        public List<libroModel> LibrosPrestados { get; set; } = new List<libroModel>();

        public abstract bool PrestarMaterial(libroModel libro);

        public virtual void DevolverMaterial(libroModel libro)
        {
            if (LibrosPrestados.Contains(libro))
            {
                LibrosPrestados.Remove(libro);
                libro.Disponible = true;
            }
        }
    }
}
