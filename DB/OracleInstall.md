<!-- TOC -->

- [Oracle环境](#oracle环境)
    - [Oracle Server安装](#oracle-server安装)
    - [新建实例](#新建实例)
    - [配置监听](#配置监听)
    - [用户操作](#用户操作)
    - [客户端的安装](#客户端的安装)
    - [PL/SQL developer](#plsql-developer)
    - [疑难解答](#疑难解答)
        - [ORA-12541](#ora-12541)

<!-- /TOC -->

<a id="markdown-oracle环境" name="oracle环境"></a>
# Oracle环境
<a id="markdown-oracle-server安装" name="oracle-server安装"></a>
## Oracle Server安装
下载 http://www.oracle.com/technetwork/cn/database/enterprise-edition/downloads/index.html

按照默认过程进行安装即可，以下为对应步骤的图示：

![](..\assets\Oracle\oracle-install-1.png)

![](..\assets\Oracle\oracle-install-2.png)

![](..\assets\Oracle\oracle-install-3.png)

![](..\assets\Oracle\oracle-install-4.png)

![](..\assets\Oracle\oracle-install-6.png)

<a id="markdown-新建实例" name="新建实例"></a>
## 新建实例
开始菜单中打开【Oracle-OraDb11g_home1】-【配置和移植工具】-【DataBase Configuration Assistant】数据库配置助手进行新建实例

![](..\assets\Oracle\oracle-db-create-config.png)

![](..\assets\Oracle\oracle-db-create-0.png)

![](..\assets\Oracle\oracle-db-create-1.png)

![](..\assets\Oracle\oracle-db-create-2.png)

![](..\assets\Oracle\oracle-db-create-3.png)

![](..\assets\Oracle\oracle-db-create-4.png)

![](..\assets\Oracle\oracle-db-create-5.png)

![](..\assets\Oracle\oracle-db-create-6.png)

![](..\assets\Oracle\oracle-db-create-7.png)

![](..\assets\Oracle\oracle-db-create-8.png)

![](..\assets\Oracle\oracle-db-create-9.png)

![](..\assets\Oracle\oracle-db-create-10.png)

![](..\assets\Oracle\oracle-db-create-11.png)

![](..\assets\Oracle\oracle-db-create-12.png)

正在安装中。。。

![](..\assets\Oracle\oracle-db-create-ing.png)

安装完成【退出】即可

![](..\assets\Oracle\oracle-db-create-finish.png)

<a id="markdown-配置监听" name="配置监听"></a>
## 配置监听
开始菜单中打开【Oracle-OraDb11g_home1】-【配置和移植工具】-【Net Configuration Assistant】网络配置

![](..\assets\Oracle\oracle-net-config-assistant.png)

选择【监听程序】进行配置

![](..\assets\Oracle\oracle-net-listen-1.png)

![](..\assets\Oracle\oracle-net-listen-2.png)

![](..\assets\Oracle\oracle-net-listen-3.png)

![](..\assets\Oracle\oracle-net-listen-4.png)

![](..\assets\Oracle\oracle-net-listen-5.png)

![](..\assets\Oracle\oracle-net-listen-6.png)

![](..\assets\Oracle\oracle-net-listen-7.png)

![](..\assets\Oracle\oracle-net-listen-8.png)

再进行【本地网络服务名】的配置

![](..\assets\Oracle\oracle-net-listen-srv-1.png)

![](..\assets\Oracle\oracle-net-listen-srv-2.png)

![](..\assets\Oracle\oracle-net-listen-srv-3.png)

![](..\assets\Oracle\oracle-net-listen-srv-4.png)

![](..\assets\Oracle\oracle-net-listen-srv-5.png)

![](..\assets\Oracle\oracle-net-listen-srv-6.png)

![](..\assets\Oracle\oracle-net-listen-srv-7.png)

![](..\assets\Oracle\oracle-net-listen-srv-8.png)

<a id="markdown-用户操作" name="用户操作"></a>
## 用户操作
做好以上准备后，我们可以针对数据添加用户并授权，以下操作均在服务端进行

![](..\assets\Oracle\create-user-1.png)

打开sqlplus，使用安装Oracle时设置的密码进行登录：

![](..\assets\Oracle\create-user-login.png)

上图中system为默认系统用户，可以用来进行用户的相关操作

以下脚本是用户相关的操作：
```sql
-- 查看数据库中所有用户的名称;
select username from dba_users;

-- 新建用户 用户名为【new_user_name】，密码为【new_password】
create user new_user_name identified by new_password;

-- 修改用户密码 将用户【new_user_name】重置为【new_password】
alter user new_user_name identified by new_password;

-- 删除用户
drop user new_user_name;

-- 同时删除与【new_user_name】相关的表;
drop user new_user_name cascade;
```


> https://blog.csdn.net/love_legain/article/details/54291400

<a id="markdown-客户端的安装" name="客户端的安装"></a>
## 客户端的安装
客户端这里推荐安装32位的版本：【win32_11gR2_client.zip】

在win10安装会有此提示，可以忽略

![](..\assets\Oracle\oracle-client-win10.png)

进行客户端的默认安装

![](..\assets\Oracle\oracle-client-install-1.png)

![](..\assets\Oracle\oracle-client-install-2.png)

![](..\assets\Oracle\oracle-client-install-4.png)

![](..\assets\Oracle\oracle-client-install-5.png)

<a id="markdown-plsql-developer" name="plsql-developer"></a>
## PL/SQL developer 

打开PLSQL Developer，选择Tools -> perference -> Connection，配置其中的Oracle Home和OCI Library项，如下图所示：

Oracle Home：E:\app\Administrator\product\11.2.0\client_1

OCI Library：E:\app\Administrator\product\11.2.0\client_1\oci.dll

tsname.ora配置：

```sql
orcl =
  (DESCRIPTION =
    (ADDRESS_LIST =
      (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.0.60)(PORT = 1521))
    )
    (CONNECT_DATA =
      (SERVER = DEDICATED)
      (SERVICE_NAME = orcl)
    )
  )
```

<a id="markdown-疑难解答" name="疑难解答"></a>
## 疑难解答
<a id="markdown-ora-12541" name="ora-12541"></a>
### ORA-12541
连接Oracle时报错ORA-12541: TNS: 无监听程序

需要检查服务端的监听程序配置
