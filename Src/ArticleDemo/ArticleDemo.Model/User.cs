using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.Model
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class User
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 中文昵称，此阶段需要与数据库对应
        /// </summary>
        public string Zh_Name { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 密码是否为密文，当自动登录时即为密文
        /// </summary>
        public bool IsCipherPwd { get; set; }

        /// <summary>
        /// 该用户具有的角色，多个角色用逗号进行分割
        /// 结合拦截器的控制，简单权限方式 继续保留
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// 角色ID，为实现动态权限添加
        /// add at 2018-1-2
        /// </summary>
        public int ROLE_ID { get; set; }

    }
}
