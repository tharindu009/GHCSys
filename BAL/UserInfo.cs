using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointofSale.BAL
{
    public static class UserInfo
    {
        public static string Userid { get; set; }
        public static string UserName { get; set; }
        public static string UserPassword { get; set; }
        public static string usertype { get; set; }
        public static string invoiceNo { get; set; }
        public static string Shopid { get; set; }
        public static string usernamWK { get; set; }
    }

    public static class ReportValue  // use in report
    {
        public static string StartDate { get; set; }
        public static string EndDate { get; set; }
        public static string emp { get; set; }
        // public static string Reportid { get; set; }
        public static string Terminal { get; set; }
        //public static string StartDateGroupby { get; set; }
        //public static string EndDateGroupby { get; set; }
    }

    public static class parameter
    {
        public static string helpid { get; set; }
        public static string peopleid { get; set; }
        public static string autoprintid { get; set; }

    }

    public static class vatdisvalue
    {
        public static string vat
        {
            set
            {
                //   //Load Vat and Discount rate
                //   string sqlVat= "select * from storeconfig";
                //   DataAccess.ExecuteSQL(sqlVat);
                //   DataTable dtVat = DataAccess.GetDataTable(sqlVat);
                ////   txtVATRate.Text = dtVatdis.Rows[0].ItemArray[6].ToString();
                //  // txtDiscountRate.Text = dtVatdis.Rows[0].ItemArray[7].ToString();
                //   string vl =  dtVat.Rows[0].ItemArray[6].ToString();
                //   vl = value;              
            }
            get
            {
                string sqlVat = " select VAT from tbl_terminallocation where Shopid = '" + UserInfo.Shopid + "' "; // 
                DAL.DataAccessManager.ExecuteSQL(sqlVat);
                DataTable dtVat = DAL.DataAccessManager.GetDataTable(sqlVat);
                string vl = dtVat.Rows[0].ItemArray[0].ToString();
                return vl;
            }
        }

        public static string dis
        {
            set
            {
                //string sqldis = "select * from storeconfig";
                //DataAccess.ExecuteSQL(sqldis);
                //DataTable dtdis = DataAccess.GetDataTable(sqldis);
                //string vl = dtdis.Rows[0].ItemArray[7].ToString();
                //vl = value;
            }
            get
            {
                string sqldis = "select Dis from tbl_terminallocation   where Shopid = '" + UserInfo.Shopid + "' ";
                DAL.DataAccessManager.ExecuteSQL(sqldis);
                DataTable dtdis = DAL.DataAccessManager.GetDataTable(sqldis);
                string vl = dtdis.Rows[0].ItemArray[0].ToString();
                return vl;
            }
        }
    }
}
