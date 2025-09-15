using Microsoft.AspNetCore.Mvc;
using ProyectoPractica.Models;
using ProyectoPractica.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<IActionResult> Get(DateOnly? fecha, string? estado)
        {
            try
            {   
                
                var ordenes = await _service.GetAllAsync(fecha, estado);
                if(ordenes.Count >0)
                {
                    return Ok(ordenes);
                }
                else
                {
                    return NotFound("No se encontraron ordenes de produccion.");
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdenesProduccion orden)
        {
            try
            {
                if(ValidarFecha(orden))
                {
                    if(orden.Detalles == null || orden.Detalles.Count < 2)
                    {
                        return BadRequest("El modelo debe tener al menos 2 componentes!");
                    }
                    if (orden.Detalles.Select(o => o.Componente).Count() != orden.Detalles.Select(d => d.Componente).Distinct().Count())
                    {
                        return BadRequest("La orden no puede contener los componentes repetidos");
                    }
                    var result = await _service.SaveAsync(orden);
                    if(result)
                    {
                        return Ok("Orden ingresada con exito");
                    }
                    else
                    {
                        return BadRequest("No se pudo ingresar la orden");
                    }
                }
                else
                {
                    return BadRequest("La fecha no puede ser anterior a la actual");
                }

            }
            catch (Exception ex)
            {

                return StatusCode(500,ex.Message);
            }
        }

        private bool ValidarFecha(OrdenesProduccion orden)
        {
            return orden.Fecha >= DateOnly.FromDateTime(DateTime.Now);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (result)
                {
                    return Ok("Orden dada de baja con exito");
                }
                else
                {
                    return BadRequest("La orden no existe o ya ha sido dada de baja");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
