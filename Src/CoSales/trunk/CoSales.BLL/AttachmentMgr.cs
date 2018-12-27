using CoSales.DAL;
using CoSales.Model;
using CoSales.Model.PO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CoSales.BLL
{
    public class AttachmentMgr
    {
        public static readonly AttachmentMgr Mgr = new AttachmentMgr();

        public bool Insert(Attachment entity)
        {
            var res = AttachmentDAO.DAO.Insert(entity);
            // 主键ID
            entity.ID = res;
            return res > 0;
        }

        /// <summary>
        /// 上传文件，目前只考虑单个文件上传
        /// </summary>
        /// <param name="fileBase">客户端上传的文件</param>
        /// <param name="relatedID">关联主键id</param>
        /// <param name="attachType">附件资源类型</param>
        /// <param name="backPath">上一级目录 ContextObjects类中配置，TODO仍需优化 </param>
        /// <returns></returns>
        public LayStateModel AddAttachment(HttpFileCollectionBase fileBase, int relatedID, string attachType, string backPath)
        {
            LayStateModel state = new LayStateModel();

            if (fileBase.Count == 0)
            {
                state.code = -1;
                state.msg = "未能接受到上传文件，请重新检查上传";
                return state;
            }
            else
            {
                HttpPostedFileBase file = fileBase[0];

                // 用户端上传的文件名称
                string fileName = file.FileName;

                // 校验服务端保存的路径是否存在
                if (!Directory.Exists(backPath))
                {
                    Directory.CreateDirectory(backPath);
                }

                // 修改文件名称为用户id，使用guid作为文件名称保存，防止文件名称冲突
                string extension = Path.GetExtension(fileName);
                string fullName = Path.Combine(backPath, Guid.NewGuid() + extension);

                // 另存为至服务器上指定路径
                file.SaveAs(fullName);

                Attachment entity = new Attachment();
                entity.AttName = fileName;
                entity.AttPath = fullName;
                entity.AttType = attachType;
                entity.UserID = ContextObjects.CurrentUser.ID;
                entity.RelatedID = relatedID;
                entity.UpTime = DateTime.Now;

                if (!Insert(entity))
                {
                    state.code = -1;
                    state.msg = "上传成功，但更新失败，请重新上传！";
                }
                else
                {
                    // 返回主键id和全路径名称
                    state.data = new { src = fullName, id = entity.ID };
                    state.msg = "上传成功！";
                }
                return state;
            }
        }//AddAttachment end

        public List<Attachment> GetList(int relatedID)
        {
            var res = AttachmentDAO.DAO.GetList(relatedID);
            return res;
        }

        /// <summary>
        /// 按主键ID进行删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(int id)
        {
            Attachment entity = AttachmentDAO.DAO.Get(id);
            bool res = AttachmentDAO.DAO.DeleteById(id) > 0;

            // 数据库删掉后，再从磁盘里删掉
            if (File.Exists(entity.AttPath) && res)
            {
                File.Delete(entity.AttPath);
            }

            return res;
        }
    }
}
