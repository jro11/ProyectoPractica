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
                var lst = await _service.GetWihtFiltersAsync(fecha, estado);
                if (lst.Count > 0)
                {
                    return Ok(lst);
                }
                else
                {
                    return NotFound("No se encontraron ordenes");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error interno del servidor: " + ex.Message);
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
                    if(orden.detalles.Select(d => d.Componente).Count() != orden.detalles.Select(d => d.Componente).Distinct().Count())
                    {
                        return BadRequest("Los componentes en los detalles deben ser únicos.");
                    }
                    if(orden.detalles.Count <2 || orden.detalles ==null)
                    {
                        return BadRequest("La orden debe tener al menos dos componentes.");
                    }
                    var result = await _service.SaveOrderAsync(orden);
                    if (result)
                    {
                        return Ok("Orden guardada exitosamente");
                    }
                    else
                    {
                        return BadRequest("No se pudo guardar la orden");
                    }
                }
                else
                {
                    return BadRequest("La fecha de la orden no puede ser anterior a la fecha actual.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error interno del servidor: " + ex.Message);
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
                if(id <=0)
                {
                    return BadRequest("El ID de la orden debe ser un número positivo.");
                }
                var result = await _service.DeleteOrderAsync(id);
                if (result)
                {
                    return Ok("Orden cancelada exitosamente");
                }
                else
                {
                    return BadRequest("No se pudo cancelar la orden porque no existe o ya esta cancelada/finalizada");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
            
        }
    }
}
