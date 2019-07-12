using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PatientManagerApp.Models
{
    public class DataAccess : IDataAccess
    {
        private const string _dbName = "Patient";

        public IEnumerable<Patient> GetPatients(int startIndex, int pageSize)
        {
            var sql = $@"SELECT [Id]
                               ,[FirstName]
                               ,[LastName]
                               ,[DateOfBirth]
                               ,[SocialSecurityNumber]
                               ,[PhoneNumber]
                               ,[Email]
                           FROM [Datacenter].[Patients]
                       ORDER BY [LastName]
                         OFFSET '{ startIndex }' ROWS
                     FETCH NEXT '{ pageSize }' ROWS ONLY";

            using (var conn = new SqlConnection(Helper.CnnVal(_dbName)))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        yield return new Patient()
                        {
                            Id = (int)dataReader["Id"],
                            FirstName = (string)dataReader["FirstName"],
                            LastName = (string)dataReader["LastName"],
                            DateOfBirth = (DateTime)dataReader["DateOfBirth"],
                            SocialSecurityNumber = (string)dataReader["SocialSecurityNumber"],
                            PhoneNumber = (string)dataReader["PhoneNumber"],
                            Email = (string)dataReader["Email"]
                        };
                    }
                    dataReader.Close();
                }
                cmd.Dispose();
                conn.Close();
            }
        }

        public int GetCountOfPatients()
        {
            var sql = $@"SELECT COUNT(*)
                           FROM [Datacenter].[Patients]";

            int result = 0;

            using (var conn = new SqlConnection(Helper.CnnVal(_dbName)))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {

                    }
                    dataReader.Read();
                    dataReader.Close();
                }

                result = (int)cmd.ExecuteScalar();

                cmd.Dispose();
                conn.Close();
            }

            return result;
        }

        public void AddNewPatient(Patient patient)
        {
            var sql = $@"INSERT INTO [Datacenter].[Patients]
                                    ([FirstName]
                                    ,[LastName]
                                    ,[DateOfBirth]
                                    ,[SocialSecurityNumber]
                                    ,[PhoneNumber]
                                    ,[Email])
                                VALUES
                                    ('{ patient.FirstName }'
                                    ,'{ patient.LastName }'
                                    ,'{ patient.DateOfBirth.ToString("yyyy-MM-dd") }'
                                    ,'{ patient.SocialSecurityNumber }'
                                    ,'{ patient.PhoneNumber }'
                                    ,'{ patient.Email }')";

            using (var conn = new SqlConnection(Helper.CnnVal(_dbName)))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (var adapter = new SqlDataAdapter())
                {
                    adapter.InsertCommand = cmd;
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                cmd.Dispose();
                conn.Close();
            }
        }

        public void UpdatePatient(Patient patient)
        {
            var sql = $@"UPDATE [Datacenter].[Patients]
                            SET [FirstName] = '{ patient.FirstName }'
                               ,[LastName] = '{ patient.LastName }'
                               ,[DateOfBirth] = '{ patient.DateOfBirth.ToString("yyyy-MM-dd") }'
                               ,[SocialSecurityNumber] = '{ patient.SocialSecurityNumber }'
                               ,[PhoneNumber] = '{ patient.PhoneNumber }'
                               ,[Email] = '{ patient.Email }'
                          WHERE [Id] = { patient.Id }";

            using (var conn = new SqlConnection(Helper.CnnVal(_dbName)))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (var adapter = new SqlDataAdapter())
                {
                    adapter.UpdateCommand = cmd;
                    adapter.UpdateCommand.ExecuteNonQuery();
                }
                cmd.Dispose();
                conn.Close();
            }
        }

        public void DeletePatient(int id)
        {
            var sql = $@"DELETE FROM [Datacenter].[Patients] WHERE [Id] = { id }";

            using (var conn = new SqlConnection(Helper.CnnVal(_dbName)))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (var adapter = new SqlDataAdapter())
                {
                    adapter.DeleteCommand = cmd;
                    adapter.DeleteCommand.ExecuteNonQuery();
                }
                cmd.Dispose();
                conn.Close();
            }
        }
    }
}
