using Controle_Usuarios.Data;
using System;
using Controle_Usuarios.Models;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Controle_Usuarios.Business
{
    public class UsuarioService : IUsuarioService
    {
        IUsuarioRepository Usuarios;
        Hash hash = new Hash(SHA512.Create());

        public UsuarioService(IUsuarioRepository usuarios)
        {
            Usuarios = usuarios;
        }

        public void Add(Usuario item)
        {
            item.id = Guid.NewGuid();
            item.senha = hash.Criptografar(item.senha);
            item.token = hash.Criptografar(Guid.NewGuid().ToString());
            item.data_criacao = item.ultimo_login = DateTime.Now;
            item.data_atualizacao = null;

            Usuarios.Add(item);
        }

        public Usuario FindByEmail(string email)
        {
            var user = Usuarios.GetAll().FirstOrDefault(a => a.email == email);
            return user;
        }

        public Usuario FindByToken(string token)
        {
            var user = Usuarios.GetAll().FirstOrDefault(a => a.token == token);
            return user;
        }

        public List<Usuario> GetAll()
        {
            return Usuarios.GetAll();
        }

        public Usuario Find(Guid id)
        {
            return Usuarios.Find(id);
        }

        public Usuario Remove(Guid id)
        {
            return Usuarios.Remove(id);
        }

        public void Update(Usuario item)
        {
            item.data_atualizacao = DateTime.Now;
            Usuarios.Update(item);
        }

        public Usuario Autentica(Login login)
        {
            var user = FindByEmail(login.email);

            if (user == null || !hash.VerificarValor(login.senha, user.senha))
                return null;

            user.ultimo_login = DateTime.Now;
            Usuarios.Update(user);
            return user;
        }
    }
}