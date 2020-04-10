using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanySales.Repository.Models
{
    [Table("users")]
    public partial class User
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
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
        [StringLength(200)]
        public string HeadImg { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? BirthDate { get; set; }
        [StringLength(2)]
        public string Gender { get; set; }
    }
}
