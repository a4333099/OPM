using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OPM.Web.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 影子账号名
        /// </summary>
        [Required(ErrorMessage = "账户不能为空")]
        [Display(Name = "账户")]
        public string Account { get; set; }


        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }


        /// <summary>
        /// 是否允许记住用户
        /// </summary>
        [Display(Name = "记住我?")]
        public bool IsRemember { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }

    public class RegModel
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }

        [Required]
        [Display(Name = "账户")]
        public string Account { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }




        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }
}