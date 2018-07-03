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
    public class CompanyJobEducationRepository : BaseADO, IDataRepository<CompanyJobEducationPoco>
    {
        public void Add(params CompanyJobEducationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                 foreach(CompanyJobEducationPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations]
                    ([Id],[Job],[Major],[Importance])Values(@Id,@Job,@Major,@Importance)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Major", Poco.Major);
                    cmd.Parameters.AddWithValue("@Importance", Poco.Importance);

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

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            CompanyJobEducationPoco[] Pocos = new CompanyJobEducationPoco[1500];
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = @"Select * From Company_Job_Educations";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while(reader.Read())
                {
                    CompanyJobEducationPoco poco = new CompanyJobEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.Importance = reader.GetInt16(3);
                    poco.TimeStamp = (byte[])reader[4];

                    Pocos[position] = poco;
                    position++;

                }
                Connection.Close();


            }
            return Pocos.Where(p=>p!=null).ToList();
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyJobEducationPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Job_Educations WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;

                foreach(CompanyJobEducationPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Job_Educations
                     SET
                      job=@job,Major=@Major,Importance=@Importance
                      WHERE Id=@Id";
                    
                    cmd.Parameters.AddWithValue("@job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Major", Poco.Major);
                    cmd.Parameters.AddWithValue("@Importance", Poco.Importance);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();

                }


            }
        }
    }
}
