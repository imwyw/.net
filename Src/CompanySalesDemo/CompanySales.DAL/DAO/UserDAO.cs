using CompanySales.Model.Entity;
using CompanySales.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanySales.Common;
using System.IO;

namespace CompanySales.DAL
{
    public class UserDAO
    {
        public static User Login(string uid, string pwd)
        {
            using (SaleContext db = new SaleContext())
            {
                var res = db.Users
                    .Where(t => t.UserId == uid && t.Password == pwd)
                    .FirstOrDefault();
                return res;
            }
        }

        public static bool AddUser(User user)
        {
            using (SaleContext db = new SaleContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
        }

        public static bool AddBatchUser(List<User> list)
        {
            using (SaleContext db = new SaleContext())
            {
                if (null == list || list.Count == 0)
                {
                    return false;
                }
                db.Users.AddRange(list);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        ///  更新用户头像
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool UpdateImage(User entity)
        {
            using (SaleContext db = new SaleContext())
            {
                var dbEntity = db.Users.Find(entity.ID);
                dbEntity.HeadImg = entity.HeadImg;

                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 根据主键获取用户实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User GetUserById(int id)
        {
            using (SaleContext db = new SaleContext())
            {
                User res = db.Users.Find(id);
                return res;
            }
        }

        /// <summary>
        /// 更新用户信息，但不包含头像，头像单独进行更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool UpdateInfo(User entity)
        {
            using (SaleContext db = new SaleContext())
            {
                var dbEntity = db.Users.Find(entity.ID);
                if (null == dbEntity)
                {
                    Log4Helper.InfoLog.Warn("未找到该user id：" + entity.ID);
                    return false;
                }
                else
                {
                    dbEntity.Name = entity.Name;
                    dbEntity.Address = entity.Address;
                    dbEntity.BirthDate = entity.BirthDate;
                    dbEntity.Gender = entity.Gender;

                    // 用户角色和头像需要单独进行设置，TODO
                    //dbEntity.Roles = entity.Roles;
                    //dbEntity.HeadImg = entity.HeadImg;

                    db.SaveChanges();
                    return true;
                }
            }
        }

        /// <summary>
        /// 重新数据库，执行【InitTables.sql】脚本
        /// </summary>
        /// <returns></returns>
        public static bool RebuildData()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_Script", "InitTables.sql");
            string sql = File.ReadAllText(path);

            using (SaleContext db = new SaleContext())
            {
                db.Database.ExecuteSqlCommand(sql);
                return true;
            }
        }
    }
}
