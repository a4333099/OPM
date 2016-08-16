using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPM.Core.Domain;

namespace OPM.Services.Users
{
    public interface IUserInfoSer
    {
        void CreateUserinfointo(UserInfo user);

        string GetUidByAccount(string account);

        UserInfo GetUserinfoByAccountandPwd(string account, string pwd);

        bool CheckLogin(string account, string pwd);

        List<string> GetTotalAccount();

        List<string> GetTotalUid();

        string GenerateUid();

        UserInfo GetUserInfofromCache(string sid);

        UserInfo CreatePartGuest();

        UserInfo GetUserInfoByUidandPasswd(string sid, string uid, string mdpwd);





    }
}
