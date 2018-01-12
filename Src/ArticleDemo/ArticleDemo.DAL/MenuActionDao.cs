using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.DAL
{
    public class MenuActionDao
    {
        public static List<MenuModel> GetMenuList()
        {
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                var res = context.Database.SqlQuery<MenuModel>("SELECT * FROM T_MENUS WHERE DISABLED = 0 AND VERSION='V1' ORDER BY ORDER_IDNEX").ToList();
                return res;
            }
        }

        public static List<MenuModel> GetMenuListRoles()
        {
            using (ARTICLE_DBEntities context = new ARTICLE_DBEntities())
            {
                //很多参数先写死吧
                var res = context.Database.SqlQuery<MenuModel>(@"
                    WITH    MENU
                              AS ( SELECT   *
                                   FROM     DBO.T_MENUS
                                   WHERE    VERSION = 'V1'
                                            AND DISABLED = 0
                                 )
                        SELECT  M.ID ,
                                M.PARENT_ID ,
                                M.ORDER_IDNEX ,
                                M.NAME ,
                                M.URL ,
                                M.VERSION ,
                                RO.ID ROLE_ID ,
                                RO.NAME ROLE_NAME
                        FROM    DBO.T_ROLES_MENUS A
                                INNER JOIN MENU M ON A.MENU_ID = M.ID
                                INNER JOIN DBO.T_ROLES RO ON A.ROLE_ID = RO.ID
                        ORDER BY M.ORDER_IDNEX").ToList();

                return res;
            }
        }
    }
}
