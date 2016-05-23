using Controle_Usuarios.Models;
using Controle_Usuarios.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Controle_Usuarios.Controllers
{
    public class AccountController : ApiController
    {
        IUsuarioService Usuarios;

        public AccountController(IUsuarioService usuarios)
        {
            Usuarios = usuarios;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Usuario> Get()
        {
            return Usuarios.GetAll();
        }


        [HttpPost]
        [Route("SignUp")]
        public IHttpActionResult SignUp([FromBody]Usuario item)
        {
            if (item == null)
                return Ok(new ResultError { statusCode = 400, mensagem = "Erro na requisição" });

            if (Usuarios.FindByEmail(item.email) != null)
                return Ok(new ResultError { statusCode = 409, mensagem = "Email já existente" });

            Usuarios.Add(item);
            return Ok(item);
        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody]Login item)
        {
            if (item == null)
                return Ok(new ResultError { statusCode = 400, mensagem = "Erro na requisição" });

            var user = Usuarios.Autentica(item);

            if (user == null)
                return Ok(new ResultError { statusCode = 401, mensagem = "Usuário e/ou senha inválidos" });

            return Ok(user);
        }

        [HttpGet]
        [Route("Profile/{id}")]
        public IHttpActionResult Profile(Guid id)
        {
            IEnumerable<string> authentication;
            if (!Request.Headers.TryGetValues("Authentication", out authentication))
                return Ok(new ResultError { statusCode = 401, mensagem = "Não autorizado" });

            var bearertoken = authentication.FirstOrDefault().Replace("Bearer", "").Trim();

            var user = Usuarios.FindByToken(bearertoken);
            if (user == null)
                return Ok(new ResultError { statusCode = 401, mensagem = "Não autorizado" });

            user = Usuarios.Find(id);
            if (user == null)
                return Ok(new ResultError { statusCode = 404, mensagem = "Não encontrado" });

            if (user.token != bearertoken)
                return Ok(new ResultError { statusCode = 401, mensagem = "Não autorizado" });

            if (user.ultimo_login.AddMinutes(30) < DateTime.Now)
                return Ok(new ResultError { statusCode = 403, mensagem = "Sessão inválida" });

            return Ok(user);
        }
    }
}
