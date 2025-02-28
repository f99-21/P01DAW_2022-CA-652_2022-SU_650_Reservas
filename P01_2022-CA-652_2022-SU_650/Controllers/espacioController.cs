﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01_2022_CA_652_2022_SU_650.Models;

namespace P01_2022_CA_652_2022_SU_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class espacioController : ControllerBase
    {
        private readonly reservaContext _espacioContext;

        public espacioController(reservaContext espacioContext)
        { 
            _espacioContext  = espacioContext;
        
        }

        [HttpGet]
        [Route("Add")]
        public ActionResult agregar ([FromBody]espacio espacios)
        {
            try
            {
                _espacioContext.espacios.Add(espacios);
                _espacioContext.SaveChanges();
                return Ok(espacios);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// EndPoint para mostrar una lista de espacios de parqueo disponibles para reservar por día
        /// </summary>
        /// <param name="fecha"></param>
        [HttpGet]
        [Route("ListaDisponibles")]
        public IActionResult ListaDisponibles(DateTime fecha) 
        {
            var espaciosDisponibles = (from e in  _espacioContext.espacios
                                       where e.disponible == true
                                       select e).ToList();

            if (espaciosDisponibles.Count() == 0) return NotFound();

            return Ok();
        }


        //Actualizar el espacio del  parqueo
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEspacio(int id, [FromBody] espacio espaciosModificar)
        {
            espacio? espacioActual = (from e in _espacioContext.espacios
                                      where e.espacioId == id
                                      select e).FirstOrDefault();

            if (espacioActual == null)
            {
                return NotFound();
            }

            espacioActual.numeroEspacio = espaciosModificar.numeroEspacio;
            espacioActual.costoHora = espaciosModificar.costoHora;
            espacioActual.disponible = espaciosModificar.disponible;


            _espacioContext.Entry(espacioActual).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            _espacioContext.SaveChanges();
            return Ok(espaciosModificar);
        }

        // eliminar un espacio 
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult eliminarEpacio(int id)
        {
            espacio? espacios = (from e in _espacioContext.espacios
                                 where e.espacioId == id
                                 select e).FirstOrDefault();
            if (espacios == null)
            { return NotFound(); }

            _espacioContext.espacios.Attach(espacios);
            _espacioContext.Remove(espacios);
            _espacioContext.SaveChanges();

            return Ok(espacios);

        }
    }
}
