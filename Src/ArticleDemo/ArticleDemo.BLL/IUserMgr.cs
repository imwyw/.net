using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.BLL
{
    public interface IUserMgr
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Add(User user);

        /// <summary>
        /// 登录验证，是否有该用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        User Login(string name, string pwd);

        /// <summary>
        /// 检查该用户名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool CheckExist(string name);
    }
}
