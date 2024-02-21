using Microsoft.AspNetCore.Mvc;
using CerveceriaAPI.Models;
using CerveceriaAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CerveceriaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CervezaController : ControllerBase
    {
        private readonly ICervezaService _cervezaService;

        public CervezaController(ICervezaService cervezaService)
        {
            _cervezaService = cervezaService ?? throw new ArgumentNullException(nameof(cervezaService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cerveza>>> Get(string nombre = "", int idMarca = 0)
        {
            var cervezas = await _cervezaService.GetCerveza(nombre, idMarca);
            if (cervezas == null)
            {
                return NotFound();
            }
            return Ok(cervezas);
        }

        [HttpPost]
        public async Task<ActionResult<Cerveza>> Post(Cerveza cerveza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _cervezaService.SaveCerveza(cerveza);
            if (!success)
            {
                return StatusCode(500, "Error al guardar cerveza");
            }

            return CreatedAtAction(nameof(Get), new { id = cerveza.Id }, cerveza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cerveza cerveza)
        {
            if (id != cerveza.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _cervezaService.UpdateCerveza(cerveza);
            if (!success)
            {
                return StatusCode(500, "Error al actualizar cerveza");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _cervezaService.DeleteCerveza(id);
            if (!success)
            {
                return StatusCode(500, "Error al eliminar cerveza");
            }

            return NoContent();
        }
    }
}
