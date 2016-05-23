using NUnit.Framework;
using Controle_Usuarios.Controllers;
using Controle_Usuarios.Models;
using Controle_Usuarios.Business;
using Controle_Usuarios.Data;
using System.Web.Http.Results;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace Controle_Usuarios.Test
{
    [TestFixture]
    public class UnitTest1
    {
        IControleUsuariosContext context = new ControleUsuariosContext();

        [TestCase(new object[] { "Fulano", "fulano@gmail.com", "123456" }, TestName = "SignUp primeiro usuario")]
        [TestCase(new object[] { "Fulano", "fulano@gmail.com", "654321" }, TestName = "SignUp email duplicado")]
        public void SignUpTest(string nome, string email, string senha)
        {
            IUsuarioRepository uRepo = new UsuarioRepository(context);
            IUsuarioService uService = new UsuarioService(uRepo);
            var controller = new AccountController(uService);

            var user = new Usuario { nome = nome, email = email, senha = senha };
            var result = controller.SignUp(user);

            var usuario = result as OkNegotiatedContentResult<Usuario>;
            if (usuario != null)
            {
                Assert.AreEqual(usuario.Content.nome, nome);
            }
            else
            {
                var resultError = result as OkNegotiatedContentResult<ResultError>;
                Assert.AreEqual(resultError.Content.statusCode, 409);
            }
        }

        [TestCase(new object[] { "Francir", "francir@gmail.com", "123456" }, TestName = "Login correto")]
        public void Login(string nome, string email, string senha)
        {
            IUsuarioRepository uRepo = new UsuarioRepository(context);
            IUsuarioService uService = new UsuarioService(uRepo);
            var controller = new AccountController(uService);

            //Faz o registro do usuário
            var user = new Usuario { nome = nome, email = email, senha = senha };
            var usuario = controller.SignUp(user) as OkNegotiatedContentResult<Usuario>;

            //verifica o login
            var login = new Login { email = email, senha = senha };
            var usuarioLogin = controller.Login(login) as OkNegotiatedContentResult<Usuario>;

            Assert.AreEqual(usuarioLogin.Content.nome, nome);

        }

        [TestCase(new object[] { "Paulo", "paulo@gmail.com", "123456" }, TestName = "Login incorreto")]
        public void LoginIncorreto(string nome, string email, string senha)
        {
            IUsuarioRepository uRepo = new UsuarioRepository(context);
            IUsuarioService uService = new UsuarioService(uRepo);
            var controller = new AccountController(uService);

            //Faz o registro do usuário
            var user = new Usuario { nome = nome, email = email, senha = senha };
            var usuario = controller.SignUp(user) as OkNegotiatedContentResult<Usuario>;

            //verifica o login
            var login = new Login { email = email, senha = "1234" };
            var usuarioLogin = controller.Login(login) as OkNegotiatedContentResult<ResultError>;

            Assert.AreEqual(usuarioLogin.Content.statusCode, 401);
        }

        [TestCase(new object[] { "Fulano", "fulano@gmail.com", "123456" }, TestName = "Profile com token correto")]
        public void Profile(string nome, string email, string senha)
        {
            IUsuarioRepository uRepo = new UsuarioRepository(context);
            IUsuarioService uService = new UsuarioService(uRepo);
            var controller = new AccountController(uService);

            //Faz o registro do usuário
            var user = new Usuario { nome = nome, email = email, senha = senha };
            var usuario = controller.SignUp(user) as OkNegotiatedContentResult<Usuario>;

            //Adiciona o token no cabeçalho
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authentication", "Bearer " + usuario.Content.token);
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var usuarioProfile = controller.Profile(usuario.Content.id) as OkNegotiatedContentResult<Usuario>;

            Assert.AreEqual(usuarioProfile.Content.nome, nome);

        }

        [TestCase(new object[] { "Sicrano", "sicrano@gmail.com", "123456" }, TestName = "Profile com token incorreto")]
        public void ProfileInvalidToken(string nome, string email, string senha)
        {
            IUsuarioRepository uRepo = new UsuarioRepository(context);
            IUsuarioService uService = new UsuarioService(uRepo);
            var controller = new AccountController(uService);

            //Faz o registro do usuário
            var user = new Usuario { nome = nome, email = email, senha = senha };
            var usuario = controller.SignUp(user) as OkNegotiatedContentResult<Usuario>;

            //Adiciona o token no cabeçalho
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authentication", "Bearer abc" + usuario.Content.token);
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var usuarioProfile = controller.Profile(usuario.Content.id) as OkNegotiatedContentResult<ResultError>;

            Assert.AreEqual(usuarioProfile.Content.statusCode, 401);

        }
    }
}


