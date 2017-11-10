<!-- TOC -->

- [DB](#db)
    - [SqlServer2012的安装](#sqlserver2012的安装)
    - [配置](#配置)
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
安装完成后打开【SQL Server ManagementStudio】连接工具，显示如下登录窗口，使用windows身份验证登录或者使用第13步安装配置的密码进行登录。

![](..\assets\SqlServer\Config_1.png)

![](..\assets\SqlServer\Config_2.png)

<a id="markdown-常见问题" name="常见问题"></a>
## 常见问题
<a id="markdown-无法登陆" name="无法登陆"></a>
### 无法登陆
- 检查数据库服务是否开启  运行 services.msc查看SQL Server服务是否正在运行；或通过SQL Server配置管理器查看服务是否正在运行；

![](..\assets\SqlServer\sql_server_1.png)
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
