<!-- TOC -->

- [DB](#db)
    - [SqlServer2012的安装](#sqlserver2012的安装)
    - [配置](#配置)
        - [SQL Server ManagementStudio](#sql-server-managementstudio)
        - [配置管理器](#配置管理器)
    - [常见问题](#常见问题)
        - [无法登陆](#无法登陆)
        - [已安装vs2010无法安装sqlserver2012](#已安装vs2010无法安装sqlserver2012)

<!-- /TOC -->
<a id="markdown-db" name="db"></a>
# DB
常见的数据库有

名称 | 开发商 | 场景
---|-----|---
DB2 | IBM | 大型企业
INFORMIX | IBM | 大型企业
Oracle | Oracle甲骨文 | 中大型企业
MySql | Oracle收购 | 中小型企业
SqlServer | Microsoft | 中小型企业
SQLite | 开源 | 轻量级，但性能非常强悍

<a id="markdown-sqlserver2012的安装" name="sqlserver2012的安装"></a>
## SqlServer2012的安装
1. 打开setup.exe，选择安装选项，如下图：
![](..\assets\SqlServer\Install_1.png)

2. 安装程序支持规则检测，点击确定进行下一步，如下图：
![](..\assets\SqlServer\Install_2.png)

3. 输入密钥FH666-Y346V-7XFQ3-V69JM-RHW28，点击下一步：
![](..\assets\SqlServer\Install_3.png)

4. 默认勾选即可，如图：
![](..\assets\SqlServer\Install_4.png)

5. 可以不需要勾选更新，如图：
![](..\assets\SqlServer\Install_5.png)

6. 默认设置，点击下一步：
![](..\assets\SqlServer\Install_6.png)

7. 默认设置，点击下一步：
![](..\assets\SqlServer\Install_7.png)

8. 点击全选，安装所有功能，点击下一步：
![](..\assets\SqlServer\Install_8.png)

9. 默认设置，点击下一步：
![](..\assets\SqlServer\Install_9.png)

10. 此处采用默认实例名称和默认安装路径，默认实例名称不建议更改，点击下一步：
![](..\assets\SqlServer\Install_10.png)

11. 默认设置，点击下一步：
![](..\assets\SqlServer\Install_11.png)

12. 默认设置，点击下一步：
![](..\assets\SqlServer\Install_12.png)

13. 身份验证模式选择混合模式并设置密码，添加当前用户为管理员；数据目录课进行修改，也可保持默认，在这里作默认处理；如下两图：
![](..\assets\SqlServer\Install_13-1.png)
![](..\assets\SqlServer\Install_13-2.png)

14. 添加当前用户权限，点击下一步：
![](..\assets\SqlServer\Install_14.png)

15. 默认设置，点击下一步：
![](..\assets\SqlServer\Install_15.png)

16. 添加当前用户权限，点击下一步：
![](..\assets\SqlServer\Install_16.png)

17. 输入控制器名称，点击下一步：
![](..\assets\SqlServer\Install_17.png)

18. 默认设置，点击下一步：
![](..\assets\SqlServer\Install_18.png)

19. 默认设置，点击下一步：
![](..\assets\SqlServer\Install_19.png)

20. 默认设置，点击安装：
![](..\assets\SqlServer\Install_20.png)

21. 重启计算机，完成安装：
![](..\assets\SqlServer\Install_21.png)

<a id="markdown-配置" name="配置"></a>
## 配置

<a id="markdown-sql-server-managementstudio" name="sql-server-managementstudio"></a>
### SQL Server ManagementStudio
SQL Server Management Studio 是一个集成环境，用于访问、配置、管理和开发 SQL Server 的所有组件。SQL Server Management Studio 组合了大量图形工具和丰富的脚本编辑器，使各种技术水平的开发人员和管理员都能访问 SQL Server。

安装完成后打开【SQL Server ManagementStudio】连接工具，显示如下登录窗口，使用windows身份验证登录或者使用第13步安装配置的密码进行登录。

![](..\assets\SqlServer\Config_1.png)

![](..\assets\SqlServer\Config_2.png)

<a id="markdown-配置管理器" name="配置管理器"></a>
### 配置管理器
SQL Server 配置管理器是一种工具，用于管理与 SQL Server相关联的服务、配置 SQL Server使用的网络协议以及从 SQL Server 客户端计算机管理网络连接配置。 

SQL Server 配置管理器是一种可以通过“开始”菜单访问的 Microsoft 管理控制台管理单元，也可以将其添加到任何其他 Microsoft 管理控制台的显示界面中。 

Microsoft 管理控制台 (mmc.exe) 使用 SQLServerManager<version>.msc 文件（例如 SQL Server 2016 的 SQLServerManager13.msc）打开配置管理器。 

以下是在 C 盘安装 Windows 的情况下最新的四个版本的路径。

版本 | 路径
---|---
SQL Server 2017 | C:\Windows\SysWOW64\SQLServerManager14.msc
SQL Server 2016 | C:\Windows\SysWOW64\SQLServerManager13.msc
SQL Server 2014 | C:\Windows\SysWOW64\SQLServerManager12.msc
SQL Server 2012 | C:\Windows\SysWOW64\SQLServerManager11.msc

![](..\assets\SqlServer\SqlServerConfigurationManager_1.png)

详见：[SQL Server 配置管理器](https://docs.microsoft.com/zh-cn/sql/relational-databases/sql-server-configuration-manager)

<a id="markdown-常见问题" name="常见问题"></a>
## 常见问题

<a id="markdown-无法登陆" name="无法登陆"></a>
### 无法登陆
- 检查数据库服务是否开启  运行 services.msc查看SQL Server服务是否正在运行；或通过SQL Server配置管理器查看服务是否正在运行；

![](..\assets\SqlServer\sql_server_1.png)

- SqlServerConfigurationManager配置管理工具，可以在[C:\Windows\System32\SQLServerManager11.msc]或[C:\Windows\SysWOW64\SQLServerManager11.msc]找到

![](..\assets\SqlServer\sql_server_2.png)

- 是否启用TCP/IP协议

![](..\assets\SqlServer\sql_server_3.png)

- 是否启用混合模式及用户名密码是否正确

![](..\assets\SqlServer\sql_server_4.gif)

- 检查网络是否可以访问，使用ping进行检测

<a id="markdown-已安装vs2010无法安装sqlserver2012" name="已安装vs2010无法安装sqlserver2012"></a>
### 已安装vs2010无法安装sqlserver2012
具体呈现如下：

![](..\assets\SqlServer\faq_vs2010_1.png)

需要安装vs2010 sp1补丁

链接: https://pan.baidu.com/s/1kVuva3H 密码: 8kf5
