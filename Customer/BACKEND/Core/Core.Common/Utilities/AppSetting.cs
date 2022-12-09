using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Core.Common.Utilities
{
    public static class AppSetting
    {
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = WebConfigurationManager.AppSettings;
                return appSettings[key];
            }
            catch (ConfigurationErrorsException)
            {
                return string.Empty;
            }
        }
        
        public static string DC
        {
            get
            {
                return ReadSetting("DungChung.ApiBaseUrl");
            }
        }
        public static string NewsAppSetting
        {
            get
            {
                return ReadSetting("News.ApiBaseUrl");
            }
        }
        public static string ProductsAppSetting
        {
            get
            {
                return ReadSetting("Products.ApiBaseUrl");
            }
        }
        public static string RecruitmentAppSetting
        {
            get
            {
                return ReadSetting("Recruitment.ApiBaseUrl");
            }
        }
        public static string GymSetupAppSetting
        {
            get
            {
                return ReadSetting("GymSetup.ApiBaseUrl");
            }
        }
        public static string AccountAppSetting
        {
            get
            {
                return ReadSetting("Account.ApiBaseUrl");
            }
        }

        public static string CartAppSetting
        {
            get
            {
                return ReadSetting("Cart.ApiBaseUrl");
            }
        }

        public static IDbConnection CMSSolutionConnection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString);
            }
        }

        public static string SessionUser
        {
            get
            {
                return ReadSetting("Ruby.SessionUser");
            }
        }
        public static string SessionBaseUrl
        {
            get
            {
                return ReadSetting("Ruby.SessionBaseUrl");
            }
        }

        public static string UserWebID
        {
            get
            {
                return ReadSetting("Ruby.UserWeb");
            }
        }

        public static string CountryID
        {
            get
            {
                return ReadSetting("Ruby.CountryID");
            }
        }
    }
}
