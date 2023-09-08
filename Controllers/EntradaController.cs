using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioApi.Models;

namespace NegocioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntradaController : ControllerBase
    {
        private readonly MiNegocioBdContext _dbContext;

        public EntradaController(MiNegocioBdContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("")]
        public IActionResult GetEntradas()
        {
            List<Entrada> ListaEntradas = new List<Entrada>();

            try
            {
                ListaEntradas = _dbContext.Entradas.Include(e=>e.oProducto).ToList();
                return StatusCode(StatusCodes.Status200OK, ListaEntradas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }


        }

        [HttpGet]
        [Route("/api/[controller]/{idEntrada}")]
        public IActionResult GetEntradasxID(int idEntrada)
        {
            Entrada objEntrada = _dbContext.Entradas.Find(idEntrada);
            if (objEntrada == null)
            {
                return BadRequest("Entrada no encontrado");
            }

            try
            {
                objEntrada = _dbContext.Entradas.Include(p => p.oProducto).Where(p => p.IdEntrada == idEntrada).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, objEntrada);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

        [HttpPost]
        [Route("/api/[controller]/subir")]
        public IActionResult SubirEntradas(Entrada entrada)
        {
            if (entrada == null)
            {
                return BadRequest("No se encontro registro a subir");
            }

            try
            {
                _dbContext.Entradas.Add(entrada);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, entrada);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("/api/[controller]/actualizar")]
        public IActionResult ActualizarEntradas(Entrada entrada)
        {
            Entrada objEntrada = _dbContext.Entradas.Find(entrada.IdEntrada);

            if (objEntrada == null)
            {
                return BadRequest("No se encontro registro a actualizar");
            }

            try
            {
                objEntrada.IdProducto = entrada.IdProducto is null ? objEntrada.IdProducto: entrada.IdProducto;
                objEntrada.Cantidad = entrada.Cantidad is null ? objEntrada.Cantidad : entrada.Cantidad;
                objEntrada.Costo = entrada.Costo is null ? objEntrada.Costo : entrada.Costo;
                objEntrada.Fecha = entrada.Fecha is null ? objEntrada.Fecha : entrada.Fecha;

                _dbContext.Entradas.Update(objEntrada);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Entrada actualizada con exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

        [HttpDelete]
        [Route("/api/[controller]/eliminar/{idEntrada}")]
        public IActionResult EliminarEntradas(int idEntrada)
        {
            Entrada objEntrada = _dbContext.Entradas.Find(idEntrada);

            if (objEntrada == null)
            {
                return BadRequest("No se encontro registro a actualizar");
            }

            try
            {
                

                _dbContext.Entradas.Remove(objEntrada);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Entrada eliminada con exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

    }
}
