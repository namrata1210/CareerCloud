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
   public  class ApplicantJobApplicationRepository : BaseADO, IDataRepository<ApplicantJobApplicationPoco>
    {
    
       public void Add(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
               

                foreach (ApplicantJobApplicationPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                    ([Id],[Applicant],[Job],[Application_Date]) values (@ID,@Applicant,@Job,@Application_Date)";

                    cmd.Parameters.AddWithValue("@ID", Poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", Poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", Poco.ApplicationDate);

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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            ApplicantJobApplicationPoco[] Pocos= new ApplicantJobApplicationPoco[1000];
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "SELECT * FROM Applicant_Job_Applications";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                 while(reader.Read())
                {
                    ApplicantJobApplicationPoco Poco = new ApplicantJobApplicationPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Applicant = reader.GetGuid(1);
                    Poco.Job = reader.GetGuid(2);
                    Poco.ApplicationDate = (DateTime)reader[3];
                    Poco.TimeStamp = (byte[])reader[4];

                    Pocos[position] = Poco;
                    position++;
                
                }
                Connection.Close();
                   
            }
            return Pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantJobApplicationPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Job_Applications WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }

        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
               
                foreach(ApplicantJobApplicationPoco Poco in items)
                {
                    cmd.CommandText= @"UPDATE Applicant_Job_Applications
                    SET 
                    Applicant=@Applicant,
                    Job =@Job,
                    Application_Date=@Application_Date
                    WHERE ID = @ID";

                  
                    cmd.Parameters.AddWithValue("@Applicant", Poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", Poco.ApplicationDate);
                    cmd.Parameters.AddWithValue("@ID", Poco.Id);

                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();



                }

            }

        }
    }
}
