using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySales.Model.Entity
{

    /// <summary>
    /// 用户上传的附件资源，统一进行管理，比如图片、文档、压缩包等等
    /// </summary>
    [Table("ATTACHMENT")]
    public class Attachment
    {
        [Key]
        public long ID { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        [Column("ATTACHMENT_TYPE")]
        public string AttachmentType { get; set; }
        /// <summary>
        /// 关联ID
        /// </summary>
        [Column("RELATED_ID")]
        public int RelatedID { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [Column("ATTACHMENT_NAME")]
        public string AttachmentName { get; set; }
        /// <summary>
        /// 物理路径，包含guid路径的全名称
        /// </summary>
        [Column("PHYSICAL_PATH")]
        public string PhysicalPath { get; set; }
        [Column("UPLOAD_TIME")]
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// 上传人
        /// </summary>
        public int UserID { get; set; }
        public string Remark { get; set; }
    }
}
