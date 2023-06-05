using Projeto.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Repository.Contracts
{
    public interface IDependenteRepository : IBaseRepository<Dependente>
    {
        List<Dependente> GetByNome(string nome);
    }
}
