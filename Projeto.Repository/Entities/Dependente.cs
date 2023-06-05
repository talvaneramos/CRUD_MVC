using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Repository.Entities
{
    public class Dependente
    {
        public int IdDependente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int IdCliente { get; set; }        

        public Dependente()
        {
            
        }

        public Dependente(int idDependente, string nome, DateTime dataNascimento, int idCliente)
        {
            IdDependente = idDependente;
            Nome = nome;
            DataNascimento = dataNascimento;
            IdCliente = idCliente;            
        }

        #region Relacionamento
        public Cliente Cliente { get; set; }
        #endregion

    }

}
