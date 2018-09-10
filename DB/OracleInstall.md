<!-- TOC -->

- [Oracle环境](#oracle环境)
    - [Oracle Server安装](#oracle-server安装)
    - [新建实例](#新建实例)
    - [配置监听](#配置监听)
    - [客户端的安装](#客户端的安装)
    - [PL/SQL developer](#plsql-developer)

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
开始菜单中打开【Oracle-OraDb11g_home1】-【配置和移植工具】-【Net Manager】网络配置

![](..\assets\Oracle\oracle-net-manager.png)

<a id="markdown-客户端的安装" name="客户端的安装"></a>
## 客户端的安装
【win64_11gR2_client.zip】在win10安装会有此提示，可以忽略

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
