using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Data.Core.Repositories.Base
{
    public class clsCommon
    {
        public static string strConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
        public static SqlConnection SQLConn = new SqlConnection(strConn);

        public static SqlConnection SQLConnection()
        {
            if (SQLConn.State != ConnectionState.Closed)
                SQLConn.Close();
            if (strConn.Trim() == "") strConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
            SQLConn = new SqlConnection(strConn);
            SQLConn.Open();
            return SQLConn;
        }

        public static DataTable GetDataTable(string sSQL)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sSQL, strConn);
                DataTable dt = new DataTable();
                int row = da.Fill(dt);
                return dt;
            }
            catch { return null; }
        }

        public static DataSet GetdataSet(string sSQL)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sSQL, strConn);
                DataSet dt = new DataSet();
                int row = da.Fill(dt);
                return dt;
            }
            catch(Exception ex) { var s = ex.Message; return null; }
        }

        public static Boolean CheckMail(string strMail)
        {
            try
            {
                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);
                if (reg.IsMatch(strMail))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool ExcuteSQL(string pstrSQL)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection SQLconn;
            SQLconn = new SqlConnection(strConn);
            try
            {
                SQLconn.Open();
                cmd.Connection = SQLconn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = pstrSQL;
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                SQLconn.Close();
                SQLconn.Dispose();
                return true;
            }
            catch
            {
                cmd.Dispose();
                SQLconn.Close();
                SQLconn.Dispose();
                return false;
            }
        }
    }
}
