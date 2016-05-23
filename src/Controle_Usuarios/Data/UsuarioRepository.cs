using Controle_Usuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controle_Usuarios.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        IControleUsuariosContext _context;

        public UsuarioRepository(IControleUsuariosContext context)
        {
            _context = context;
        }

        public List<Usuario> GetAll()
        {
            IQueryable<Usuario> query = _context.Usuario;
            return query.ToList();
        }

        public void Add(Usuario item)
        {
            _context.Usuario.Add(item);
        }

        public Usuario Find(Guid id)
        {
            var item = _context.Usuario.FirstOrDefault(a => a.id == id);
            return item;
        }

        public Usuario Remove(Guid id)
        {
            var user = Find(id);
            _context.Usuario.Remove(user);
            return user;
        }

        public void Update(Usuario item)
        {
            var user = Find(item.id);
            user.data_atualizacao = item.data_atualizacao;
        }
    }
}