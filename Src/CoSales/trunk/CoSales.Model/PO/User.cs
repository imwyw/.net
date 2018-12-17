using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    [Dapper.Table("T_USER")]
    public class User
    {
        [Dapper.Key]
        public int ID { get; set; }

        public string UserID { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int RoleID { get; set; }
        public string Remark { get; set; }
    }
}
