using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PointofSale.DAL
{
    public static class Quote
    {
        public static DataSet GetQuoteReportData(string QuotationNo)
        {
            try
            {
                string[] paraname = new string[1];
                string[] paravalue = new string[1];

                DAL.DataAccessManager dam = new DataAccessManager();

                DataSet dsItem;

                paraname[0] = "@QuotationNo";
                paravalue[0] = QuotationNo;

                dsItem = dam.GetDatasetsp("[spGetQuotationDataset]", paraname, paravalue);

                return dsItem;
            }
            catch
            {
                throw;
            }
        }
    }
}
