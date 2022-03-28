using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi_Test.Repositorios;

namespace WebApi_Test.Controllers
{
    [Route("api/asteroids")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return NotFound("Se debe rellenar el parametro 'planet'.");
        }

        [HttpGet("planet/{planet}")]
        public IActionResult Get(string planet)
        {
            if (string.IsNullOrEmpty(planet))
                return NotFound("La variable planet debe estar rellena.");

            RPAsteroides asteroides = new RPAsteroides();
            var ret = asteroides.ObtenerAsteroides();

            if(ret == null)
                return NotFound("No se han devuelto datos.");

            return Ok(JsonConvert.SerializeObject(ret).ToString());
        }
    }
}
