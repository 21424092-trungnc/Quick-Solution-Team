using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    //public class LoginViewModel
    //{
    //    [Required(ErrorMessage = "Bắt buộc nhập {0}")]
    //    [Display(Name = "Tên đăng nhập")]
    //    public string UserName { get; set; }

    //    [Required(ErrorMessage = "Bắt buộc nhập {0}")]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Mật khẩu")]
    //    public string Password { get; set; }

    //    [Display(Name = "Tự động đăng nhập lần sau?")]
    //    public bool RememberMe { get; set; }
    //}

    public class LoginParam
    {
        [Required(ErrorMessage = "Bắt buộc nhập {0}")]
        [StringLength(100, ErrorMessage = "{0} không vượt quá 100 ký tự")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bắt buộc nhập {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}