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
    class CompanyProfileRepository : BaseADO, IDataRepository<CompanyProfilePoco>

    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(CompanyProfilePoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                    ([Id],[Registration_Date],[Company_Website],[Contact_Phone],[Contact_Name],[Company_Logo])
                     VALUES(@Id,@Regestration_Date,@Company_Website,@Contact_Phone,@Contact_Name,@Company_Logo)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Regestration_Date", Poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", Poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", Poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", Poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", Poco.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] Pocos = new CompanyProfilePoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM Comapny_Profiles";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while(reader.Read())
                {
                    CompanyProfilePoco Poco = new CompanyProfilePoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.RegistrationDate = reader.GetDateTime(1);
                    Poco.CompanyWebsite = reader.GetString(2);
                    Poco.ContactPhone = reader.GetString(3);
                    Poco.ContactName = reader.GetString(4);
                    Poco.CompanyLogo = (byte[])reader[5];
                    Poco.TimeStamp = (byte[])reader[6];

                    Pocos[position] = Poco;
                    position++;
                }
                _connection.Close();
                    
            }
            return Pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (CompanyProfilePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Profiles WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach(CompanyProfilePoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Profiles
                    SET 
                     Regestration_Date=@Regestration_Date,Company_Website=@Company_Website,Contact_Phone=@Contact_Phone,
                    Contact_Name=@Contact_Name,Company_Logo=@Company_Logo 
                     WHERE Id=@Id";



                    cmd.Parameters.AddWithValue("@Regestration_Date", Poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", Poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", Poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", Poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", Poco.CompanyLogo);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);


                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }
    }
}
