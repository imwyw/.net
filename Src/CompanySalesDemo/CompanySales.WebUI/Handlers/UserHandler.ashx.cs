using CompanySales.BLL;
using CompanySales.Model.Entity;using CompanySales.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CompanySales.WebUI.Handlers
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// 用于处理和用户相关的操作
    /// </summary>
    public class UserHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.PathInfo.Substring(1);
            // 反射获取方法对象，方法名大小写敏感
            MethodInfo method = GetType().GetMethod(name);
            if (null == method)
            {
                context.Response.Write(null);
                return;
            }
            // 调用反射得到的方法
            method.Invoke(this, new object[] { context });
        }

        public void Login(HttpContext context)
        {
            string uid = context.Request["uid"];
            string pwd = context.Request["pwd"];

            bool success = UserMgr.Login(uid, pwd) != null;
            string json = string.Empty;
            StateModel state = new StateModel(success);
            if (success)
            {
                state.Message = "登录成功";
                //json = "{\"status\":true,\"message\":\"登录成功\"}";
            }
            else
            {
                state.Message = "用户名或密码错误！";
                //json = "{\"status\":false,\"message\":\"用户名不存在\"}";
            }
            context.Response.Write(JsonConvert.SerializeObject(state));
        }

        public void Register(HttpContext context)
        {
            User user = new User();
            user.UserId = context.Request["uid"];
            user.Password = context.Request["pwd"];
            user.Name = context.Request["name"];
            user.Address = context.Request["address"];

            bool isAdd = UserMgr.AddUser(user);
            StateModel state = new StateModel(isAdd);
            if (!state.Status)
            {
                state.Message = "注册发生异常，请联系管理员！";
            }
            context.Response.Write(JsonConvert.SerializeObject(state));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}