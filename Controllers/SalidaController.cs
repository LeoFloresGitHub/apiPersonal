using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioApi.Models;

namespace NegocioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        private readonly MiNegocioBdContext _dbContext;

        public SalidaController(MiNegocioBdContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("")]
        public IActionResult GetSalidas()
        {
            List<Salida> ListaSalidas = new List<Salida>();

            try
            {
                ListaSalidas = _dbContext.Salida.Include(e => e.oProducto).ToList();
                return StatusCode(StatusCodes.Status200OK, ListaSalidas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

        [HttpGet]
        [Route("/api/[controller]/{idSalida}")]
        public IActionResult GetSalidasxID(int idSalida)
        {
            Salida objSalida = _dbContext.Salida.Find(idSalida);
            if (objSalida == null)
            {
                return BadRequest("Salida no encontrado");
            }

            try
            {
                objSalida = _dbContext.Salida.Include(p => p.oProducto).Where(p => p.IdSalida == idSalida).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, objSalida);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

        [HttpPost]
        [Route("/api/[controller]/subir")]
        public IActionResult SubirSalidas(Salida salida)
        {
            if (salida == null)
            {
                return BadRequest("No se encontro registro a subir");
            }

            try
            {
                _dbContext.Salida.Add(salida);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, salida);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("/api/[controller]/actualizar")]
        public IActionResult ActualizarSalidas(Salida salida)
        {
            Salida objSalida = _dbContext.Salida.Find(salida.IdSalida);

            if (objSalida == null)
            {
                return BadRequest("No se encontro registro a actualizar");
            }

            try
            {
                objSalida.IdProducto = salida.IdProducto is null ? objSalida.IdProducto : salida.IdProducto;
                objSalida.Cantidad = salida.Cantidad is null ? objSalida.Cantidad : salida.Cantidad;
                objSalida.Descuento = salida.Descuento is null ? objSalida.Descuento : salida.Descuento;
                objSalida.Fecha = salida.Fecha is null ? objSalida.Fecha : salida.Fecha;
                objSalida.Total = salida.Total is null ? objSalida.Total : salida.Total;
                objSalida.Delivery = salida.Delivery is null ? objSalida.Delivery : salida.Delivery;
                objSalida.Precio=salida.Precio is null ? objSalida.Precio : salida.Precio;

                
                _dbContext.Salida.Update(objSalida);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Salida actualizada con exito" });
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error al guardar los cambios: {innerExceptionMessage}" });

            }


        }

        [HttpDelete]
        [Route("/api/[controller]/eliminar/{idSalida}")]
        public IActionResult EliminarSalida(int idSalida)
        {
            Salida objSalida = _dbContext.Salida.Find(idSalida);

            if (objSalida == null)
            {
                return BadRequest("No se encontro registro a actualizar");
            }

            try
            {


                _dbContext.Salida.Remove(objSalida);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Salida eliminada con exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

    }
}
