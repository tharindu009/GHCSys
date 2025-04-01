using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointofSale.DAL
{
    public class customer
    {
        public static DataSet GetVehicleMake()
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetVehicleMake", connection))
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

        public static DataSet GetVehicleModel(string MakeID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetVehicleModel", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@MakeID", MakeID));


                    

                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet dataSet = new DataSet();
                        connection.Open();
                        adapter.Fill(dataSet);
                        connection.Close();

                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public static int InsertCustomer()
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                SqlCommand command = new SqlCommand("spInsertCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.Add(new SqlParameter("@FirstName", "John"));
                command.Parameters.Add(new SqlParameter("@LastName", "Doe"));
                command.Parameters.Add(new SqlParameter("@Age", 25));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static DataSet GetCustomerVehicle(string CusID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                SqlCommand command = new SqlCommand("spGetCustomerVehicle", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.Add(new SqlParameter("@CusID", CusID));

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    connection.Open();
                    adapter.Fill(dataSet);
                    connection.Close();

                    return dataSet;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static DataSet GetCustomer(string peopleType)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                SqlCommand command = new SqlCommand("spGetCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.Add(new SqlParameter("@peopleType", peopleType));

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    connection.Open();
                    adapter.Fill(dataSet);
                    connection.Close();

                    return dataSet;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
