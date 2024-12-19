﻿using bibliotecaLAST.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bibliotecaLAST.Models;


namespace bibliotecaLAST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IUsuarioDBService _usuarioService;

        public ProfesorController(IUsuarioDBService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> ObtenerProfesores()
        {
            try
            {
                var profesores = await _usuarioService.ObtenerProfesoresAsync();
                return Ok(profesores);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los profesores: {ex.Message}");
            }
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CrearProfesor([FromBody] ProfesorModel nuevoProfesor)
        {
            if (nuevoProfesor == null) return BadRequest("El usuario no puede ser nulo.");

            await _usuarioService.CreateUserAsync(nuevoProfesor.Usuario, nuevoProfesor.Nombre, "Profesor");

            return CreatedAtAction(nameof(ObtenerProfesores), new { id = nuevoProfesor.Usuario }, nuevoProfesor);
        }
    }
}