using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDemo.Model
{
    public class MenuModel
    {
        public string ID { get; set; }
        public string PARENT_ID { get; set; }
        public int ORDER_INDEX { get; set; }
        public string NAME { get; set; }
        public string URL { get; set; }
        public string VERSION { get; set; }
        public bool DISABLED { get; set; }
        public int ROLE_ID { get; set; }
        public string ROLE_NAME { get; set; }
    }

}
