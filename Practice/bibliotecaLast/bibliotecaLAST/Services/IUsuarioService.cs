using bibliotecaLAST.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace bibliotecaLAST.Services
{
    public interface IUsuarioService
    {
        //Task<Usuarios> GetNameByIdAsync(int id);

        Task DeleteUserByIdAsync(int usuario);

        Task CreateUserAsync(int usuario, string nombre, string tipoUsuario);

        Task UpdateUserAsync(int usuario, string nombre, string tipoUsuario);
        Task<Usuarios> GetUsuarioConPrestamosAsync(int id);

        Task RegistrarProfesorAsync(int usuario, string nombre);
        Task RegistrarEstudianteAsync(int usuario, string nombre);

        Task<IEnumerable<Usuarios>> ObtenerEstudiantesAsync();
        Task<IEnumerable<Usuarios>> ObtenerProfesoresAsync();
        Task<IEnumerable<Usuarios>> GetAllUsersAsync();
    }
}