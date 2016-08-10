using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM.Core.Domain
{
    public class UserInfo
    {

        public string UId { get; set; }
        /// <summary>
        ///用户名称
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        ///<summary>
        ///用户等级id
        ///</summary>
        public int UserRid { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        public int StoreId { get; set; }

        ///<summary>
        ///商城管理员组id
        ///</summary>
        public int MallAGid { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        ///<summary>
        ///支付积分
        ///</summary>
        public int PayCredits { get; set; }

        /// <summary>
        /// 等级积分
        /// </summary>
        public int RankCredits { get; set; }

        /// <summary>
        /// 是否验证邮箱
        /// </summary>
        public int VerifyEmail { get; set; }

        /// <summary>
        /// 是否验证手机
        /// </summary>
        public int VerifyMobile { get; set; }

        /// <summary>
        /// 解禁时间
        /// </summary>
        public DateTime LiftBanTime { get; set; }

        ///<summary>
        ///盐值
        ///</summary>
        public string Salt { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastVisitTime { get; set; }

        /// <summary>
        /// 最后访问ip
        /// </summary>
        public string LastVisitIP { get; set; }

        /// <summary>
        /// 最后访问区域id
        /// </summary>
        public int LastVisitRgId { get; set; }

        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 用户注册ip
        /// </summary>
        public string RegisterIP { get; set; }

        /// <summary>
        /// 用户注册区域id
        /// </summary>
        public int RegisterRgId { get; set; }

        ///<summary>
        ///用户性别(0代表未知，1代表男，2代表女)
        ///</summary>
        public int Gender { get; set; }

        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string RealName { get; set; }

        ///<summary>
        ///用户出生日期
        ///</summary>
        public DateTime Bday { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        ///<summary>
        ///区域id
        ///</summary>
        public int RegionId { get; set; }

        ///<summary>
        ///所在地
        ///</summary>
        public string Address { get; set; }

        ///<summary>
        ///简介
        ///</summary>
        public string Bio { get; set; }
    }
}
