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
  public  class CompanyJobSkillRepository : BaseADO, IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                
                foreach(CompanyJobSkillPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                    ([Id],[Job],[Skill],[Skill_Level],[Importance])
                    Values(@Id,@Job,@Skill,@Skill_Level,@Importance)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Skill", Poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", Poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", Poco.Importance);


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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            CompanyJobSkillPoco[] Pocos = new CompanyJobSkillPoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM CompanyJobSkill";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while (reader.Read())
                {
                    CompanyJobSkillPoco Poco = new CompanyJobSkillPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Job = reader.GetGuid(1);
                    Poco.Skill = reader.GetString(2);
                    Poco.SkillLevel = reader.GetString(3);
                    Poco.Importance = reader.GetInt32(4);
                    Poco.TimeStamp = (byte[])reader[5];

                    Pocos[position] = Poco;
                    position++;

                }

                _connection.Close();
            }
            return Pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach(CompanyJobSkillPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM CompanyJobSkill WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }


              
             
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(CompanyJobSkillPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE CompanyJobskill
                    SET Job=@Job,Skill=@Skill,Skill_Level=@Skill_Level,Importance=@Importance
                     WHERE Id=@Id";
                 
                    cmd.Parameters.AddWithValue("@Job", Poco.Job);
                    cmd.Parameters.AddWithValue("@Skill", Poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", Poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", Poco.Importance);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }

            }
        }
    }
}
