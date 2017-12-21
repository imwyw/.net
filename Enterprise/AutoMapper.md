# AutoMapper

> http://automapper.org/

这是一款DTO的映射工具，AutoMapper是基于对象到对象约定的映射工具，它可以把复杂的对象模型转为DTO，或者其他的--那些让设计更合理更适于序列化、通信、传递消息的简单对象或者干脆就只是在领域层与应用层之间搭建一个简单的ACL防护层(就像DTO一样，用于代码的显示转换)来增加各自层的相互独立性。

简单来说就是：就是根据A的模型和B的模型中的定义，自动将A模型映射为一个全新的B模型。

## 前言
### VO、DTO、DO、PO、DAO
* VO(View Object)：视图对象，用于展示层，它的作用是把某个指定页面(或组件)的所有数据封装起来。 
* DTO(Data Transfer Object)：数据传输对象，泛指用于展示层与服务层之间的数据传输对象。 
* DO(Domain Object)：领域对象，就是从现实世界中抽象出来的有形或无形的业务实体。 
* PO(Persistent Object)：持久化对象，它跟持久层(通常是关系型数据库)的数据结构形成一一对应的映射关系，如果持久层是关系型数据库，那么，数据表中的每个字段(或若干个)就对应PO的一个(或若干个)属性。
* DAO(Data Access Object):数据访问对象，主要用来封装对数据库的操作。

### 为什么要有DTO
通常我们通过DAO获取PO，PO是和数据库映射的，但是可能包含了很多对于传输来说并不需要的属性。

比如一张表有100个字段，那么对应的PO可能就是100个属性，但是对于表示层而言并不需要那么多属性展示给用户，同样的也不应该把底层表结构暴露给表示层，那么中间就有一个DTO对象的转换，表示层需要多少属性则DTO的设置就定义多少属性。

## 应用场景
### 人工转换
我们经常需要有这样的转换，将PO模型转换为实际传递需要的DTO模型，如果人工方式实现就会有一堆如下繁琐、效率低下的代码：
```cs
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
    }
```

### AutoMapper安装
同样，我们通过NuGet程序包管理器进行安装引用：

![](..\assets\Enterprise\AutoMapper_nuget1.png)

### 默认映射
> AutoMapper uses a convention-based matching algorithm to match up source to destination values.

在使用AutoMapper进行映射对象前，还需要initialize AutoMapper，参考官网提欧的方法：

> http://automapper.readthedocs.io/en/latest/Static-and-Instance-API.html

我们以官网中提供的static api作为示例:
```cs
class Program
{
    static void Main(string[] args)
    {
        MapperInit();

        StudentPO po = new StudentPO() { ID = 1, FirstName = "Money", LastName = "Wang", Password = "admin", UserID = "imwyw" };

        //使用Mapper 将po转换为dto
        StudentDTO dto1 = AutoMapper.Mapper.Map<StudentPO, StudentDTO>(po);

        //使用Mapper 将dto转换为po，没有的属性赋默认值
        StudentPO po1 = AutoMapper.Mapper.Map<StudentDTO, StudentPO>(new StudentDTO() { FirstName = "First", LastName = "Last", UserID = "samsung" });
    }

    /// <summary>
    /// Mapper初始化，在程序启动的时候进行一次初始化
    /// You now must use either Mapper.Initialize or new MapperConfiguration() to initialize AutoMapper. 
    /// If you prefer to keep the static usage, use Mapper.Initialize.
    /// </summary>
    static void MapperInit()
    {
        //static api 我们使用静态方法进行初始化
        AutoMapper.Mapper.Initialize(cfg =>
        {
            cfg.CreateMap<StudentPO, StudentDTO>();
            cfg.CreateMap<StudentDTO, StudentPO>();
        });

        //在程序启动时对所有的配置进行严格的验证
        AutoMapper.Mapper.AssertConfigurationIsValid();
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
}
```

### Reverse Mapping
反向映射，在上述的示例中，进行了PO->DTO和DTO->PO转换的映射，我们也可以通过反向映射来代替：
```cs
//static api 我们使用静态方法进行初始化
AutoMapper.Mapper.Initialize(cfg =>
{
    cfg.CreateMap<StudentPO, StudentDTO>().ReverseMap();
    //cfg.CreateMap<StudentDTO, StudentPO>(); 不再需要
});
```

### Customizing reverse mapping
默认的映射有时候可能并不符合我们的需求，对于以下属性名称并不相同的映射需求，我们可以ForMember()自定义映射关系：
```cs
class Program
{
    static void Main(string[] args)
    {
        AutoMapper.Mapper.Initialize(cfg =>
        {
            cfg.CreateMap<StudentPO, StudentDTO>()
            .ForMember(des => des.EnName, op => op.MapFrom(src => src.ZhName))
            //.ForMember("EnName", op => op.MapFrom(src => src.ZhName)) ForMember重载，作用同上
            .ReverseMap();
        });

        StudentDTO dto = AutoMapper.Mapper.Map<StudentPO, StudentDTO>(new StudentPO() { ZhName = "杰克" });
        StudentPO po = AutoMapper.Mapper.Map<StudentDTO, StudentPO>(new StudentDTO() { EnName = "jack" });
    }
}

public class StudentPO
{
    public string ZhName { get; set; }
}
public class StudentDTO
{
    public string EnName { get; set; }
}
```


在映射对象时，我们还可以进行自定义的投影操作，可以在lambda表达式中定义我们需要的任意投影需要：
```cs
class Program
{
    static void Main(string[] args)
    {
        AutoMapper.Mapper.Initialize(cfg =>
        {
            cfg.CreateMap<BirthInfo, BirthDto>()
            .ForMember(des => des.BirthYear, op => op.MapFrom(src => src.Birth.Year))
            .ForMember(des => des.BirthMonth, op => op.MapFrom(src => src.Birth.Month))
            .ForMember(des => des.BirthDay, op => op.MapFrom(src => src.Birth.Day));
        });

        BirthDto dto = AutoMapper.Mapper.Map<BirthInfo, BirthDto>(new BirthInfo() { Name = "Jack", Birth = Convert.ToDateTime("1900-9-19") });
    }
}

public class BirthInfo
{
    public string Name { get; set; }
    public DateTime Birth { get; set; }
}
public class BirthDto
{
    public string Name { get; set; }
    public int BirthYear { get; set; }
    public int BirthMonth { get; set; }
    public int BirthDay { get; set; }
}
```

---

参考引用：

[浅析VO、DTO、DO、PO的概念、区别和用处](http://blog.csdn.net/zjrbiancheng/article/details/6253232)

[AutoMapper-docs](http://automapper.readthedocs.io/en/latest/index.html)