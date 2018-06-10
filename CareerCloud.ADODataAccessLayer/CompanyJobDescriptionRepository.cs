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
    class CompanyJobDescriptionRepository : BaseADO, IDataRepository<CompanyJobDescriptionPoco>

    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(CompanyJobDescriptionPoco Poco in items)
                {
                    cmd.CommandText= @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
           ([Id],[Job],[Job_Name],[Job_Descriptions])
             Values(@Id,@Job,@Job_Name,@Job_Descriptions)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", Poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Description", Poco.JobDescriptions);


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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyJobDescriptionPoco[] Pocos = new CompanyJobDescriptionPoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"select * FROM CompanyJobDescription";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while(reader.Read())
                {
                    CompanyJobDescriptionPoco Poco = new CompanyJobDescriptionPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Job = reader.GetGuid(1);
                    Poco.JobName = reader.GetString(2);
                    Poco.JobDescriptions = reader.GetString(3);
                    Poco.TimeStamp = (byte[])reader[4];

                    Pocos[position] = Poco;
                    position++;

                }
                _connection.Close();
                
            }
            return Pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (CompanyJobDescriptionPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM CompanyJobDescription WHERE @ID=ID";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(CompanyJobDescriptionPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE CompanyJobDescription
                    SET 
                      Job=@Job,Job_Name=@Job_Name,Job_Description=@Job_Description
                       WHERE Id=@Id";





                    cmd.Parameters.AddWithValue("@Job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", Poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Description", Poco.JobDescriptions);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }
    }
}
