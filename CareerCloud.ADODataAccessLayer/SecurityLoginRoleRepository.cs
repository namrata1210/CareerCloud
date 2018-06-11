﻿using CareerCloud.DataAccessLayer;
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
    class SecurityLoginRoleRepository : BaseADO, IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach (SecurityLoginsRolePoco Poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                      ([Id],[Login],[Role]) 
                        Values(@Id,@Login,@Role)";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Role", Poco.Role);


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

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            SecurityLoginsRolePoco[] Pocos = new SecurityLoginsRolePoco[1000];
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"SELECT * FROM Security_Logins__Roles";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    SecurityLoginsRolePoco Poco = new SecurityLoginsRolePoco();
                    Poco.Id = reader.GetGuid(0);
                    Poco.Login = reader.GetGuid(1);
                    Poco.Role = reader.GetGuid(2);
                    Poco.TimeStamp = (byte[])reader[3];

                    Pocos[position] = Poco;
                    position++;
                }
                _connection.Close();

            }
            return Pocos.Where(p => p != null).ToList();
        }
            
        

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SecurityLoginsRolePoco Poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Security_Logins_Roles WHERE ID = @ID";

                    cmd.Parameters.AddWithValue("@Id", Poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {

            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;

                foreach (SecurityLoginsRolePoco Poco in items)
                {
                    cmd.CommandText = @"UPDATE Security_Logins_Roles
                     SET  Login=@Login,Role=@Role
                     WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("@Login", Poco.Login);
                    cmd.Parameters.AddWithValue("@Role", Poco.Role);
                    cmd.Parameters.AddWithValue("@Id", Poco.Id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close()
                }
            }
        }
    

