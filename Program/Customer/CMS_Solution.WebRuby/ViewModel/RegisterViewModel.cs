using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public int CountryID { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int WardID { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string GoogleID { get; set; }
        public string FacebookID { get; set; }
        public string TypeRegister { get; set; }
    }

    public class Login
    {
        public string UserLogin { get; set; }
        public string PassWord { get; set; }
    }
    public class AccountIsActive
    {
        public bool status { get; set; }
        public string messenger { get; set; }
    }
}