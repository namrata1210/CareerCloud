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
    class CompanyDescriptionRepository : BaseADO, IDataRepository<CompanyDescriptionPoco>

    {
        public CompanyDescriptionPoco poco { get; private set; }

        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                
                foreach(CompanyDescriptionPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                    ([Id],[Company],[LanguageID] ,[Company_Description])Values(@Id,@Company,@LangaugeId,@Company_Description)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Company", Poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageId", Poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", Poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", Poco.CompanyDescription);

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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
               
                cmd.CommandText = @"SELECT * FROM Company_Descriptions";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while(reader.Read())
                {
                    CompanyDescriptionPoco Poco = new CompanyDescriptionPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Company = reader.GetGuid(1);
                    Poco.LanguageId = reader.GetString(2);
                    Poco.CompanyName = reader.GetString(3);
                    Poco.CompanyDescription = reader.GetString(4);
                    Poco.TimeStamp = (byte[])reader[5];

                    pocos[position] = poco;
                    position++;


                }

                _connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (CompanyDescriptionPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Descriptions  WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(CompanyDescriptionPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Descriptions
                    SET
                    Company=@Company,LanguageId=@LanguageId,Company_Name=@Company_Name,Company_Description=@Company_Description 
                     WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Company", Poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageId", Poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", Poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", Poco.CompanyDescription);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }

               
            }
        }
    }
}
