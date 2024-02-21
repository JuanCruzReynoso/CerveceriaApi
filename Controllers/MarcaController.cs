using Microsoft.AspNetCore.Mvc;
using CerveceriaAPI.Models;
using CerveceriaAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CerveceriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService ?? throw new ArgumentNullException(nameof(marcaService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marca>>> Get()
        {
            var marcas = await _marcaService.GetMarca();
            if (marcas == null)
            {
                return NotFound();
            }
            return Ok(marcas);
        }

        [HttpPost]
        public async Task<ActionResult<Marca>> Post([FromBody] Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _marcaService.SaveMarca(marca);
            if (!success)
            {
                return StatusCode(500, "Error al guardar marca.");
            }

            return CreatedAtAction(nameof(Get), new { id = marca.Id }, marca);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Marca marca)
        {
            if (id != marca.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _marcaService.UpdateMarca(marca);
            if (!success)
            {
                return StatusCode(500, "Error al actualizar marca.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _marcaService.DeleteMarca(id);
            if (!success)
            {
                return StatusCode(500, "Error al eliminar marca.");
            }

            return NoContent();
        }
    }
}
