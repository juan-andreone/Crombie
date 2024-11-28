using bibliotecaLAST.Logging;
using bibliotecaLAST.Middlewares;
using bibliotecaLAST.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<LibroService>()
    .AddScoped<UsuarioService>()
    .AddScoped<ExcelService>();


var logFilePath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory, // Ruta base del proyecto una vez compilado
    "Logs",                               // Nombre de la carpeta
    "api-errors.log"                      // Nombre del archivo
);

Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); // Crear el directorio si no existe

builder.Services.AddSingleton(new FileLogger(logFilePath));






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
