using LibraryCrombie.Models;
using System.Collections.Generic;

namespace LibraryCrombie.Services
{
    public class UsuarioService
    {
        private readonly List<usuarioModel> usuarios = new List<usuarioModel>();

        public void RegistrarUsuario(usuarioModel usuario)
        {
            if (!usuarios.Exists(u => u.ID == usuario.ID))
            {
                usuarios.Add(usuario);
            }
        }

        public usuarioModel ObtenerUsuario(string idUsuario)
        {
            return usuarios.Find(u => u.ID == idUsuario);
        }
    }
}
