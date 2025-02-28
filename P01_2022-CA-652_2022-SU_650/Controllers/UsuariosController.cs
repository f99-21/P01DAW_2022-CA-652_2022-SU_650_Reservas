using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01_2022_CA_652_2022_SU_650.Models;

namespace P01_2022_CA_652_2022_SU_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly reservaContext _usuarioContex;

        public UsuariosController(reservaContext usuarioContext)
        {
            _usuarioContex = usuarioContext;
        }


        /// <summary>
        /// EndPoint para obtener todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuario> listaUsuario = (from e in _usuarioContex.usuarios
                                          select e).ToList();

            if (listaUsuario.Count == 0)
            {
                return NotFound();
            }

            return Ok(listaUsuario);
        }

        /// <summary>
        /// EndPoint para obtener un usuario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            usuario? usuario = (from e in _usuarioContex.usuarios
                                where e.usuarioId == id
                                select e).FirstOrDefault();


            if (usuario == null)
            {
                return NotFound();

            }
            return Ok(usuario);
        }

        /// <summary>
        /// EndPoint para crear un registro de usuario
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IActionResult agregar([FromBody] usuario usuri)
        {
            try
            {
                _usuarioContex.usuarios.Add(usuri);
                _usuarioContex.SaveChanges();
                return Ok(usuri);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);


            }
        }

        /// <summary>
        /// EndPoint para actualizar un registro de usuario
        /// </summary>
        /// <param name="id"></param>
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] usuario usuarioModificar)
        {
            usuario? usuarioActual = (from e in _usuarioContex.usuarios
                                      where e.usuarioId == id
                                      select e).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();

            }


            usuarioActual.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioActual.correo = usuarioModificar.correo;
            usuarioActual.telefono = usuarioModificar.telefono;
            usuarioActual.contrasena = usuarioModificar.contrasena;
            usuarioActual.rolId = usuarioModificar.rolId;


            _usuarioContex.Entry(usuarioActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _usuarioContex.SaveChanges();
            return NoContent();



        }

        /// <summary>
        /// EndPoint para eliminar un registro de usuario
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            usuario? usuaio = (from e in _usuarioContex.usuarios
                               where e.usuarioId == id
                               select e).FirstOrDefault();
            if (usuaio == null)
            {
                return NotFound();
            }

            _usuarioContex.usuarios.Attach(usuaio);
            _usuarioContex.usuarios.Remove(usuaio);
            _usuarioContex.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// EndPoint para verificar usuario y contraseña
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasena"></param>
        [HttpGet]
        [Route("VerficarCredenciales/{nombreUsuario}/{contrasena}")]
        public IActionResult VerificarCredenciales(string nombreUsuario, string contrasena)
        {
            usuario? usuario = (from u in _usuarioContex.usuarios
                                where u.nombreUsuario == nombreUsuario && u.contrasena == contrasena
                                select u).FirstOrDefault();

            if (usuario == null) return NotFound();

            return Ok("Verificación realizada con exito");
        }

        

    }
}
