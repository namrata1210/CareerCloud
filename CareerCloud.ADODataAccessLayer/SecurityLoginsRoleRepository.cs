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
   public class SecurityLoginsRoleRepository : BaseADO, IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;

                foreach (SecurityLoginsRolePoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                      ([Id],[Login],[Role]) 
                        Values(@Id,@Login,@Role)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Role", Poco.Role);


                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            SecurityLoginsRolePoco[] Pocos = new SecurityLoginsRolePoco[1000];
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = @"SELECT * FROM Security_Logins_Roles";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    SecurityLoginsRolePoco Poco = new SecurityLoginsRolePoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Login = reader.GetGuid(1);
                    Poco.Role = reader.GetGuid(2);
                    Poco.TimeStamp =reader.IsDBNull(3)?null: (byte[])reader[3];

                    Pocos[position] = Poco;
                    position++;
                }
                Connection.Close();

            }
            return Pocos.Where(p => p != null).ToList();
        }



        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SecurityLoginsRolePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Security_Logins_Roles WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;

                foreach (SecurityLoginsRolePoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Security_Logins_Roles
                     SET  Login=@Login,Role=@Role
                     WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Role", Poco.Role);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
    

