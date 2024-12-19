using System.Threading.Tasks;
using System.Collections.Generic;
using bibliotecaLAST.Models;

namespace bibliotecaLAST.Services.Interfaces
{
    public interface IPrestamoService
    {
        Task TomarPrestadoAsync(int usuarioID, int libroID);
        Task DevolverLibroAsync(int libroID);
        Task<IEnumerable<Prestamo>> ObtenerHistorialAsync();
        Task DevolverTodos();

    }
}
