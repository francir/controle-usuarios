using Controle_Usuarios.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Controle_Usuarios.Filters
{
    public class AppFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = context.Request.CreateResponse<ResultError>(HttpStatusCode.OK, new ResultError { statusCode = 404, mensagem = "Não encontrado" });
            }
        }

    }
}