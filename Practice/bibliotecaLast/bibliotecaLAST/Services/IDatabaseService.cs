using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebApplication1.Services
{
    public interface IDatabaseService
    {
        Task<IEnumerable<string>> GetNamesAsync();
        Task<string> GetNameByIdAsync(int id);

        Task DeleteUserByIdAsync(int usuario);

        Task CreateUserAsync(int usuario, string nombre, string tipoUsuario);

        Task UpdateUserAsync(int usuario, string nombre, string tipoUsuario);
    }
}