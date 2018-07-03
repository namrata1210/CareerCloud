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
   public class SecurityLoginsLogRepository : BaseADO, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;

                foreach (SecurityLoginsLogPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                    ([Id],[Login] ,[Source_IP],[Logon_Date],[Is_Succesful])
                     Values(@Id,@Login,@Source_IP,@Logon_Date,@Is_Succesful)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Source_Ip", Poco.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", Poco.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", Poco.IsSuccesful);

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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            SecurityLoginsLogPoco[] Pocos = new SecurityLoginsLogPoco[1900];
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = @"SELECT * FROM Security_Logins_Log";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())

                {
                    SecurityLoginsLogPoco Poco = new SecurityLoginsLogPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Login = reader.GetGuid(1);
                    Poco.SourceIP = reader.GetString(2);
                    Poco.LogonDate = reader.GetDateTime(3);
                    Poco.IsSuccesful = reader.GetBoolean(4);

                    Pocos[position] = Poco;
                    position++;
                }
                Connection.Close();

            }
            return Pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SecurityLoginsLogPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Security_Logins_Log WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;

                foreach (SecurityLoginsLogPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Security_Logins_Log 
                    SET  Login=@Login,Source_Ip=@Source_Ip,Logon_Date=@Logon_Date,Is_Succesful=@Is_Succesful
                     WHERE Id=@Id";




                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Source_Ip", Poco.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", Poco.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", Poco.IsSuccesful);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);


                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }


            }
        }
    }
}
    

