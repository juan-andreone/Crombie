using bibliotecaLAST.Services;
using bibliotecaLAST.Models;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaLAST.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ExcelService _excelService;
        // Especifica la ruta relativa del archivo Excel
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "BibliotecaBaseDatos.xlsx");

        public ExcelController(ExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> ObtenerEncabezados()
        {
            try
            {
                return Ok(_excelService.ObtenerEncabezados(filePath));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los encabezados: {ex.Message}");
            }
        }

        [HttpGet("datos")]
        public ActionResult<List<ExcelModel>> GetDatos()
        {
            try
            {
                return Ok(_excelService.ObtenerDatos(filePath));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los datos: {ex.Message}");
            }
        }

        [HttpPost("insertar")]
        public ActionResult InsertarDatos([FromBody] List<ExcelModel> nuevosDatos)
        {
            try
            {
                _excelService.InsertarDatos(filePath, nuevosDatos);
                return Ok("Datos insertados exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar datos: {ex.Message}");
            }
        }
        
        [HttpPut("actualizar")]
        public ActionResult ActualizarDatos([FromBody] ExcelModel datosActualizados)
        {
            try
            {
                _excelService.ActualizarDatosPorId(filePath, datosActualizados);
                return Ok("Datos actualizados exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar datos: {ex.Message}");
            }
        }


    }

}
