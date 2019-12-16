namespace CompanySales.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Users")]
    public partial class User
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 重构-重命名，在输入法英文状态下
        /// ctrl+. 显示快捷操作提示窗口
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string UserId { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Roles { get; set; }

        /// <summary>
        /// 用户上传头像的保存
        /// 实际保存的是图片在服务器保存的全路径名
        /// </summary>
        [StringLength(200)]
        public string HeadImg { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
    }
}
