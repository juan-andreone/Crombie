using bibliotecaLAST.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using static bibliotecaLAST.Services.UsuarioDBService;


namespace bibliotecaLAST.Services
{

    public class UsuarioDBService : IUsuarioDBService
    {
        private readonly SqlConnection _sqlConnection;

        public UsuarioDBService(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            try
            {
                
                var query = "SELECT * FROM BibliotecaTable";
                var result = await _sqlConnection.QueryAsync<string>(query);
                return result;
            }
            catch (Exception ex)
            {
               
                throw new ApplicationException($"Error al consultar la base de datos: {ex.Message}");
            }
        }

       


        public async Task<string> GetNameByIdAsync(int id)
        {
            try
            {
                var query = "SELECT Nombre FROM BibliotecaTable WHERE Usuario = @Id";
                return await _sqlConnection.QueryFirstOrDefaultAsync<string>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al consultar la base de datos: {ex.Message}");
            }
        }

        public async Task DeleteUserByIdAsync(int usuario)
        {
            try
            {
                var query = "DELETE FROM BibliotecaTable WHERE Usuario = @Usuario";
                await _sqlConnection.ExecuteAsync(query, new { Usuario = usuario });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar el usuario: {ex.Message}");
            }
        }

        public async Task CreateUserAsync(int usuario, string nombre, string tipoUsuario)
        {
            try
            {
                var query = "INSERT INTO BibliotecaTable (Usuario, Nombre, TipoUsuario) " +
                            "VALUES (@Usuario, @Nombre, @TipoUsuario)";
                await _sqlConnection.ExecuteAsync(query, new { Usuario = usuario, Nombre = nombre, TipoUsuario = tipoUsuario });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al crear el usuario: {ex.Message}");
            }
        }

        public async Task UpdateUserAsync(int usuario, string nombre, string tipoUsuario)
        {
            try
            {
                var query = "UPDATE BibliotecaTable " +
                            "SET Nombre = @Nombre, TipoUsuario = @TipoUsuario " +
                            "WHERE Usuario = @Usuario";
                await _sqlConnection.ExecuteAsync(query, new { Usuario = usuario, Nombre = nombre, TipoUsuario = tipoUsuario });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al actualizar el usuario: {ex.Message}");
            }
        }

        public async Task<Usuarios> GetUsuarioConPrestamosAsync(int id)
        {
            try
            {
                var queryUsuario = "SELECT * FROM BibliotecaTable WHERE Usuario = @Id";
                var queryPrestamos = @"
            SELECT p.*, l.* 
            FROM PrestamoTable p 
            INNER JOIN BookTable l ON p.LibroID = l.ID
            WHERE p.UsuarioID = @Id AND p.FechaDevolucion IS NULL"
                ;
                
                var usuario = await _sqlConnection.QueryFirstOrDefaultAsync<Usuarios>(queryUsuario, new { Id = id });

                if (usuario != null)
                {
                    var prestamos = await _sqlConnection.QueryAsync<Prestamo, Libro, Prestamo>(
                        queryPrestamos,
                        (prestamo, libro) =>
                        {
                            prestamo.Libro = libro;
                            return prestamo;
                        },
                        new { Id = id },
                        splitOn: "ID"
                    );

                    usuario.Prestamos = prestamos.ToList();
                }

                return usuario;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener el usuario con préstamos: {ex.Message}");
            }
        }
    }
}