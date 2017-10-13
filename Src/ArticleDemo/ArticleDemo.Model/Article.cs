using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.Model
{
    public class Article
    {
        public int ID { get; set; }
        public int Cate_id { get; set; }
        public string Cate_Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Update_Time { get; set; }
        public int Create_User { get; set; }
        public string User_Name { get; set; }
    }
}
