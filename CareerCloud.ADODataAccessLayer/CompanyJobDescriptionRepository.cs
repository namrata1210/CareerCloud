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
    public class CompanyJobDescriptionRepository : BaseADO, IDataRepository<CompanyJobDescriptionPoco>

    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;

                foreach(CompanyJobDescriptionPoco Poco in items)
                {
                    cmd.CommandText= @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
           ([Id],[Job],[Job_Name],[Job_Descriptions])
             Values(@Id,@Job,@Job_Name,@Job_Descriptions)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", Poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", Poco.JobDescriptions);


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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyJobDescriptionPoco[] Pocos = new CompanyJobDescriptionPoco[1000];
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = @"select * FROM Company_Jobs_Descriptions";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while(reader.Read())
                {
                    CompanyJobDescriptionPoco Poco = new CompanyJobDescriptionPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Job = reader.GetGuid(1);
                    Poco.JobName =reader.IsDBNull(2)?null: reader.GetString(2);
                    Poco.JobDescriptions = reader.IsDBNull(3)?null:reader.GetString(3);
                    Poco.TimeStamp = reader.IsDBNull(4)?null:(byte[])reader[4];

                    Pocos[position] = Poco;
                    position++;

                }
                Connection.Close();
                
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
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyJobDescriptionPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Jobs_Descriptions WHERE @ID=ID";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;

                foreach(CompanyJobDescriptionPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Job_Descriptions
                    SET 
                      Job=@Job,Job_Name=@Job_Name,Jobs_Description=@Job_Description
                       WHERE Id=@Id";





                    cmd.Parameters.AddWithValue("@Job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", Poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Description", Poco.JobDescriptions);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();

                }
            }
        }
    }
}
