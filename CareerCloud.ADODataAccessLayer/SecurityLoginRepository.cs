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
   public class SecurityLoginRepository : BaseADO, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(SecurityLoginPoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins]
           ([Id],[Login],[Password],[Created_Date],[Password_Update_Date],[Agreement_Accepted_Date],
            [Is_Locked],[Is_Inactive],[Email_Address],[Phone_Number],[Full_Name],[Force_Change_Password],[Prefferred_Language])
            VALUES(@ID,@Login,@Password,@Created_Date,@Password_Update_Date,@Agreement_Accepted_Date,@Is_Locked,@Is_Inactive,
            @Email_Address,@Phone_Number,@Full_Name,@Force_Change_Password,@Prefferred_Language)";


     
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Password", Poco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", Poco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", Poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", Poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", Poco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", Poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", Poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", Poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", Poco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", Poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", Poco.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SecurityLoginPoco[] Pocos = new SecurityLoginPoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM Security_Logins";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while(reader.Read())
                {
                    SecurityLoginPoco Poco = new SecurityLoginPoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Login = reader.GetString(1);
                    Poco.Password = reader.GetString(2);
                    Poco.Created = reader.GetDateTime(3);
                    Poco.PasswordUpdate = (DateTime?)reader[4];
                    Poco.AgreementAccepted = (DateTime?)reader[5];
                    Poco.IsLocked = reader.GetBoolean(6);
                    Poco.IsInactive = reader.GetBoolean(7);
                    Poco.EmailAddress = reader.GetString(8);
                    Poco.PhoneNumber = reader.GetString(9);
                    Poco.FullName = reader.GetString(10);
                    Poco.ForceChangePassword = reader.GetBoolean(11);
                    Poco.PrefferredLanguage = reader.GetString(12);
                    Poco.TimeStamp = (byte[])reader[13];


                    Pocos[position] = Poco;
                    position++;
                   
                  
                }
                _connection.Close();

            }
            return Pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SecurityLoginPoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Security_Logins WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach (SecurityLoginPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Security_Logins 
                    SET
                     Login=@Login,Password=@Password,Created_Date=@Created_Date,Password_Update_Date=@Password_Update_Date
                      Agreement_Accepted_Date=@Agreement_Accepted_Date,Is_Locked=@Is_Locked,Is_Inactive=@Is_Inactive
                      Email_Address=@Email_Address,Phone_Number=@Phone_Number,Full_Name=@Full_Name,Force_Change_Password=@Force_Change_Password
                      Prefferred_Language=@Prefferred_Language WHERE Id=@Id";






                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Password", Poco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", Poco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", Poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", Poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", Poco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", Poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", Poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", Poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", Poco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", Poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", Poco.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }
    }
}
