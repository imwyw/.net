using CompanySales.Common;
using CompanySales.DAL;
using CompanySales.Model;
using CompanySales.Model.Domain;
using CompanySales.Model.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CompanySales.BLL
{
    public class AttachmentMgr
    {
        public static bool Add(Attachment entity)
        {
            return AttachmentDAO.Add(entity);
        }

        /// <summary>
        /// 上传文件，目前只考虑单个文件上传
        /// </summary>
        /// <param name="fileBase">客户端上传的文件</param>
        /// <param name="relatedID">关联主键id</param>
        /// <param name="attachType">附件资源类型</param>
        /// <returns></returns>
        public static StateModel AddAttachment(HttpFileCollectionBase fileBase, int relatedID, AttachmentType aType)
        {
            StateModel state = new StateModel();

            if (fileBase.Count == 0)
            {
                state.Code = -1;
                state.Message = "未能接受到上传文件，请重新检查上传";
                return state;
            }
            else
            {
                HttpPostedFileBase file = fileBase[0];

                // 用户端上传的文件名称
                string fileName = file.FileName;

                // 文件对应保存的目录
                string dir = GetPhysicalDirectory(aType);

                // 校验服务端保存的路径是否存在
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // 修改文件名称为用户id，使用guid作为文件名称保存，防止文件名称冲突
                string extension = Path.GetExtension(fileName);
                string fullName = Path.Combine(dir, Guid.NewGuid() + extension);

                // 另存为至服务器上指定路径
                file.SaveAs(fullName);

                Attachment entity = new Attachment();
                entity.ID = IdGenerator.GeneratorInt64();
                entity.AttachmentName = fileName;
                entity.PhysicalPath = fullName;
                entity.AttachmentType = aType.ToString();
                entity.UserID = ContextObject.CurrentUser.ID;
                entity.RelatedID = relatedID;
                entity.UploadTime = DateTime.Now;

                if (!Add(entity))
                {
                    state.Code = -1;
                    state.Message = "上传成功，但更新失败，请重新上传！";
                }
                else
                {
                    // 返回主键id和全路径名称
                    state.Data = new { src = fullName, id = entity.ID };
                    state.Message = "上传成功！";
                }
                return state;
            }
        }//AddAttachment end

        public static List<Attachment> GetListByRelatedId(int relatedID)
        {
            var res = AttachmentDAO.GetListByRelatedId(relatedID);
            return res;
        }

        /// <summary>
        /// 按主键ID进行删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteById(int id)
        {
            Attachment entity = AttachmentDAO.Get(id);
            bool res = AttachmentDAO.DeleteById(id);

            // 数据库删掉后，再从磁盘里删掉
            if (File.Exists(entity.PhysicalPath) && res)
            {
                File.Delete(entity.PhysicalPath);
            }

            return res;
        }

        /// <summary>
        /// 根据资源的枚举类型确定保存的目录
        /// 返回对应资源类型的目录
        /// </summary>
        /// <param name="aType"></param>
        /// <returns></returns>
        public static string GetPhysicalDirectory(AttachmentType aType)
        {
            switch (aType)
            {
                case AttachmentType.ProductImage:
                    return ContextObject.ProductImagePath;
                default:
                    return ContextObject.BaseFilePath;
            }
        }

    }
}
