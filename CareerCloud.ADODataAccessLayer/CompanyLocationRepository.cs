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
 public   class CompanyLocationRepository : BaseADO, IDataRepository<CompanyLocationPoco>
    {
       

        public void Add(params CompanyLocationPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(CompanyLocationPoco Poco in items)
                {
                    cmd.CommandText= @"INSERT INTO [dbo].[Company_Locations]
                   ([Id],[Company],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])
                    Values(@Id,@Company,@Country_Code,@State_Province_code,@Street_Addeess,@City_Town,@Zip_Postal_Code)";


                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Company", Poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", Poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_code", Poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Addeess", Poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", Poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", Poco.PostalCode);


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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM Company_Locations";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while(reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.CountryCode = reader.GetString(2);
                    poco.Province = reader.GetString(3);
                    poco.Street = reader.GetString(4);
                    poco.City = reader.GetString(5);
                    poco.PostalCode = reader.GetString(6);
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[position] = poco;
                    position++;


                }
                _connection.Close();
            }
            return pocos.Where(p => p != null).ToList();

        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {

            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"DELETE FROM Company_Locations WHERE ID=@ID";
                foreach (CompanyLocationPoco Poco in items)
                {
                    cmd.Parameters.AddWithValue("@ID", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
                
                
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach(CompanyLocationPoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Locations 
                     SET Company=@Company,Country_Code=@Country_Code,State_Province_code=@State_Province_code
                     Street_Addeess=@Street_Addeess,City_Town=@City_Town,Zip_Postal_Code=@Zip_Postal_Code
                       WHERE Id=@Id";


                    cmd.Parameters.AddWithValue("@Company", Poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", Poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_code", Poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Addeess", Poco.Street);
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
