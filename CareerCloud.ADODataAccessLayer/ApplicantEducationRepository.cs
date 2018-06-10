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
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
               cmd.Connection = _connection;
             

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
                    cmd.Parameters.AddWithValue("@Completion_Percent", Poco.CompletionDate);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            ApplicantEducationPoco[] Pocos = new ApplicantEducationPoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from Applicant_Education";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;

                while (reader.Read())
                {
                    ApplicantEducationPoco Poco = new ApplicantEducationPoco();

                    Poco.Id = reader.GetGuid(0);
                    Poco.Applicant = reader.GetGuid(1);
                    Poco.Major = reader.GetString(2);
                    Poco.CertificateDiploma = reader.GetString(3);
                    Poco.StartDate = (DateTime?)reader[4];
                    Poco.CompletionDate = (DateTime?)reader[5];
                    Poco.CompletionPercent = (byte?)reader[6];
                    Poco.TimeStamp = (byte[])reader[7];

                    Pocos[position] = Poco;
                    position++;

                }

                _connection.Close();

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
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (ApplicantEducationPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Educations WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
              
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
               
                foreach(ApplicantEducationPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Application_Educations
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
                    cmd.Parameters.AddWithValue("@Completion_Percent", Poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@ID", Poco.Id);

                    _connection.Open();
                     cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }
    }
}
