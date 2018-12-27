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

        /// <summary>
        /// 根据id返回实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Attachment Get(int id)
        {
            return DapperHelper.Get<Attachment>(id);
        }

        /// <summary>
        /// 按主键ID进行删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteById(int id)
        {
            return DapperHelper.Delete<Attachment>(id);
        }
    }
}
