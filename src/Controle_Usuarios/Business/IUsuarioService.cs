using Controle_Usuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Controle_Usuarios.Business
{
    public interface IUsuarioService
    {
        void Add(Usuario item);
        Usuario FindByEmail(string email);
        Usuario FindByToken(string token);
        List<Usuario> GetAll();
        Usuario Find(Guid id);
        Usuario Remove(Guid id);
        void Update(Usuario item);
        Usuario Autentica(Login login);
    }
}