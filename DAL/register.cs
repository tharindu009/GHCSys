using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointofSale.DAL
{
    public static class register
    {
        public static DataSet BindItemWithImages(string value)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dsItem;

                paraname[0] = "@value";
                paravalue[0] = value;

                dsItem = dam.GetDatasetsp("spGetItemListWithImage", paraname, paravalue);

                return dsItem;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet BindItemWithStock(string value)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dsItem;

                paraname[0] = "@value";
                paravalue[0] = value;

                dsItem = dam.GetDatasetsp("spBindItemWithStock", paraname, paravalue);

                return dsItem;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet BindItemBarcode(string value,double TaxRate)
        {
            try
            {
                string[] paraname = new string[2];
                string[] paravalue = new string[2];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dsItem;

                paraname[0] = "@ItemID";
                paravalue[0] = value;

                paraname[1] = "@TaxRate";
                paravalue[1] = TaxRate.ToString();

                dsItem = dam.GetDatasetsp("[spGetItemBarcode]", paraname, paravalue);

                return dsItem;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet GetItemListWithImageDefault()
        {
            using (SqlConnection connection = new SqlConnection(DataAccessManager._ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetItemListWithImageDefault", connection))
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

        public static DataSet BindItemStockList(string value)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dsItem;

                paraname[0] = "@value";
                paravalue[0] = value;

                dsItem = dam.GetDatasetsp("BindItemStockList", paraname, paravalue);

                return dsItem;
            }
            catch
            {
                throw;
            }
        }

    }
}
