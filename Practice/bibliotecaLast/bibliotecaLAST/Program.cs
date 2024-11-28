using bibliotecaLAST.Logging;
using bibliotecaLAST.Middlewares;
using bibliotecaLAST.Services;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<LibroService>()
    .AddScoped<UsuarioService>()
    .AddScoped<ExcelService>();


var logFilePath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory, // Ruta base del proyecto una vez compilado
    "Logs",                               // Nombre de la carpeta
    "logs.log"                      // Nombre del archivo
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
