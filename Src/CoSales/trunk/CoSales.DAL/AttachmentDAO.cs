using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.DAL
{
    public class AttachmentDAO
    {
        public static readonly AttachmentDAO DAO = new AttachmentDAO();

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(Attachment entity)
        {
            return DapperHelper.Insert(entity);
        }

        /// <summary>
        /// 按照关联id寻找所有相关资源
        /// </summary>
        /// <param name="relatedID"></param>
        /// <returns></returns>
        public List<Attachment> GetList(int relatedID)
        {
            object where = new { RelatedID = relatedID };
            return DapperHelper.GetList<Attachment>(where).ToList();
        }
    }
}
