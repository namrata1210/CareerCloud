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
    public class CompanyProfileRepository : BaseADO, IDataRepository<CompanyProfilePoco>

    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] Pocos = new CompanyProfilePoco[1000];
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = @"SELECT * FROM Comapny_Profiles";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while(reader.Read())
                {
                    CompanyProfilePoco Poco = new CompanyProfilePoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.RegistrationDate = reader.GetDateTime(1);
                    Poco.CompanyWebsite =reader.IsDBNull(2)?null:reader.GetString(2);
                    Poco.ContactPhone = reader.GetString(3);
                    Poco.ContactName = reader.IsDBNull(4)?null:reader.GetString(4);
                    Poco.CompanyLogo = reader.IsDBNull(5)?null:(byte[])reader[5];
                    Poco.TimeStamp = reader.IsDBNull(6)?null:(byte[])reader[6];

                    Pocos[position] = Poco;
                    position++;
                }
                Connection.Close();
                    
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
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyProfilePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Profiles WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_Connstring);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
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


                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
