﻿using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bibliotecaLAST
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public List<object> GetEntities(string query)
        {
            using var connection = CreateConnection();
            {
                var sql = query;

                var response = connection.Query<object>(sql).ToList();

                return response;
            }
        }

            
        
    }
}



