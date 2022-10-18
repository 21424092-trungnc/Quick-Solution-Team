using Business.Entities.Domain;
using Core.Common.Utilities;
using Dapper;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Data;

namespace Data.Core.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(NewsRepository));
        private const string TableName = "";
        public AccountRepository(ILog logger)
        {
            _logger = logger;
        }

        #region "CRM Customer Data Leads"
        public string CustomerDataLeads_InsOrUpd(CustomerDataLeadsParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("DATALEADSID",model.DataLeadsID, DbType.String, ParameterDirection.Input);
                    paramters.Add("FULLNAME", model.FullName, DbType.String, ParameterDirection.Input);
                    paramters.Add("BIRTHDAY", model.BirthDay, DbType.String, ParameterDirection.Input);
                    paramters.Add("GENDER", model.GenDer, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PHONENUMBER", model.PhoneNumber, DbType.String, ParameterDirection.Input);
                    paramters.Add("EMAIL", model.Email, DbType.String, ParameterDirection.Input);
                    paramters.Add("MARITALSTATUS", model.MaritalStatus, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("IDCARD", model.IDCard, DbType.String, ParameterDirection.Input);
                    paramters.Add("IDCARDDATE", model.IDCardDate, DbType.String, ParameterDirection.Input);
                    paramters.Add("IDCARDPLACE", model.IDCardPlace, DbType.String, ParameterDirection.Input);
                    paramters.Add("ADDRESS", model.Address, DbType.String, ParameterDirection.Input);
                    paramters.Add("ADDRESSFULL", model.AddressFull, DbType.String, ParameterDirection.Input);
                    paramters.Add("PROVINCEID", model.ProvinceID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("DISTRICTID", model.DistrictID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("COUNTRYID", model.CountryID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("COUNTRYID", model.CountryID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("WARDID", model.WardID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("BUSINESSID", model.BusinessID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISACTIVE", model.IsActive, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CREATEDUSER", model.CreatedUser, DbType.String, ParameterDirection.Input);

                    var datas = conns.QueryFirstOrDefault<string>("CRM_CUSTOMERDATALEADS_CreateOrUpdate", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CustomerDataLeads_InsOrUpd Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public CustomerDataLeadsMap CustomerDataLeads_GetByPhone(string PhoneNumber, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PHONENUMBER", PhoneNumber, DbType.String, ParameterDirection.Input);

                    var datas = conns.QueryFirstOrDefault<CustomerDataLeadsMap>("CRM_CUSTOMERDATALEADS_GetByPhone", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CustomerDataLeads_InsOrUpd Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        #endregion

        #region "Account"
        public long Account_InsOrUpd(AccountParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("MEMBERID", model.MemberID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("USERNAME", model.UserName, DbType.String, ParameterDirection.Input);
                    paramters.Add("DATALEADSID", model.DataLeadsID, DbType.String, ParameterDirection.Input);
                    paramters.Add("PASSWORD", model.Passwrord, DbType.String, ParameterDirection.Input);
                    paramters.Add("BIRTHDAY", model.BirthDay, DbType.String, ParameterDirection.Input);
                    paramters.Add("CUSTOMERCODE", model.CustomerCode, DbType.String, ParameterDirection.Input);
                    paramters.Add("TYPEREGISTER", model.TypeRegister, DbType.String, ParameterDirection.Input);
                    paramters.Add("REGISTERDATE", model.RegisterDate, DbType.String, ParameterDirection.Input);
                    paramters.Add("FACEBOOKID", model.FacebookID, DbType.String, ParameterDirection.Input);
                    paramters.Add("GOOGLEID", model.GoogleID, DbType.String, ParameterDirection.Input);
                    paramters.Add("GENDER", model.Gender, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("FULLNAME", model.FullName, DbType.String, ParameterDirection.Input);
                    paramters.Add("IMAGEAVATAR", model.ImageAvatar, DbType.String, ParameterDirection.Input);
                    paramters.Add("PHONENUMBER", model.PhoneNumber, DbType.String, ParameterDirection.Input);
                    paramters.Add("EMAIL", model.Email, DbType.String, ParameterDirection.Input);
                    paramters.Add("MARITALSTATUS", model.MaritalStatus, DbType.String, ParameterDirection.Input);
                    paramters.Add("IDCARD", model.IDCard, DbType.String, ParameterDirection.Input);
                    paramters.Add("IDCARDDATE", model.IDCardDate, DbType.String, ParameterDirection.Input);
                    paramters.Add("IDCARDPLACE", model.IDCardPlace, DbType.String, ParameterDirection.Input);
                    paramters.Add("ADDRESS", model.Address, DbType.String, ParameterDirection.Input);
                    paramters.Add("PROVINCEID", model.ProvinceID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("DISTRICTID", model.DistrictID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("COUNTRYID", model.CountryID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("WARDID", model.WardID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISCONFIRM", model.IsConfirm, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISNOTIFICATION", model.IsNotification, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CONFIRMCODE", model.ConfirmCode, DbType.String, ParameterDirection.Input);
                    paramters.Add("CONFIRMDATE", model.ConfirmDate, DbType.Date, ParameterDirection.Input);
                    paramters.Add("ISCANSMSORPHONE", model.IsCanSMSOrPhone, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISCANEMAIL", model.IsCanEmail, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISACTIVE", model.IsActive, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISSYSTEM", model.IsSystem, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CREATEDUSER", model.CreateUser, DbType.String, ParameterDirection.Input);

                    var datas = conns.QueryFirstOrDefault<long>("CRM_ACCOUNT_CreateOrUpdate_Web", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return Convert.ToInt64(datas);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Account_InsOrUpd Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion

        #region "Login"
        public LoginDataModel CRM_ACCOUNT_CheckUserName_Web(string model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("USERNAME", model, DbType.String, ParameterDirection.Input);
                  
                    var datas = conns.QueryFirstOrDefault<LoginDataModel>("CRM_ACCOUNT_CheckUserName_Web", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CRM_ACCOUNT_CheckUserName_Web Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public int Account_CheckLogin(LoginDataModel model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("USERNAME", model.PhoneNumber, DbType.String, ParameterDirection.Input);
                    paramters.Add("PASSWORD", model.Password, DbType.String, ParameterDirection.Input); 
                    var datas = conns.QueryFirstOrDefault<int>("CRM_ACCOUNT_CheckLogin_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Account_CheckLogin Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public UserManager Account_GetInfoByPhone(string phone, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PHONENUMBER", phone, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<UserManager>("CRM_ACCOUNT_GetInfoByPhone_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    if (datas != null)
                    {
                        return datas;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Account_GetInfoByEmail Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public UserManager Account_GetInfoBySSO(string googleid, string facebookid, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("GOOGLEID", googleid, DbType.String, ParameterDirection.Input);
                    paramters.Add("FACEBOOKID", facebookid, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<UserManager>("CRM_ACCOUNT_GetInfoBySSO_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    if (datas != null)
                    {
                        return datas;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Account_GetInfoBySSO Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public bool Account_Active(string model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("KEY", model, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<bool>("CRM_ACCOUNT_Active_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Account_Active Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
        #endregion

        #region "Get info account"
        public UserManager Account_GetInfoByID(int MemberID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("MEMBERID", MemberID, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<UserManager>("CRM_ACCOUNT_GetInfoByID_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    if (datas != null)
                    {
                        return datas;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Account_GetInfoByID Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion
    }
}

