using Controle_Usuarios.Models;
using System.Data.Entity;

namespace Controle_Usuarios.Data
{
    public class ControleUsuariosContext : DbContext, IControleUsuariosContext
    {
        public IDbSet<Usuario> Usuario { get; set; }

        public ControleUsuariosContext()
        {
            Seed();
        }
        public void ClearAll()
        {
            this.Usuario = new MemoryPersistenceDbSet<Usuario>();
        }
        public override int SaveChanges()
        {
            return 0;
        }

        public void Seed()
        {
            ClearAll();

            //this.Usuario.Add(new Usuario { id = new Guid(), nome = "Francir" });
        }


    }
}