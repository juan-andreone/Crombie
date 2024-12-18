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
                var updateLibroQuery = "UPDATE BookTable SET Disponible = 0 WHERE ID = @LibroID";
                await _sqlConnection.ExecuteAsync(updateLibroQuery, new { LibroID = libroID });

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
                var updateLibroQuery = "UPDATE BookTable SET Disponible = 1 WHERE ID = @LibroID";
                await _sqlConnection.ExecuteAsync(updateLibroQuery, new { LibroID = libroID });

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
        

        public async Task DevolverTodos()
        {
            try
            {
                var queryLibrosPrestados = @"
            SELECT p.LibroID
            FROM PrestamoTable p
            WHERE p.FechaDevolucion IS NULL";

                var librosPrestados = await _sqlConnection.QueryAsync<int>(queryLibrosPrestados);

                if (librosPrestados is not null && librosPrestados.Any())
                {
                    var queryUpdateLibros = @"
                UPDATE BookTable
                SET Disponible = 1
                WHERE ID IN @LibrosPrestados";
                    await _sqlConnection.ExecuteAsync(queryUpdateLibros, new { LibrosPrestados = librosPrestados });

                    var queryUpdatePrestamos = @"
                UPDATE PrestamoTable
                SET FechaDevolucion = @FechaDevolucion
                WHERE LibroID IN @LibrosPrestados AND FechaDevolucion IS NULL";
                    await _sqlConnection.ExecuteAsync(queryUpdatePrestamos, new { FechaDevolucion = DateTime.Now, LibrosPrestados = librosPrestados });
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al devolver los libros prestados: {ex.Message}");
            }
        }

    }
}

