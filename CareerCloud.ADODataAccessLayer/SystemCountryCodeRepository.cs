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
    class SystemCountryCodeRepository : BaseADO, IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach(SystemCountryCodePoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                    ([Code],[Name])
                      Values (@Code,@Name)";

                    cmd.Parameters.AddWithValue("@Code", Poco.Code);
                    cmd.Parameters.AddWithValue("@Name", Poco.Name);



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

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SystemCountryCodePoco[] Pocos = new SystemCountryCodePoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM System_Country_Codes";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    SystemCountryCodePoco Poco = new SystemCountryCodePoco();
                    Poco.Code = reader.GetString(0);
                    Poco.Name = reader.GetString(1);


                    Pocos[position] = Poco;
                    position++;
                }
                _connection.Close();
            }
            return Pocos.Where(p => p != null).ToList();


        }
            

        

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {

            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SystemCountryCodePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM System_Country_Codes WHERE Code=@Code";

                    cmd.Parameters.AddWithValue("@Code",Poco.Code);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SystemCountryCodePoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE FROM System_Country_Codes
                    SET Name=@Name WHERE Code=@Code";

                    
                    cmd.Parameters.AddWithValue("@Name", Poco.Name);
                    cmd.Parameters.AddWithValue("@Code", Poco.Code);


                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();



                }
            }
        }
    }
}
