using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OPM.Core.Cache;
using OPM.Core.Domain;
using OPM.Core.Helpers;
using OPM.Core.RandomsMethod;
using OPM.Services.Users;
using OPM.Web.Framework.Controllers;
using OPM.Web.Models;

namespace OPM.Web.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController()
        {
            _userInfoSer = OPMContext.CurrentEngine.Resolve<IUserInfoSer>();

            _cache = OPMContext.CurrentEngine.Resolve<IOPMSessionCache>();

            _random = OPMContext.CurrentEngine.Resolve<IOPMRandom>();
        }

        private IUserInfoSer _userInfoSer;

        private IOPMSessionCache _cache;
        private IOPMRandom _random;

        [HttpGet]
        public ActionResult Login()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";
            LoginModel model = new LoginModel()
            {
                ReturnUrl = returnUrl

            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                var account = WebHelper.GetFormString("Account");
                var password = WebHelper.GetFormString("Password");
                if (!_userInfoSer.GetTotalAccount().Contains(account))
                {
                    OPMContext.CurrentEngine.PromptLst.Add("登录失败，账号不存在");
                }
                else if (!_userInfoSer.CheckLogin(account, password))
                {
                    OPMContext.CurrentEngine.PromptLst.Add("登录失败，提供的账户名或者密码不正确");
                }
                if (OPMContext.CurrentEngine.PromptLst.Count > 0)
                {
                    // _userInfoSer.AddLoginFailTime(OPMContext.Sid);
                    loginModel.IsVerifyCode = true;
                    return View(loginModel);
                }
                else
                {
                    Utils.SetCookie("uid", _userInfoSer.GetUidByAccount(account));
                    var user = _userInfoSer.GetUserinfoByAccountandPwd(account, password);
                    Utils.SetPasswordCookie(user.Password, OPMContext.CurrentEngine.OPMConfig.EncryptKey);
                    return Redirect(loginModel.ReturnUrl);

                }
            }
            return View(loginModel);
        }

        [HttpGet]
        public ActionResult Register()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";
            RegModel regmodel = new RegModel
            {
                ReturnUrl = returnUrl,
                IsVerifyCode = true
            };
            return View(regmodel);

        }


        [HttpPost]
        public ActionResult Register(RegModel model)
        {
            if (ModelState.IsValid)
            {
                var account = WebHelper.GetFormString("Account");
                string password = WebHelper.GetFormString("password");
                string confirmPwd = WebHelper.GetFormString("confirmPwd");
                string verifyCode = WebHelper.GetFormString("verifyCode");
                string sid = Utils.GetSidCookie();
                var varifycd = _cache.Get<string>(sid, "verifyCode").ToString();
                #region 验证
                //账号验证
                if (string.IsNullOrEmpty(account))
                {
                    OPMContext.CurrentEngine.PromptLst.Add("提供的账户名不能为空");
                    //  ModelState.AddModelError("Account", "提供的账户名不能为空");
                }
                else if (account.Length < 4 || account.Length > 20)
                {

                    OPMContext.CurrentEngine.PromptLst.Add("账号必须大于4位且小于20位");
                    //  ModelState.AddModelError("Account", "账号必须大于4位且小于20位");
                }
                else if (account.Contains(" "))
                {
                    OPMContext.CurrentEngine.PromptLst.Add("账号不能包含空格");

                    //ModelState.AddModelError("Account", "账号不能包含空格");
                }
                else if (account.Contains(":"))
                {

                    OPMContext.CurrentEngine.PromptLst.Add("账号不能包含冒号");
                    //ModelState.AddModelError("Account", "账号不能包含冒号");
                }
                else if (account.Contains("<"))
                {
                    OPMContext.CurrentEngine.PromptLst.Add("账号不能包含特殊字符'<'");

                    //  ModelState.AddModelError("Account", "账号不能包含特殊字符'<'");
                }
                else if (account.Contains(">"))
                {
                    OPMContext.CurrentEngine.PromptLst.Add("账号不能包含特殊字符'>'");

                    // ModelState.AddModelError("Account", "账号不能包含特殊字符'>'");
                }
                else if ((!SecureHelper.IsSafeSqlString(account, false)))
                {
                    OPMContext.CurrentEngine.PromptLst.Add("账号不符合系统要求");
                    // ModelState.AddModelError("Account", "账号不符合系统要求");
                }
                else if (_userInfoSer.GetTotalAccount().Contains(account))
                {
                    OPMContext.CurrentEngine.PromptLst.Add("该账号已经被注册");
                    // ModelState.AddModelError("Account", "该账号已经被注册");
                }

                else if (varifycd != verifyCode)
                {
                    OPMContext.CurrentEngine.PromptLst.Add("验证码不正确");
                    // ModelState.AddModelError("verifyCode", "验证码不正确");
                }
                #endregion


                if (OPMContext.CurrentEngine.PromptLst.Count > 0)
                {
                    model.IsVerifyCode = true;
                    return View(model);
                }
                else
                {
                    UserInfo user = new UserInfo { Salt = _random.CreateRandomValue(6, true), Account = account };
                    user.Password = SecureHelper.MD5(password + user.Salt);
                    user.UId = _userInfoSer.GenerateUid();
                    user.NickName = user.NickName ?? user.Account;
                    _userInfoSer.CreateUserinfointo(user);
                    return null;
                }

            }
            else
            {
                return View(model);
            }

        }

        public ActionResult Logout()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";
            Utils.SetCookie("uid", -1);
            Utils.SetCookie("password", "");
            return Redirect(returnUrl);
        }

    }
}