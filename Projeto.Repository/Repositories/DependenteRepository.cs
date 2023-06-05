using Dapper;
using Projeto.Repository.Contracts;
using Projeto.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Repository.Repositories
{
    public class DependenteRepository : IDependenteRepository
    {
        private readonly string connectionStrings;

        public DependenteRepository(string connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }

        public void Create(Dependente entity)
        {
            var query = "insert into Dependente(Nome, DataNascimento, IdCliente) "
                      + "values(@Nome, @DataNascimento, @IdCliente) ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Dependente entity)
        {
            var query = "update Dependente set Nome = @Nome, DataNascimento = @DataNascimento, IdCliente = @IdCliente "
                      + "where IdDependente = @IdDependente ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Dependente entity)
        {
            var query = "delete from Dependente where IdDependente = @IdDependente ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Dependente> GetAll()
        {
            var query = "select * from Dependente order by Nome asc ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.Query<Dependente>(query).ToList();
            }
        }

        public Dependente GetById(int id)
        {
            var query = "select * from Dependente where IdDependente = @IdDependente ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.QueryFirstOrDefault<Dependente>(query, new { IdDependente = id });
            }
        }

        public List<Dependente> GetByNome(string nome)
        {
            var query = "select * from Dependente where Nome Like @Nome order by Nome asc ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.Query<Dependente>(query, new { Nome = ("%" + nome + "%") }).ToList();
            }
        }
    }
}
