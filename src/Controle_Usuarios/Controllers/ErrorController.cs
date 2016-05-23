using Controle_Usuarios.Models;
using System.Web.Http;

namespace Controle_Usuarios.Controllers
{
    public class ErrorController : ApiController
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public IHttpActionResult Handle404()
        {
            return Ok(new ResultError { statusCode = 404, mensagem = "Não encontrado" });
        }
    }
}
