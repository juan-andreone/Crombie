﻿using System.Collections.Generic;
using System.Threading.Tasks;
namespace bibliotecaLAST.Services
{
    public interface IUsuarioDBService
    {
        Task<IEnumerable<string>> GetNamesAsync();
        Task<string> GetNameByIdAsync(int id);

        Task DeleteUserByIdAsync(int usuario);

        Task CreateUserAsync(int usuario, string nombre, string tipoUsuario);

        Task UpdateUserAsync(int usuario, string nombre, string tipoUsuario);
    }
}