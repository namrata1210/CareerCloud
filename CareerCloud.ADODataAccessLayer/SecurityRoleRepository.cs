using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
   public class SecurityRoleRepository : BaseADO, IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(SecurityRolePoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                    ([Id],[Role],[Is_Inactive])
                     Values (@Id,@Role,@Is_Inactive)";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Role", Poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", Poco.IsInactive);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            SecurityRolePoco[] Pocos = new SecurityRolePoco[1000];
            using (_connection)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM Security_Roles";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    SecurityRolePoco Poco = new SecurityRolePoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Role = reader.GetString(1);
                    Poco.IsInactive = reader.GetBoolean(2);

                    Pocos[position] = Poco;
                    position++;
                }
                _connection.Close();
            }
            return Pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            using(_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SecurityRolePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Security_Roles WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {

            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach (SecurityRolePoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE FROM Security_Roles
                     SET Role=@Role,Is_Inactive=@Is_Inactive
                      WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("@Role", Poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", Poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }
    }
}
