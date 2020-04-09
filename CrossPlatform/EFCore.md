<!-- TOC -->

- [EF Core](#ef-core)
    - [安装](#安装)
    - [生成数据实体](#生成数据实体)

<!-- /TOC -->

<a id="markdown-ef-core" name="ef-core"></a>
# EF Core
Entity Framework (EF) Core 是轻量化、可扩展、开源和跨平台版的常用 Entity Framework 数据访问技术。

就是 EntityFramework 的跨平台版本，用法基本一致。

<a id="markdown-安装" name="安装"></a>
## 安装
在Package Manager console中运行以下命令，安装包管理器控制台工具：

```bash
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

<a id="markdown-生成数据实体" name="生成数据实体"></a>
## 生成数据实体
首先确保都安装CLI相关工具引用，在Package Manager console中运行以下命令：

```bash
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.Tools
```

需要使用 Scaffold-DbContext 命令生成数据实体类型，数据表必须有主键！

```bash
Scaffold-DbContext -connection "Server=.;Database=CompanySales;Trusted_Connection=True;uid=sa;pwd=123456;" -provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -context SaleContext -project CompanySales.Repository -force
```

详见 [Scaffold-DbContext](https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/powershell#scaffold-dbcontext)

需要注意的是，如果是多个项目的话，比如三层架构，同样需要在每个项目添加EF相关的依赖。

