using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutoMapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // PO->DTO 的人工转换
            StudentPO po = new StudentPO() { ID = 1, FirstName = "Money", LastName = "Wang", Password = "admin", UserID = "imwyw" };

            StudentDTO dto = new StudentDTO()
            {
                FirstName = po.FirstName,
                LastName = po.LastName,
                UserID = po.UserID
            };

            //如果是在web项目中，只要初始化一次即可，不需要每次调用前进行初始化
            // 相对于MVC项目的话，我们将初始化放在Application_Start()方法中即可
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<StudentPO, StudentDTO>().ReverseMap();
                //cfg.CreateMap<StudentDTO,StudentPO>();

                cfg.CreateMap<PerEn, PerZh>()
                .ForMember("ZhName", opt => { opt.MapFrom(src => src.EnName); })
                .ReverseMap();
            });
            //AutoMapper.Mapper.AssertConfigurationIsValid();

            StudentDTO dto1 = AutoMapper.Mapper.Map<StudentPO, StudentDTO>(po);
            StudentPO po1 = AutoMapper.Mapper.Map<StudentDTO, StudentPO>(dto1);

            PerZh pzh = AutoMapper.Mapper.Map<PerEn, PerZh>(new PerEn() { Age = 12, EnName = "jack" });
            PerEn pen = AutoMapper.Mapper.Map<PerZh, PerEn>(new PerZh() { Age = 120, ZhName = "张三丰" });
        }
    }

    /// <summary>
    /// Persistence Object
    /// 模拟PO，库表映射对象
    /// </summary>
    public class StudentPO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public DateTime Birth { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }

    /// <summary>
    /// DTO Data Transport Object 数据传输对象
    /// 轻量级，并不是PO的所有属性
    /// </summary>
    public class StudentDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserID { get; set; }
        public string Remark { get; set; }
    }

    public class PerEn
    {
        public int Age { get; set; }
        public string EnName { get; set; }
    }

    public class PerZh
    {
        public int Age { get; set; }
        public string ZhName { get; set; }
    }
}
