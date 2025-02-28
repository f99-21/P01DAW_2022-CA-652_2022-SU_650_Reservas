using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01_2022_CA_652_2022_SU_650.Models;

namespace P01_2022_CA_652_2022_SU_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservasController : ControllerBase
    {

        private readonly reservaContext _reservaContext;

        public reservasController(reservaContext reservaContext)
        {
            _reservaContext = reservaContext;
        }

        /// <summary>
        /// EndPoint para crear una reservacion
        /// </summary>
        [HttpPost]
        [Route("Reservar")]
        public IActionResult agregarReserva([FromBody] reserva reserva)
        {
            try
            {
                _reservaContext.reservas.Add(reserva);
                _reservaContext.SaveChanges();
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// EndPoint para mostrar una lista de reservas activas del usuario
        /// </summary>
        /// <param name="usuarioId"></param>
        [HttpGet]
        [Route("ReservasUsuario/{usuarioId}")]
        public IActionResult GetReservas(int usuarioId)
        {
            var reservasActivas = (from u in _reservaContext.usuarios
                                   where u.usuarioId == usuarioId
                                   select new
                                   {
                                       u.usuarioId,
                                       u.nombreUsuario,
                                       Reservas = (from r in _reservaContext.reservas
                                                   where r.usuarioId == u.usuarioId
                                                   && r.fechaReservacion > DateTime.Now
                                                   select r).ToList()
                                   }).FirstOrDefault();

            if (reservasActivas == null) return NotFound();

            return Ok(reservasActivas);

        }

        /// <summary>
        /// EndPoint para cancelar una reserva antes de su uso
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("Cancelar/{id}")]
        public IActionResult Cancelar(int id) 
        {
            reserva? reservaCancelar = (from r in _reservaContext.reservas
                                        where r.reservaId == id
                                        select r).FirstOrDefault();

            if (reservaCancelar == null) return NotFound();

            if (reservaCancelar.fechaReservacion > DateTime.Now)
            {
                _reservaContext.reservas.Attach(reservaCancelar);
                _reservaContext.Remove(reservaCancelar);
                _reservaContext.SaveChanges();

                return Ok(reservaCancelar);
            }
            else 
            {
                return BadRequest("La reserva ya pasó. No puede ser cancelada");
            }

        }

        /// <summary>
        /// EndPoint para mostrar lista de los espacios reservados por día de todas las sucursales.
        /// </summary>
        /// <param name="fecha"></param>
        [HttpGet]
        [Route("ReservasPorDia")]
        public IActionResult listarReservasXDia(DateTime fecha)
        {
            var listaSucursal = (from s in _reservaContext.sucursales
                                 select new
                                 {
                                     s.sucursalNombre,
                                     Reservas = (from e in _reservaContext.espacios
                                                 join r in _reservaContext.reservas on e.espacioId equals r.espacioId
                                                 where r.fechaReservacion.Date == fecha.Date
                                                 select new
                                                 {
                                                     r.reservaId
                                                 }).ToList()
                                 }).ToList();

            if (listaSucursal.Count == 0) return NotFound();

            return Ok(listaSucursal);
        }

    }
}
