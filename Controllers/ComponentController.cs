using Microsoft.AspNetCore.Mvc;
using ProyectoPractica.Models;
using ProyectoPractica.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _service;

        public ComponentController(IComponentService service)
        {
            _service = service;
        }

        // GET: api/<ComponentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var componentes = await _service.GetAllComponentsAsync();
                if (componentes.Count > 0)
                {
                    return Ok(componentes);
                }
                else
                {
                    return NotFound("No se encontraron componentes.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
            
        }

        // POST api/<ComponentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Componente componente)
        {
            try
            {
                var comp = await _service.SaveComponentAsync(componente);
                if (comp)
                {
                    return Ok("Componente guardado correctamente.");
                }
                else
                {
                    return BadRequest("No se pudo guardar el componente.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT api/<ComponentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Componente componente)
        {
            try
            {
                if(id != componente.Codigo)
                {
                    return BadRequest("ID incorrecto");
                }
                await _service.SaveComponentAsync(componente);
                return Ok("Producto actualizado correctamente");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

        // DELETE api/<ComponentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, string motivoBaja)
        {
            try
            {
                var result = await _service.DeleteComponentAsync(id, motivoBaja);
                if(result)
                {
                    return Ok("Componente dado de baja con exito");
                }
                else
                {
                    return BadRequest("El componente que desea eliminar no existe");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
