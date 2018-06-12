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
    public class ApplicantEducationRepository : BaseADO,IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
               cmd.Connection = Connection;
             

                foreach(ApplicantEducationPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations] 
                   ([Id],[Applicant],[Major],[Certificate_Diploma],[Start_Date] ,[Completion_Date] ,[Completion_Percent])
                   Values(@Id,@Applicant,@Major,@Certificate_Diploma,@Start_Date,@Completion_Date ,@Completion_Percent)";

                    cmd.Parameters.AddWithValue("@ID", Poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", Poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", Poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma",Poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", Poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", Poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", Poco.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            ApplicantEducationPoco[] Pocos = new ApplicantEducationPoco[1000];
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "select * from Applicant_Educations";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;

                while (reader.Read())
                {
                    ApplicantEducationPoco Poco = new ApplicantEducationPoco();

                    Poco.Id = reader.GetGuid(0);
                    Poco.Applicant = reader.GetGuid(1);
                    Poco.Major = reader.GetString(2);
                    Poco.CertificateDiploma = reader.GetString(3);
                    Poco.StartDate = reader.IsDBNull(4) ? (DateTime?)null : (DateTime)reader[4];

                    Poco.CompletionDate = reader.IsDBNull(5) ? (DateTime?)null : (DateTime)reader[5];
                    Poco.CompletionPercent = reader.IsDBNull(6)?(byte?)null:(byte)reader[6];
                    Poco.TimeStamp = (byte[])reader[7];

                    Pocos[position] = Poco;
                    position++;

                }

                Connection.Close();

            }
            return Pocos.Where(p=>p!=null).ToList();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantEducationPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Educations WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
              
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
               
                foreach(ApplicantEducationPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Educations
                      SET 
                      Applicant=@Applicant,
                      Major =@Major,
                      Certificate_Diploma=@Certificate_Diploma,
                      Start_Date=@Start_date,
                      Completion_Date=@Completion_Date,
                      Completion_Percent=@Completion_Percent
                      WHERE ID = @ID";

                   
                    cmd.Parameters.AddWithValue("@Applicant", Poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", Poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", Poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", Poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", Poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", Poco.CompletionPercent);
                    cmd.Parameters.AddWithValue("@ID", Poco.Id);

                    Connection.Open();
                     cmd.ExecuteNonQuery();
                    Connection.Close();

                }
            }
        }
    }
}
