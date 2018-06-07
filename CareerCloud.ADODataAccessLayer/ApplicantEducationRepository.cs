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
    class ApplicantEducationRepository : BaseADO,IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
               cmd.Connection = _connection;
               int  rowseffected = 0;

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
                     rowseffected += cmd.ExecuteNonQuery();
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
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
