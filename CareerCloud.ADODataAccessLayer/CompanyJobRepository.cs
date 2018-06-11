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
    public class CompanyJobRepository : BaseADO, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach(CompanyJobPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                ([Id],[Company],[Profile_Created],[Is_Inactive],[Is_Company_Hidden])
                 Values(@Id,@Company,@Profile_Created,@Is_Inactive,@Is_Company_Hidden)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Company", Poco.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", Poco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", Poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", Poco.IsCompanyHidden);


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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            CompanyJobPoco[] Pocos = new CompanyJobPoco[1000];
                using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM CompanyJobs";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while(reader.Read())
                {
                    CompanyJobPoco Poco = new CompanyJobPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Company = reader.GetGuid(1);
                    Poco.ProfileCreated = reader.GetDateTime(2);
                    Poco.IsInactive = reader.GetBoolean(3);
                    Poco.IsCompanyHidden = reader.GetBoolean(4);
                    Poco.TimeStamp = reader.IsDBNull(5)?null:(byte[])reader[5];

                    Pocos[position] = Poco;
                    position++;

                }
                _connection.Close();


            }
            return Pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach(CompanyJobPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM CompanyJobs WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                  
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(CompanyJobPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE CompanyJobs
                     SET Company=@Company,Profile_Created=@Profile_Created,Is_Inactive=@Is_Inactive,Is_Company_Hidden=@Is_Company_Hidden,
                     WHERE Id=@Id";



                    cmd.Parameters.AddWithValue("@Company", Poco.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", Poco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", Poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", Poco.IsCompanyHidden);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();


                }
            }
        }
    }
}
