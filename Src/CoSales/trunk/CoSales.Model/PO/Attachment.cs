using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    /// <summary>
    /// 附件资源，比如上传的图片、文档等
    /// </summary>
    [Dapper.Table("T_ATTACHMENT")]
    public class Attachment
    {
        [Dapper.Key]
        public int ID { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public string AttType { get; set; }
        /// <summary>
        /// 关联ID
        /// </summary>
        public int RelatedID { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string AttName { get; set; }
        /// <summary>
        /// 文件名称，包含guid路径的全名称
        /// </summary>
        public string AttPath { get; set; }
        public DateTime? UpTime { get; set; }
        public int UserID { get; set; }
        public string Remark { get; set; }
    }
}
