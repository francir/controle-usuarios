using Controle_Usuarios.Data;
using System;
using System.Collections.Generic;

namespace Controle_Usuarios.Models
{
    public class Usuario : IEntity
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string token { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime? data_atualizacao { get; set; }
        public DateTime ultimo_login { get; set; }
        public List<Telefone> telefones { get; set; }
    }

    public class Telefone
    {
        public string ddd { get; set; }
        public string numero { get; set; }
    }
}