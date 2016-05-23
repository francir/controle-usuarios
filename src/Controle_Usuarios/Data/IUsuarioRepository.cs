using System;
using System.Collections.Generic;
using Controle_Usuarios.Models;

namespace Controle_Usuarios.Data
{
    public interface IUsuarioRepository
    {
        void Add(Usuario item);
        List<Usuario> GetAll();
        Usuario Find(Guid id);
        Usuario Remove(Guid id);
        void Update(Usuario item);
    }
}