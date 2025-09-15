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
                var lst = await _service.GetComponentsAsyinc();
                if (lst.Count > 0)
                {
                    return Ok(lst);
                }
                else
                {
                    return NotFound("No se encontraron componentes");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error al obtener los componentes: " + ex.Message);
            }
        }

        // POST api/<ComponentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Componente componente )
        {
            try
            {
                if(componente != null)
                {
                    var result = await _service.SaveComponentAsync(componente);
                    if (result)
                    {
                        return Ok("Componente guardado correctamente");
                    }
                    else
                    {
                        return StatusCode(500, "Error al guardar el componente");
                    }
                }
                else
                {
                    return BadRequest("Error al traer el componente");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error al guardar el componente (servidor): " + ex.Message);
            }
        }

        // PUT api/<ComponentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Componente componente)
        {
            try
            {
                if (componente.Codigo != id)
                {
                    return BadRequest("El id del componente no coincide con el id de la URL");
                }
                var result = await _service.SaveComponentAsync(componente);
                if (result)
                {
                    return Ok("Componente actualizado correctamente");
                }
                else
                {
                    return StatusCode(500, "Error al actualizar el componente");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error al actualizar el componente (servidor): " + ex.Message);
            }
        }

        // DELETE api/<ComponentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, string motivoBaja)
        {
            try
            {
                if (id > 0 && motivoBaja != string.Empty)
                {
                    var result = await _service.DeleteComponentAsync(id, motivoBaja);
                    if (result)
                    {
                        return Ok("Componente eliminado correctamente");
                    }
                    else
                    {
                        return StatusCode(500, "Error al eliminar el componente (no existe?)");
                    }
                }
                else
                {
                    return BadRequest("El id del componente no puede ser 0 y el motivo de baja no puede estar vacio");
                }

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error al eliminar el componente (servidor): " + ex.Message);
            }
            

        }
    }
}
