using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : BaseADO, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {

            using (_connection) 
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
           

                foreach (ApplicantProfilePoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                    ([Id],[Login] ,[Current_Salary],[Current_Rate] ,[Currency],[Country_Code] ,[State_Province_Code] ,[Street_Address],[City_Town],[Zip_Postal_Code])
                     values(@Id,@Login,@Current_Salary,@Current_Rate,@Currency,@Country_Code,@State_Province_Code,@Street_Address,@City_Town,@Zip_Postal_Code)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", Poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", Poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", Poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", Poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", Poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", Poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", Poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", Poco.PostalCode);
                    cmd.Parameters.AddWithValue("@Time_Stamp", Poco.TimeStamp);


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

        public IList<ApplicantProfilePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "SELECT * FROM Applicant_Profiles";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.CurrentSalary = reader.IsDBNull(2) ? (decimal?)null : (decimal)reader[2]; 
                    poco.CurrentRate = reader.IsDBNull(3)?(decimal?)null:(decimal)reader[3];
                    poco.Currency = reader.IsDBNull(4)?null:reader.GetString(4);
                    poco.Country = reader.IsDBNull(5)?null:reader.GetString(5);
                    poco.Province = reader.IsDBNull(6)?null:reader.GetString(6);
                    poco.Street = reader.IsDBNull(7)?null:reader.GetString(7);
                    poco.City = reader.IsDBNull(8)?null:reader.GetString(8);
                    poco.PostalCode = reader.IsDBNull(9)?null:reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];

                    pocos[position] = poco;
                    position++;
                }
                _connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantProfilePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (ApplicantProfilePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Profiles  WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
   
                foreach(ApplicantProfilePoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Profile 
                    SET
                    login=@login,Current_Salary=@Current_Salary,Current_Rate=@Current_Rate,Currency=@Currency,
                    Country_Code=@Country_Code,State_Province_Code=@State_Province_Code,Street_Address=@Street_Address,
                    City_Town=@City_Town,Zip_Postal_Code=@Zip_Postal_Code
                    WHERE ID= @ID";
                    
                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", Poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", Poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", Poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", Poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", Poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", Poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", Poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", Poco.PostalCode);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    _connection.Open();
                   cmd.ExecuteNonQuery();
                    _connection.Close();


                }
            }
        }
    }
}
