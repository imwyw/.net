using ArticleDemo.DAL;
using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace ArticleDemo.BLL
{
    public class MenuActionMgr
    {
        /// <summary>
        /// 获取当前版本的全量菜单
        /// </summary>
        /// <returns></returns>
        public static List<MenuModel> GetMenuList()
        {
            return CacheValue<List<MenuModel>>("MenuList", MenuActionDao.GetMenuList);
        }

        /// <summary>
        /// 获取所有权限对应的菜单项，包含权限ID和名称
        /// </summary>
        /// <returns></returns>
        public static List<MenuModel> GetMenuListRoles()
        {
            return CacheValue<List<MenuModel>>("MenuListRole", MenuActionDao.GetMenuListRoles);
        }

        /// <summary>
        /// 当前用户可访问的权限，通过GetMenuListRoles和当前用户的ROLE_ID进行匹配筛选返回
        /// </summary>
        /// <returns></returns>
        public static List<MenuModel> GetMenuListSelf()
        {
            List<MenuModel> res = null;
            if (ContextObjects.CurrentUser != null)
            {
                int roleid = ContextObjects.CurrentUser.ROLE_ID;
                List<MenuModel> menus = GetMenuListRoles();
                res = menus
                    .Where(t => t.ROLE_ID == roleid)
                    .OrderBy(t => t.ORDER_INDEX).ToList();
            }
            return res;
        }

        /// <summary>
        /// Cache操作封装
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func">需要保存到缓存的变量，定义一个委托接收</param>
        /// <returns></returns>
        private static T CacheValue<T>(string key, Func<object> func) where T : class
        {
            //通过缓存Cache设置获取
            if (HttpRuntime.Cache.Get(key) == null)
            {
                //相对时间过期，此处测试方便，先100秒过期，后期再更改 TODO
                HttpRuntime.Cache.Insert(key, func(), null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(100));
            }
            return HttpRuntime.Cache.Get(key) as T;
        }
    }
}
