using Microsoft.Extensions.Configuration;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories.Interfaces;
using System;
using System.Data.SqlClient;
using System.Text;

namespace SimpleBotCore.Repositories
{
    public class SqlUserProfileRepository : IUserProfileRepository
    {
        SimpleUser _users;
        private IConfiguration _configuration;

        public SqlUserProfileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SimpleUser TryLoadUser(string userId)
        {
            if (Exists(userId))
            {
                return GetUser(userId);
            }

            return null;
        }

        public SimpleUser Create(SimpleUser user)
        {
            if (Exists(user.Id))
                throw new InvalidOperationException("Usuário ja existente");

            CreateUser(user);

            return user;
        }

        public void AtualizaNome(string userId, string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Nome = name;

            SaveUser(user);
        }

        public void AtualizaIdade(string userId, int idade)
        {
            if (idade <= 0)
                throw new ArgumentOutOfRangeException(nameof(idade));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Idade = idade;

            SaveUser(user);
        }

        public void AtualizaCor(string userId, string cor)
        {
            if (cor == null)
                throw new ArgumentNullException(nameof(cor));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Cor = cor;

            SaveUser(user);
        }

        private bool Exists(string userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SimpleBotCore")))
            {
                //string result = string.Empty;
                connection.Open();
                string query = "SELECT 1 FROM dbo.SimpleUser WHERE Id=@userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@userId", userId));

                    command.ExecuteScalar();
                    var result = command.ExecuteScalar();
                    // Call Read before accessing data.
                    if (result != null)
                    {
                        return (int)result > 0;
                    }
                    else return false;
                };
            }
        }

        public SimpleUser GetUser(string userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SimpleBotCore")))
            {
                connection.Open();
                string query = "SELECT Id, Nome, Idade, Cor FROM dbo.SimpleUser WHERE Id=@userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@userId", userId));
                    SqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        _users = new SimpleUser(rdr["Id"].ToString());
                        _users.Nome = rdr["Nome"].ToString();
                        _users.Idade = string.IsNullOrEmpty(rdr["Idade"].ToString()) ? 0 : Convert.ToInt32(rdr["Idade"].ToString());
                        _users.Cor = rdr["Cor"].ToString();
                    }
                };
            }
            return _users;
        }

        private void CreateUser(SimpleUser user)
        {
            StringBuilder query = new StringBuilder("INSERT INTO [dbo].[SimpleUser] ([Id] ");
            StringBuilder queryValues = new StringBuilder(" VALUES (@userId ");

            if (!string.IsNullOrEmpty(user.Nome))
            {
                query.Append(", Nome");
                queryValues.Append(", @nome");
            }

            if (user.Idade > 0)
            {
                query.Append(", Idade");
                queryValues.Append(", @idade");

            }
            if (!string.IsNullOrEmpty(user.Cor))
            {
                query.Append(", Cor");
                queryValues.Append(", @cor");
            }
            query.Append(")");
            queryValues.Append(")");

            query.Append(queryValues.ToString());


            using (var connection = new SqlConnection(_configuration.GetConnectionString("SimpleBotCore")))
            {
                connection.Open();
                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    command.Parameters.Add(new SqlParameter("@userId", user.Id));

                    if (!string.IsNullOrEmpty(user.Nome))
                    {
                        command.Parameters.Add(new SqlParameter("@nome", user.Nome));
                    }

                    if (user.Idade > 0)
                    {
                        command.Parameters.Add(new SqlParameter("@idade", user.Idade));
                    }
                    if (!string.IsNullOrEmpty(user.Cor))
                    {
                        command.Parameters.Add(new SqlParameter("@cor", user.Cor));
                    }
                    command.ExecuteScalar();
                };
            }
        }

        private void SaveUser(SimpleUser user)
        {
            StringBuilder query = new StringBuilder(" UPDATE [dbo].[SimpleUser] SET ");
            StringBuilder queryValues = new StringBuilder();

            if (!string.IsNullOrEmpty(user.Nome))
            {
                queryValues.Append(" Nome = @nome ");

            }

            if (user.Idade > 0)
            {
                if (string.IsNullOrEmpty(queryValues.ToString()))
                {
                    queryValues.Append(" Idade = @idade ");

                }
                else
                    queryValues.Append(", Idade = @idade ");


            }
            if (!string.IsNullOrEmpty(user.Cor))
            {
                if (string.IsNullOrEmpty(queryValues.ToString()))
                {
                    queryValues.Append(" Cor = @cor");

                }
                else
                    queryValues.Append(", Cor = @cor");

            }
            query.Append(queryValues.ToString());
            query.Append(" WHERE Id = @userId");

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SimpleBotCore")))
            {
                connection.Open();
                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    command.Parameters.Add(new SqlParameter("@userId", user.Id));
                    command.Parameters.Add(new SqlParameter("@idade", user.Idade));
                    command.Parameters.Add(new SqlParameter("@cor", user.Cor));
                    command.Parameters.Add(new SqlParameter("@nome", user.Nome));
                    command.ExecuteNonQuery();
                };
            }
        }
    }
}

