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
    class ApplicantResumeRepository : BaseADO, IDataRepository<ApplicantResumePoco>

    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                int rowseffected = 0;

                foreach (ApplicantResumePoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                       ([Id],[Applicant],[Resume],[Last_Updated])Values(@Id,@Applicant,@Resume,@Last_Updated)";

                    cmd.Parameters.AddWithValue("@id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", Poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", Poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", Poco.LastUpdated);

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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            ApplicantResumePoco[] Pocos = new ApplicantResumePoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while(reader.Read())
                {
                    ApplicantResumePoco Poco = new ApplicantResumePoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Applicant = reader.GetGuid(1);
                    Poco.Resume = reader.GetString(2);
                    Poco.LastUpdated = (DateTime?)reader[3];

                    Pocos[position] = Poco;
                    position++;
                }
                _connection.Close();
            }
            return Pocos;
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (ApplicantResumePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Resumes Poco WHERE ID= @ID";


                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                   
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                _connection.Open();
                int rowseffected = 0;
                foreach (ApplicantResumePoco Poco in items)
                {
                    cmd.CommandText=@"UPDATE Applicant_Resumes Poco 
                     SET
                     ID=@ID,Applicant= @Applicant,Resume=@Resume,Last_Updated=@Last_Updated
                     WHERE ID = @ID"


                    cmd.Parameters.AddWithValue("@id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", Poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", Poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", Poco.LastUpdated);

                    _connection.Open();
                    rowseffected += cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }
    }
}
