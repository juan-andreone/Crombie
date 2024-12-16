using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bibliotecaLAST.Models;

namespace bibliotecaLAST.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly SqlConnection _sqlConnection;

        public PrestamoService(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task TomarPrestadoAsync(int usuarioID, int libroID)
        {
            try
            {
                // Cambiar estado del libro a no disponible
                var updateLibroQuery = "UPDATE BookTable SET Disponible = 0 WHERE ID = @LibroID";
                await _sqlConnection.ExecuteAsync(updateLibroQuery, new { LibroID = libroID });

                // Registrar el préstamo en el historial
                var insertPrestamoQuery = "INSERT INTO PrestamoTable (UsuarioID, LibroID, FechaPrestamo) VALUES (@UsuarioID, @LibroID, @FechaPrestamo)";
                await _sqlConnection.ExecuteAsync(insertPrestamoQuery, new { UsuarioID = usuarioID, LibroID = libroID, FechaPrestamo = DateTime.Now });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al registrar el préstamo: {ex.Message}");
            }
        }

        public async Task DevolverLibroAsync(int libroID)
        {
            try
            {
                // Cambiar estado del libro a disponible
                var updateLibroQuery = "UPDATE BookTable SET Disponible = 1 WHERE ID = @LibroID";
                await _sqlConnection.ExecuteAsync(updateLibroQuery, new { LibroID = libroID });

                // Actualizar la fecha de devolución en el historial
                var updatePrestamoQuery = "UPDATE PrestamoTable SET FechaDevolucion = @FechaDevolucion WHERE LibroID = @LibroID AND FechaDevolucion IS NULL";
                await _sqlConnection.ExecuteAsync(updatePrestamoQuery, new { FechaDevolucion = DateTime.Now, LibroID = libroID });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al devolver el libro: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Prestamo>> ObtenerHistorialAsync()
        {
            try
            {
                var query = "SELECT * FROM PrestamoTable";
                var result = await _sqlConnection.QueryAsync<Prestamo>(query);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener el historial: {ex.Message}");
            }
        }
    }
}
