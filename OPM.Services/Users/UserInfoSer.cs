using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPM.Core.Cache;
using OPM.Core.Config;
using OPM.Core.Data;
using OPM.Core.Domain;
using OPM.Core.Helpers;
using OPM.Core.RandomsMethod;

namespace OPM.Services.Users
{
    public class UserInfoSer : IUserInfoSer
    {

        public UserInfoSer(IRepository<UserInfo> users, IOPMSessionCache cache, IOPMRandom random)
        {
            _users = users;
            _cache = cache;
            _random = random;
        }

        private IRepository<UserInfo> _users;

        private IOPMSessionCache _cache;
        private IOPMRandom _random;
        //==============================================================================================
        #region Db
        /// <summary>
        /// 获取用户信息(NOSQL)
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mdpwd">加密密码</param>
        /// <returns></returns>
        private UserInfo GetUserInfoByUidandPwd(string uid, string mdpwd)
        {
            var userinfo = _users.GetEntity(u => u.UId == uid);
            if (userinfo != null && userinfo.Password == mdpwd)
            {
                return userinfo;
            }
            return null;
        }
        /// <summary>
        /// 创建用户(NOSQL)
        /// </summary>
        /// <param name="user"></param>
        public void CreateUserinfointo(UserInfo user)
        {
            _users.Insert(user);
        }
        /// <summary>
        /// 获取UID(NOSQL)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetUidByAccount(string account)
        {
            var userinfo = _users.GetEntity(u => u.Mobile == account || u.Email == account || u.Account == account);
            if (userinfo != null)
                return userinfo.UId.ToString();
            return null;
        }
        /// <summary>
        /// 获取账号信息(NOSQL)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd">明文密码</param>
        /// <returns></returns>
        public UserInfo GetUserinfoByAccountandPwd(string account, string pwd)
        {
            var userinfo = _users.GetEntity(u => u.Account == account);
            if (userinfo != null && userinfo.Password == SecureHelper.MD5(pwd + userinfo.Salt))
            {
                return userinfo;
            }
            return null;
        }

        /// <summary>
        /// 校验登录(NOSQL)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd">明文密码</param>
        /// <returns></returns>
        public bool CheckLogin(string account, string pwd)
        {
            var userinfo = _users.GetEntity(u => u.Account == account);
            if (userinfo != null && userinfo.Password == SecureHelper.MD5(pwd + userinfo.Salt))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取账号列表(NOSQL)
        /// </summary>
        /// <returns></returns>
        public List<string> GetTotalAccount()
        {
            var accs = _users.GetEntities(u => true);
            return accs.Select(u => u.Account).ToList();
        }

        /// <summary>
        /// 获取UID列表(NOSQL)
        /// </summary>
        /// <returns></returns>
        public List<string> GetTotalUid()
        {
            var accs = _users.GetEntities(u => true);
            return accs.Select(u => u.UId).ToList();
        }

        /// <summary>
        /// 生成UID(NOSQL)
        /// </summary>
        /// <returns></returns>
        public string GenerateUid()
        {
            string uid = "0";
            Func<string> func = null;

            func = () =>
            {
                uid = _random.CreateRandomValueWithoutZero(6, true);
                if (!GetTotalUid().Contains(uid))
                {
                    return uid;
                }
                else
                {
                    func();
                }
                return "0";
            };

            uid = func();
            return uid;
        }

        #endregion


        #region Cache
        /// <summary>
        /// 通过sID获取userinfo(缓存)
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public UserInfo GetUserInfofromCache(string sid)
        {
            var user = _cache.Get<UserInfo>(sid, "userinfo") as UserInfo;
            return user;
        }


        /// <summary>
        /// 通过sID获取userinfo(缓存)
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="pwd">明文密码</param>
        /// <returns></returns>
        private UserInfo GetUserInfofromCache(string sid, string uid, string pwd)
        {
            var user = _cache.Get<UserInfo>(sid, "userinfo") as UserInfo;
            if (user != null && user.UId == uid && user.Password == pwd)
            {
                return user;
            }
            return null;
        }


        /// <summary>
        /// 保存userinfo到Session缓存中(缓存)
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="user"></param>
        private void SaveUserInfo(string sid, UserInfo user)
        {
            _cache.Set(sid, "userinfo", user, 30);
        }

        #endregion



        //==============================================================================================
        #region Other
        /// <summary>
        /// 创建游客
        /// </summary>
        /// <returns></returns>
        public UserInfo CreatePartGuest()
        {
            return new UserInfo
            {
                UId = "-1",
                Account = "guest",
                Email = "",
                Mobile = "",
                Password = "",
                UserRid = 6,
                StoreId = 0,
                MallAGid = 1,
                NickName = "游客",
                Avatar = "",
                PayCredits = 0,
                RankCredits = 0,
                VerifyEmail = 0,
                VerifyMobile = 0,
                LiftBanTime = new DateTime(1900, 1, 1),
                Salt = ""
            };
        }
        #endregion


        #region Total
        /// <summary>
        /// 汇总获取USERINFO
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="mdpwd"></param>
        /// <returns></returns>
        public UserInfo GetUserInfoByUidandPasswd(string sid, string uid, string mdpwd)
        {
            UserInfo userinfo;
            userinfo = GetUserInfofromCache(sid, uid, mdpwd);
            if (userinfo != null) return userinfo;
            userinfo = GetUserInfoByUidandPwd(uid, mdpwd);
            if (userinfo != null)
            {
                SaveUserInfo(sid, userinfo);
                return userinfo;
            }
            return null;
        }


        #endregion
    }
}
