using Controle_Usuarios.Models;
using System.Data.Entity;

namespace Controle_Usuarios.Data
{
    public interface IControleUsuariosContext
    {
        IDbSet<Usuario> Usuario { get; set; }

        int SaveChanges();

    }
}