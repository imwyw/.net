using ArticleDemo.BLL;
using ArticleDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ArticleDemo.WebUI.Handlers
{
    /// <summary>
    /// ArticleHandler 的摘要说明
    /// </summary>
    public class ArticleHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string methodName = context.Request.PathInfo.Substring(1);
            MethodInfo method = GetType().GetMethod(methodName);
            method.Invoke(this, new object[] { context });
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void GetArticles(HttpContext context)
        {
            List<Article> list = ArticleMgr.GetArticles();

            //拼接json字符串
            StringBuilder builder = new StringBuilder();
            builder.Append("[");//[

            foreach (Article item in list)
            {
                builder.Append("{");// { 'name':'jack','age':12 },{ 'name':'jack','age':12 },{ 'name':'jack','age':12 },
                builder.AppendFormat("\"ID\":\"{0}\",\"Cate_Name\":\"{1}\",\"Title\":\"{2}\",\"Content\":\"{3}\",\"Update_Time\":\"{4}\",\"User_Name\":\"{5}\"",
                    item.ID, item.Cate_Name, item.Title, item.Content, item.Update_Time.ToString(), item.User_Name);
                builder.Append("},");
            }

            // [{ 'name':'jack','age':12 },{ 'name':'jack','age':12 },{ 'name':'jack','age':12 },
            // [{ 'name':'jack','age':12 },{ 'name':'jack','age':12 },{ 'name':'jack','age':12 }]
            context.Response.Write(builder.ToString().Substring(0, builder.ToString().Length - 1) + "]");

            // "123,"->"1234"
            /*string str = "123,";
            str.Substring(0, 3);//123
            str.Substring(0, str.Length - 1) + "4";
            */
        }

        public void AddArticle(HttpContext context)
        {
            string cateid = context.Request["cateid"];
            string title = context.Request["title"];
            string content = context.Request["content"];

            Article entity = new Article();
            entity.Cate_id = int.Parse(cateid);
            entity.Title = title;
            entity.Content = content;

            entity.Update_Time = DateTime.Now;
            //从session中获取当前用户
            User curUser = context.Session["CurUser"] as User;
            entity.Create_User = curUser.ID;

            bool res = ArticleMgr.Add(entity);
            if (res)
            {
                context.Response.Write("{\"status\":true}");
            }
            else
            {
                context.Response.Write("{\"status\":false}");
            }
        }
    }
}