using Dapper;
using Projeto.Repository.Contracts;
using Projeto.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Repository.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string connectionStrings;

        public ClienteRepository(string connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }

        public void Create(Cliente entity)
        {
            var query = "insert into Cliente(Nome, Email, Cpf) "
                      + "values(@Nome, @Email, @Cpf) ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Cliente entity)
        {
            var query = "update Cliente set Nome = @Nome, Email = @Email, Cpf = @Cpf "
                      + "where IdCliente = @IdCliente ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Cliente entity)
        {
            var query = "delete from Cliente where IdCliente = @IdCliente ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Cliente> GetAll()
        {
            var query = "select * from Cliente order by Nome asc ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.Query<Cliente>(query).ToList();
            }
        }

        public Cliente GetById(int id)
        {
            var query = "select * from Cliente where IdCliente = @IdCliente ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.QueryFirstOrDefault<Cliente>(query, new { IdCliente = id });
            }
        }

        public Cliente GetByEmail(string email)
        {
            var query = "select * from Cliente where Email = @Email ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.QueryFirstOrDefault<Cliente>(query, new { Email = email });
            }
        }

        public Cliente GetByCpf(string cpf)
        {
            var query = "select * from Cliente where Cpf = @Cpf ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.QueryFirstOrDefault<Cliente>(query, new { Cpf = cpf });
            }
        }
        public List<Cliente> GetByNome(string nome)
        {
            var query = "select * from Cliente where Nome Like @Nome order by Nome asc ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.Query<Cliente>(query, new { Nome = ("%" + nome + "%")}).ToList();
            }
        }

        public int ContadorDependentes(int idCliente)
        {
            var query = "select count(*) from Dependente where IdCliente = IdCliente ";

            using (var connection = new SqlConnection(connectionStrings))
            {
                return connection.QueryFirstOrDefault<int>(query, new { IdCliente = idCliente });
            }
        }
    }
}
