namespace Business.Entities.Domain
{
    public class AccountParam
    {
        public long MemberID { get; set; }
	    public string UserName { get; set; }
        public string DataLeadsID { get; set; }
        public string Passwrord { get; set; }
        public string BirthDay { get; set; }
        public string CustomerCode { get; set; }
        public string TypeRegister { get; set; }
        public string RegisterDate { get; set; }
        public string GoogleID { get; set; }
        public string FacebookID { get; set; }
        public int Gender { get;set; }
        public string FullName { get; set; }
        public string ImageAvatar { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int MaritalStatus { get; set; }
        public string IDCard { get; set; }
        public string IDCardDate { get; set; }
        public string IDCardPlace { get; set; }
        public string Address { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int CountryID { get; set; }
        public int WardID { get; set; }
        public int IsConfirm { get; set; }
        public int IsNotification { get; set; }
        public string ConfirmCode { get; set; }
        public string ConfirmDate { get; set; }
        public int IsCanSMSOrPhone { get; set; }
        public int IsCanEmail { get; set; }
        public int IsActive { get; set; }
        public int IsSystem { get; set; }
        public string CreateUser { get; set; }
    }

    public class LoginDataModel
    {
        public long MEMBERID { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordOld { get; set; }
        public string Salt { get; set; }
        public string MaXacNhan { get; set; }
        public string RequestVerificationToken { get; set; }
        public string Message { get; set; }
        public bool status { get; set; }
    }
    public class ResultAccount
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
    public class ResultLogin
    {
        public int status { get; set; }
        public string message { get; set; }
        public bool result { get; set; }
        public string url { get; set; }
    }

    public class UserManager
    {
        public long MemberID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }
        public string DanhXung { get; set; }
        public string XacThucToken { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryID { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int WardID { get; set; }
        public string Address { get; set; }
        public string ImageAvatar { get; set; }

    }
    public class UserSSO
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }
        public string GoogleID { get; set; }
        public string FacebookID { get; set; }

    }
    public class UserSSOParam
    {
        public string GoogleID { get; set; }
        public string FacebookID { get; set; }

    }
}
