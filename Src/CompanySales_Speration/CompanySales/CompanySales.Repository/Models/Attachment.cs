using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanySales.Repository.Models
{
    public partial class Attachment
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("ATTACHMENT_TYPE")]
        [StringLength(20)]
        public string AttachmentType { get; set; }
        [Column("RELATED_ID")]
        public int RelatedId { get; set; }
        [Column("ATTACHMENT_NAME")]
        [StringLength(100)]
        public string AttachmentName { get; set; }
        [Column("PHYSICAL_PATH")]
        [StringLength(100)]
        public string PhysicalPath { get; set; }
        [Column("UPLOAD_TIME", TypeName = "datetime")]
        public DateTime? UploadTime { get; set; }
        [Column("USERID")]
        public int Userid { get; set; }
        [Column("REMARK")]
        [StringLength(100)]
        public string Remark { get; set; }
    }
}
