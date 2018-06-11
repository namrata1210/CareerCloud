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
   public class SystemLanguageCodeRepository : BaseADO, IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SystemLanguageCodePoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
           ([LanguageID],[Name],[Native_Name]) 
             Values (@LanguageId,@Name,@Native_Name)";

                    cmd.Parameters.AddWithValue("@LanguageId", Poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", Poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", Poco.NativeName);

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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SystemLanguageCodePoco[] Pocos = new SystemLanguageCodePoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM System_Language_Codes";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    SystemLanguageCodePoco Poco = new SystemLanguageCodePoco();
                    Poco.LanguageID = reader.GetString(0);
                    Poco.Name = reader.GetString(1);
                    Poco.NativeName = reader.GetString(2);

                    Pocos[position] = Poco;
                    position++;
                }
                _connection.Close();
            }
            return Pocos.Where(p => p != null).ToList();

        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {

            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SystemLanguageCodePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM System_Language_Codes WHERE LanguageId=@LanguageId";


                    cmd.Parameters.AddWithValue("@LanguageId", Poco.LanguageID);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SystemLanguageCodePoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE System_Language-Codes
                      SET Name=@Name,Native_Name=@Native_Name
                        WHERE LanguageId=@LanguageId";

                    cmd.Parameters.AddWithValue("@Name", Poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", Poco.NativeName);
                    cmd.Parameters.AddWithValue("@LanguageId", Poco.LanguageID);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }
    }
}
