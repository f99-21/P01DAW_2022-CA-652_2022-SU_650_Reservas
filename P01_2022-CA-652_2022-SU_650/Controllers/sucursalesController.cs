using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_2022_CA_652_2022_SU_650.Models;

namespace P01_2022_CA_652_2022_SU_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sucursalesController : ControllerBase
    {
        private readonly reservaContext _sucursalesContext;

        public sucursalesController(reservaContext sucursalesContext)
        {
            _sucursalesContext = sucursalesContext;
        }

        /// <summary>
        /// EndPoint para obtener todas las sucursale
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetSucursales() 
        {
            List<sucursales> listaSucursales = (from s in _sucursalesContext.sucursales
                                              select s).ToList();

            if (listaSucursales.Count() == 0) return NotFound(); 

            return Ok(listaSucursales);
        }

        /// <summary>
        /// EndPoint para obtener una sucursal por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id) 
        {
            sucursales? sucursal = (from s in _sucursalesContext.sucursales
                                  where s.sucursalId == id
                                  select s).FirstOrDefault();

            if(sucursal == null) return NotFound();

            return Ok(sucursal);
        }

        /// <summary>
        /// EndPoint para crear un registro de sucursal
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IActionResult AddSucursal([FromBody] sucursales sucursal) 
        {
            try
            {
                _sucursalesContext.sucursales.Add(sucursal);
                _sucursalesContext.SaveChanges();
                return Ok(sucursal);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// EndPoint para actualizar un registro de sucursal
        /// </summary>
        /// <param name="id"></param>
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarSucursal(int id, [FromBody] sucursales sucursalModificar) 
        {
            // Obtener registro original
            sucursales? sucursalActual = (from s in _sucursalesContext.sucursales
                                        where s.sucursalId == id
                                        select s).FirstOrDefault();

            // Verificar si el registro existe
            if(sucursalActual == null) return NotFound();

            //Modificar si el registro existe
            sucursalActual.sucursalNombre = sucursalModificar.sucursalNombre;
            sucursalActual.direccion = sucursalModificar.direccion;
            sucursalActual.telefono = sucursalModificar.telefono;
            sucursalActual.administrador = sucursalModificar.administrador;
            sucursalActual.espacios = sucursalModificar.espacios;

            //Marcar como modificado y enviar a la base de datos
            _sucursalesContext.Entry(sucursalActual).State = EntityState.Modified;
            _sucursalesContext.SaveChanges();

            return Ok(sucursalModificar);
        }

        /// <summary>
        /// EndPoint para eliminar un registro de sucursal
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult Eliminar(int id) 
        {
            //Obtener el registro original
            sucursales? sucursal = (from s in _sucursalesContext.sucursales
                                  where s.sucursalId == id
                                  select s).FirstOrDefault();

            //Verificar si el registro existe
            if (sucursal == null) return NotFound();

            //Ejecutar la eliminacion
            _sucursalesContext.sucursales.Attach(sucursal);
            _sucursalesContext.sucursales.Remove(sucursal);
            _sucursalesContext.SaveChanges();

            return Ok(sucursal);
        }

    }
}
