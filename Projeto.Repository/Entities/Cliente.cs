using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Repository.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public Cliente()
        {
            
        }
        public Cliente(int idCliente, string nome, string email, string cpf)
        {
            IdCliente = idCliente;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        #region Relacionamento
        public List<Dependente> Dependentes { get; set; }
        #endregion

    }
}
