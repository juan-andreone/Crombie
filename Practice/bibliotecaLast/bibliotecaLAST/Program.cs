using bibliotecaLAST.Logging;
using bibliotecaLAST.Middlewares;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using bibliotecaLAST.Services;
using bibliotecaLAST;
using bibliotecaLAST.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();
builder.Services.AddScoped<ILibroService, LibroService>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var logFilePath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory, // Ruta base del proyecto una vez compilado
    "Logs",                               // Nombre de la carpeta
    "logs.log"                      // Nombre del archivo
);

Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); // Crear el directorio si no existe

builder.Services.AddSingleton(new FileLogger(logFilePath));


builder.Services.AddSingleton<SqlConnection>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<ILibroService, LibroService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorLoggingMiddleware>();

app.MapControllers();

app.Run();
