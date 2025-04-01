using Microsoft.VisualStudio.Services.CircuitBreaker;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointofSale.DAL
{
    public static class roles
    {

        public static DataSet GetRoles()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("spGetRoles", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet dataSet = new DataSet();

                        connection.Open();
                        adapter.Fill(dataSet);
                        connection.Close();

                        return dataSet;
                    }
                }
            }
            catch
            {

                throw;
            }
        }


        public static SqlDataReader GetControlPermissions()
        {
            try
            {
                SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString);
                connection.Open();
                System.Data.SqlClient.SqlCommand Command = new System.Data.SqlClient.SqlCommand();
                Command.Connection = connection;
                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "spGetControlPermissions";
                System.Data.SqlClient.SqlDataReader DataReader = Command.ExecuteReader();

                connection.Close();
                return DataReader;

            }
            catch
            {

                throw;
            }
        }

        public static DataSet GetControlPermissionsTree()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("spGetControlPermissions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet dataSet = new DataSet();

                        connection.Open();
                        adapter.Fill(dataSet);
                        connection.Close();

                        return dataSet;
                    }
                }
            }
            catch
            {

                throw;
            }
        }


        public static DataSet GetControlPermissionsChildTree()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("spGetControlPermissionsChild", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet dataSet = new DataSet();

                        connection.Open();
                        adapter.Fill(dataSet);
                        connection.Close();

                        return dataSet;
                    }
                }
            }
            catch
            {

                throw;
            }
        }
    }
}
