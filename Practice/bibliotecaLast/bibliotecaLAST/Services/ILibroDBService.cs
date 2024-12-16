﻿using bibliotecaLAST.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bibliotecaLAST.Services
{
    public interface ILibroDBService
    {
        Task<IEnumerable<string>> GetNombresAsync();
        Task<string> GetNombreByIdAsync(int id);

        Task DeleteLibroByIdAsync(int id);

        Task CreateLibroAsync(int id, string nombre, string titulo, string isbn, bool disponible);

        Task UpdateLibroAsync(int id, string nombre, string titulo, string isbn, bool disponible);
        Task<Libro> GetLibroByIdAsync(int id);
    }
}
