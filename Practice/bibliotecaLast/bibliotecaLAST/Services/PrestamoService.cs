using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bibliotecaLAST.Models;
using bibliotecaLAST.Services.Interfaces;

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
                var libroDisponible = await _sqlConnection.QueryFirstOrDefaultAsync<bool>(
                    "SELECT CASE WHEN Disponible = 1 THEN 1 ELSE 0 END FROM BookTable WHERE ID = @LibroID",
                    new { LibroID = libroID }
                );

                if (!libroDisponible)
                {
                    throw new ApplicationException("El libro no está disponible para préstamo.");
                }

                var prestamoActivo = await _sqlConnection.QueryFirstOrDefaultAsync<bool>(
                    "SELECT CASE WHEN COUNT(1) > 0 THEN 1 ELSE 0 END FROM PrestamoTable WHERE LibroID = @LibroID AND FechaDevolucion IS NULL",
                    new { LibroID = libroID }
                );

                if (prestamoActivo)
                {
                    throw new ApplicationException("El libro ya está prestado.");
                }

                var usuario = await _sqlConnection.QueryFirstOrDefaultAsync<Usuarios>(
                    "SELECT TipoUsuario FROM BibliotecaTable WHERE Usuario = @UsuarioID",
                    new { UsuarioID = usuarioID }
                );

                if (usuario == null)
                {
                    throw new ApplicationException("Usuario no encontrado.");
                }

                var prestamosActivos = await _sqlConnection.QueryAsync<Prestamo>(
                    "SELECT * FROM PrestamoTable WHERE UsuarioID = @UsuarioID AND FechaDevolucion IS NULL",
                    new { UsuarioID = usuarioID }
                );

                int limiteLibros = usuario.TipoUsuario == "Estudiante" ? 3 : 5;

                if (prestamosActivos.Count() >= limiteLibros)
                {
                    throw new ApplicationException($"El usuario ha alcanzado el límite máximo de préstamos permitidos: {limiteLibros} libros.");
                }

                var queryPrestamo = "INSERT INTO PrestamoTable (UsuarioID, LibroID, FechaPrestamo) VALUES (@UsuarioID, @LibroID, @FechaPrestamo)";
                await _sqlConnection.ExecuteAsync(queryPrestamo, new { UsuarioID = usuarioID, LibroID = libroID, FechaPrestamo = DateTime.Now });

                var queryLibro = "UPDATE BookTable SET Disponible = 0 WHERE ID = @LibroID";
                await _sqlConnection.ExecuteAsync(queryLibro, new { LibroID = libroID });
            
           
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error inesperado: {ex.Message}");
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

