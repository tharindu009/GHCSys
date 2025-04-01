using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointofSale.DAL
{

    public static class jobCard
    {


        public static int InsertNextServiceDetail(string JobNo,string NextServiceMileage, DateTime NextServiceDt, string NextServiceComments,string VehicleReg)
        {
            int Affected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                SqlCommand command = new SqlCommand("spInsertNextServiceDetail", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.Add(new SqlParameter("@JobNo", JobNo));
                command.Parameters.Add(new SqlParameter("@VehicleReg", VehicleReg));
                command.Parameters.Add(new SqlParameter("@Mileage", NextServiceMileage));
                command.Parameters.Add(new SqlParameter("@NextDate", NextServiceDt));
                command.Parameters.Add(new SqlParameter("@Comments", NextServiceComments));


                try
                {
                    connection.Open();
                    Affected = command.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    
                }
            }
            return Affected;
        }


        public static int UpdateNextServiceDetail(string JobNo, string NextServiceMileage, DateTime NextServiceDt, string NextServiceComments, string VehicleReg)
        {
            int Affected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                SqlCommand command = new SqlCommand("spUpdateNextServiceDetail", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.Add(new SqlParameter("@JobNo", JobNo));
                command.Parameters.Add(new SqlParameter("@VehicleReg", VehicleReg));
                command.Parameters.Add(new SqlParameter("@Mileage", NextServiceMileage));
                command.Parameters.Add(new SqlParameter("@NextDate", NextServiceDt));
                command.Parameters.Add(new SqlParameter("@Comments", NextServiceComments));


                try
                {
                    connection.Open();
                    Affected = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
            }
            return Affected;
        }

        public static DataSet BindFinishedJobs()
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("spBindFinishedJobs", connection))
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


        public static DataSet BindOpenJobs()
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("spBindOpenJobs", connection))
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


        public static DataSet BindStockItems()
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("spBindStockItems", connection))
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


        public static DataSet BindStockItemsWithCat(string Category,string SubCategory,string SubCategory2)
        {
            try
            {
                string[] paraname = new string[3];
                string[] paravalue = new string[3];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dtJobHistory;

                paraname[0] = "@Category";
                paravalue[0] = Category;

                paraname[1] = "@SubCategory";
                paravalue[1] = SubCategory;

                paraname[2] = "@SubCategory2";
                paravalue[2] = SubCategory2;

                dtJobHistory = dam.GetDatasetsp("spBindStockItemsWithCat", paraname, paravalue);

                return dtJobHistory;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet BindJobHistory(string vehicleReg)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dtJobHistory;

                paraname[0] = "@vehicleReg";
                paravalue[0] = vehicleReg;

                dtJobHistory = dam.GetDatasetsp("spGetJobHistory", paraname, paravalue);

                return dtJobHistory;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet GetCustomerDetail(string vehicleReg)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dtJobHistory;

                paraname[0] = "@vehicleReg";
                paravalue[0] = vehicleReg;

                dtJobHistory = dam.GetDatasetsp("spGetCustomerDetail", paraname, paravalue);

                return dtJobHistory;
            }
            catch
            {
                throw;
            }
        }


        public static DataSet GetServices(string SearchValue)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dtJobHistory;

                paraname[0] = "@ProductName";
                paravalue[0] = SearchValue;

                dtJobHistory = dam.GetDatasetsp("spGetServices", paraname, paravalue);

                return dtJobHistory;
            }
            catch
            {
                throw;
            }
        }


        public static DataSet GetSearchItems(string SearchValue)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dtJobHistory;

                paraname[0] = "@ProductName";
                paravalue[0] = SearchValue;

                dtJobHistory = dam.GetDatasetsp("spGetSearchItems", paraname, paravalue);

                return dtJobHistory;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet GetCustomerDetailFromID(string CustomerID)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dtCustomerDetail;

                paraname[0] = "@CustomerID";
                paravalue[0] = CustomerID;

                dtCustomerDetail = dam.GetDatasetsp("spGetCustomerDetailFromID", paraname, paravalue);

                return dtCustomerDetail;
            }
            catch
            {
                throw;
            }
        }

    }

    
}
