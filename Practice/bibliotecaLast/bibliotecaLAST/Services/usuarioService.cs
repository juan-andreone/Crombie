using bibliotecaLAST.Models;
using bibliotecaLAST.Models;
using System.Collections.Generic;

namespace bibliotecaLAST.Services
{
    public class UsuarioService
    {
        private readonly List<UsuarioModel> usuarios = new List<UsuarioModel>();
        public virtual bool PrestarMaterial(UsuarioModel usuario, libroModel libro)
        {
            return false;
        }

        public void RegistrarUsuario(UsuarioModel usuario)
        {
            if (!usuarios.Exists(u => u.ID == usuario.ID))
            {
                usuarios.Add(usuario);
            }
        }

        public UsuarioModel ObtenerUsuario(string idUsuario)
        {
            return usuarios.Find(u => u.ID == idUsuario);
        }


        //public virtual void DevolverMaterial(libroModel libro)
        //{
        //    if (LibrosPrestados.Contains(libro))
        //    {
        //        LibrosPrestados.Remove(libro);
        //        libro.Disponible = true;
        //    }
        //}
    }
}
