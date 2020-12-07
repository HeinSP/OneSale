using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Lib
{
    class DataLib
    {
        public string Connect()
        {
            string strConnection = "Server=localhost; initial catalog=Ceres;";
            strConnection = "Data Source = 127.0.0.1; Initial Catalog = Ceres; User ID = sa; Password = 0506Hein";
            SqlConnection connection = new SqlConnection(strConnection);
            try
            {
                connection.Open();
                return SuccessfulInfo;
            }
            catch(Exception e)
            {
                string s = e.Message;
                return e.Message;
            }
        }

        static internal readonly string SuccessfulInfo = "DataLib connected successfully.";
    }
}
