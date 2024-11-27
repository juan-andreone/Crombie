using bibliotecaLAST.Models;
using System.Collections.Generic;

namespace bibliotecaLAST.Services
{
    public class UsuarioService
    {
        private static List<UsuarioModel> usuarios = new List<UsuarioModel>();
        
        public bool PrestarMaterial(UsuarioModel usuario, LibroModel libro)
        {
            if (!libro.Disponible)
            {
                return false;
            }

            if (usuario.LibrosPrestados.Count >= usuario.MaxLibrosPrestados)
            {
                return false;
            }

            usuario.LibrosPrestados.Add(libro);
            libro.Disponible = false;

            return true;
        }

        public bool DevolverMaterial(UsuarioModel usuario, LibroModel libro)
        {
           

            if (usuario.LibrosPrestados.Count >= usuario.MaxLibrosPrestados)
            {
                
                return false;
            }

            usuario.LibrosPrestados.Remove(libro);
            libro.Disponible = true;

            return true;
        }

        public bool RegistrarEstudiante(EstudianteModel estudiante)
        {
            if (!usuarios.Exists(u => u.ID == estudiante.ID))
            {
                usuarios.Add(estudiante);
                return true;
            }
            else { return false; }
        }

        public bool RegistrarProfesor(ProfesorModel profesor)
        {
            if (!usuarios.Exists(u => u.ID == profesor.ID))
            {
                usuarios.Add(profesor);
                return true;
            }
            else { return false; }
        }

        public UsuarioModel ObtenerUsuario(string idUsuario)
        {
            return usuarios.Find(u => u.ID == idUsuario);
        }

        public List<UsuarioModel> ObtenerUsuarios()
        {
            return usuarios;
        }

        public bool EliminarUsuario(string idUsuario)
        {
            UsuarioModel usuario = usuarios.FirstOrDefault(u => u.ID == idUsuario); 

            if (usuario != null)
            {
                foreach (var libro in usuario.LibrosPrestados)
                {
                 libro.Disponible = true;
                }
                usuarios.Remove(usuario); 
                return true;
            }
            return false; 
        }



        
    }
}
