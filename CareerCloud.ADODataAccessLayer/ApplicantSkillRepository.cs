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
   public class ApplicantSkillRepository : BaseADO, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                 foreach(ApplicantSkillPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                    ([Id],[Applicant],[Skill],[Skill_Level],[Start_Month],[Start_Year],[End_Month],[End_Year])
                     Values(@Id,@Applicant,@Skill,@Skill_Level,@Start_Month,@Start_Year,@End_Month,@End_Year)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1000];
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "select * from Applicant_Skills";
                Connection.Open();
                SqlDataReader reader= cmd.ExecuteReader();
                int position = 0;
                while(reader.Read())
                {
                    ApplicantSkillPoco Poco = new ApplicantSkillPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Applicant = reader.GetGuid(1);
                    Poco.Skill = reader.GetString(2);
                    Poco.SkillLevel = reader.GetString(3);
                    Poco.StartMonth = reader.GetByte(4);
                    Poco.StartYear = reader.GetInt32(5);
                    Poco.EndMonth = reader.GetByte(6);
                    Poco.EndYear = reader.GetInt32(7);
                    Poco.TimeStamp = (byte[])reader[8];

                    pocos[position] = Poco;
                    position++;   
                }
                Connection.Close();
            }
            return pocos.Where(p=>p!=null).ToList();
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {

            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantSkillPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Skills WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);

            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
              

                foreach(ApplicantSkillPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Skills
                     SET
                     Applicant=@Applicant,Skill=@Skill,Skill_Level=@Skill_Level,STart_Month=@Start_Month,Start_Year=@Start_Year,End_Month=@End_Month,End_Year=@End_Year
                     WHERE ID=@ID";


                    
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();

                }
            }
        }
    }
}
