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
    class ApplicantWorkHistoryRepository : BaseADO, IDataRepository<ApplicantWorkHistoryPoco>

    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                    cmd.Connection = _connection;
       

                foreach(ApplicantWorkHistoryPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
           ([Id],[Applicant] ,[Company_Name],[Country_Code],[Location],[Job_Title],[Job_Description],[Start_Month],[Start_Year],[End_Month],[End_Year])
           Values(@Id,@Applicant,@Company_Name,@Country_Code,@Location,@Job_Title,@Job_Description,@Start_Month,@Start_Year,@End_Month,@End_Year,@Time_Stamp)";

                    cmd.Parameters.AddWithValue("@ID", Poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", Poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", Poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", Poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", Poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", Poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", Poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", Poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", Poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", Poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", Poco.EndYear);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            ApplicantWorkHistoryPoco[] Pocos = new ApplicantWorkHistoryPoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from Applicant_Work_History";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.CompanyName = reader.GetString(2);
                    poco.CountryCode = reader.GetString(3);
                    poco.Location = reader.GetString(4);
                    poco.JobTitle = reader.GetString(5);
                    poco.JobDescription = reader.GetString(6);
                    poco.StartMonth = reader.GetInt16(7);
                    poco.StartYear = reader.GetInt32(8);
                    poco.EndMonth = reader.GetInt16(9);
                    poco.EndYear = reader.GetInt32(10);
                    poco.TimeStamp = (byte[])reader[11];


                    Pocos[position] = poco;
                    position++;

                }
                _connection.Close();
                
            }
            return Pocos.Where(p => p != null).ToList();


        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            foreach (ApplicantWorkHistoryPoco Poco in items)
            {
                cmd.CommandText = @"DELETE FROM Applicant_Work_History WHERE ID = @ID";

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach(ApplicantWorkHistoryPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Work_History 
                   SET
                     ID=@ID,Applicant=@Applicant,Company_Name=@Company_Name,Country_Code=@Country_Code,Location=@Location,
                     Job_Title=@Job_Title, Job_Description=@Job_Description, Start_Month=@Start_Month,Start_Year=@Start_Year,End_Month=@End_Month,@End_Year
                      WHERE ID=@ID";


                   
                    cmd.Parameters.AddWithValue("@Applicant", Poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", Poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", Poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", Poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", Poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", Poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", Poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", Poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", Poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", Poco.EndYear);
                    cmd.Parameters.AddWithValue("@ID", Poco.Id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }
    }
}
