using Microsoft.AspNetCore.Http;
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

            
        
    }
}
