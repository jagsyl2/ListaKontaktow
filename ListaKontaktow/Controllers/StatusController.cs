using Microsoft.AspNetCore.Mvc;

namespace ListaKontaktow.Controllers
{
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        //Method: GET
        //URL: http://localhost:10500/api/status
        //Body: no body
        [HttpGet]
        public string GetStatus()
        {
            return "Status OK";
        }
    }
}
