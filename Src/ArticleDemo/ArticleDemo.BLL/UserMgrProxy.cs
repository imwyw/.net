using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleDemo.Model;
using ArticleDemo.Common;

namespace ArticleDemo.BLL
{
    public class UserMgrProxy : IUserMgr
    {
        UserMgr mgr = null;
        public UserMgrProxy()
        {
            mgr = new UserMgr();
        }
        public bool Add(User user)
        {
            LogHelper.Log("添加用户", user.Name);
            return mgr.Add(user);
        }

        public bool CheckExist(string name)
        {
            LogHelper.Log("检查用户名称是否冲突", name);
            return mgr.CheckExist(name);
        }

        public User Login(string name, string pwd, bool isCipherPwd = false)
        {
            LogHelper.Log("登录", name);
            return mgr.Login(name, pwd, isCipherPwd);
        }
    }
}
