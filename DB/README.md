<!-- TOC -->

- [数据库](#数据库)
    - [什么是数据库](#什么是数据库)
    - [数据库分类](#数据库分类)
        - [关系型数据库](#关系型数据库)
        - [非关系型数据库（NoSQL）](#非关系型数据库nosql)
            - [Redis](#redis)
            - [MongoDB](#mongodb)
            - [HBase](#hbase)
        - [关系型和非关系型](#关系型和非关系型)

<!-- /TOC -->

<a id="markdown-数据库" name="数据库"></a>
# 数据库

数据库(Database，DB)是按照数据结构来组织、存储和管理数据的建立在计算机存储设备上的仓库。

现如今，我们所有见到的跟日常生活有关、需要记录的基本全部放在数据库里面：
1. 身份证信息放在公安部的系统
2. 银行卡的余额和交易记录、转帐信息
3. 在酒店的开房信息（所有出现了某些方面的数据库被盗和信息泄漏）
4. 飞机、火车、汽车联网购票记录
5. 各个不同的网站、QQ、网上购物、贴吧、喜欢听的音乐、电影的收藏信息
6. 手机电话机录、余额、公交卡余额、水费、电费、彩票的购买记录
7. 打游戏的装备、等级、魔力、力量、攻击能力等信息

<a id="markdown-什么是数据库" name="什么是数据库"></a>
## 什么是数据库
人们通常用数据库这个术语来代表他们使用的数据库软件。这是不正确的，它是引起混淆的根源。

确切地说，数据库软件应称为DBMS(数据库管理系统)。数据库 是通过 DBMS 创建和操纵的容器。

数据库可以是保存在硬设备 上的文件，但也可以不是。在很大程度上说，数据库究竟是 文件还是别的什么东西并不重要，因为你并不直接访问数据 库；

你使用的是DBMS，它替你访问数据库。

<a id="markdown-数据库分类" name="数据库分类"></a>
## 数据库分类

<a id="markdown-关系型数据库" name="关系型数据库"></a>
### 关系型数据库

RDBMS即关系数据库管理系统(Relational Database Management System)的特点：
1. 数据以表格的形式出现
2. 每行为各种记录名称
3. 每列为记录名称所对应的数据域
4. 许多的行和列组成一张表单
5. 若干的表单组成database

常见的有以下：
* MySQL
* PostgreSQL
* Microsoft Access
* Google Fusion Tables
* SQL Server
* Oracle
* Sybase
* dBASE
* DB2

<a id="markdown-非关系型数据库nosql" name="非关系型数据库nosql"></a>
### 非关系型数据库（NoSQL）
NoSQL(Not Only SQL)一词最早出现于1998年，是Carlo Strozzi开发的一个轻量、开源、不提供SQL功能的关系数据库。

当代典型的关系数据库在一些数据敏感的应用中表现了糟糕的性能，例如为巨量文档创建索引、高流量网站的网页服务，以及发送流式媒体。

关系型数据库的典型实现主要被调整用于执行规模小而读写频繁，或者大批量极少写访问的事务。

<a id="markdown-redis" name="redis"></a>
#### Redis
redis是一个key-value存储系统。

和Memcached类似，它支持存储的value类型相对更多，包括string(字符串)、list(链表)、set(集合)、zset(sorted set --有序集合)和hash（哈希类型）。

这些数据类型都支持push/pop、add/remove及取交集并集和差集及更丰富的操作，而且这些操作都是原子性的。

在此基础上，redis支持各种不同方式的排序。与memcached一样，为了保证效率，数据都是缓存在内存中。

区别的是redis会周期性的把更新的数据写入磁盘或者把修改操作写入追加的记录文件，并且在此基础上实现了master-slave(主从)同步。

Redis 是一个高性能的key-value数据库。 redis的出现，很大程度补偿了memcached这类key/value存储的不足，在部 分场合可以对关系数据库起到很好的补充作用。

它提供了Java，C/C++，C#，PHP，JavaScript，Perl，Object-C，Python，Ruby，Erlang等客户端，使用很方便。

Redis支持主从同步。数据可以从主服务器向任意数量的从服务器上同步，从服务器可以是关联其他从服务器的主服务器。

这使得Redis可执行单层树复制。存盘可以有意无意的对数据进行写操作。由于完全实现了发布/

官网 redis (http://redis.io),被墙....

http://www.redis.com.cn/

<a id="markdown-mongodb" name="mongodb"></a>
#### MongoDB
MongoDB是一个基于分布式文件存储的数据库，介于关系数据库和非关系数据库之间，是非关系数据库当中功能最丰富，最像关系数据库的。

MongoDB最大的特点是他支持的查询语言非常强大，其语法有点类似于面向对象的查询语言，几乎可以实现类似关系数据库单表查询的绝大部分功能，而且还支持对数据建立索引。

MongoDB支持C#，PYTHON，JAVA，C++，PHP等多种语言。

MongoDB是高性能开源文档数据库，也是目前最受关注的NoSQL技术之一，以敏捷、可扩展和对企业应用友好（支持事务，一致性和数据完整性保证，有大企业应用案例）而著称。

有人甚至认为LAMP中的M应该用MongoDB取代MySQL，其火热程度可见一斑。

使用MongoDB的公司包括Foursquare, Craiglist, 迪士尼，SAP，Intuit，EA等，国内淘宝、大众点评、视觉中国等公司有应用。

官网 https://www.mongodb.com/

<a id="markdown-hbase" name="hbase"></a>
#### HBase
HBase(Hadoop Database)，是一个高可靠性、高性能、面向列、可伸缩的分布式存储系统，利用HBase技术可在廉价PC Server上搭建起大规模结构化存储集群。

HBase是Google Bigtable的开源实现，类似Google Bigtable利用GFS作为其文件存储系统，HBase利用Hadoop HDFS作为其文件存储系统；

Google运行MapReduce来处理Bigtable中的海量数据，HBase同样利用Hadoop MapReduce来处理HBase中的海量数据；

Google Bigtable利用 Chubby作为协同服务，HBase利用Zookeeper作为协同服务。

<a id="markdown-关系型和非关系型" name="关系型和非关系型"></a>
### 关系型和非关系型
非关系型数据库的优势：
1. **性能** NOSQL是基于键值对的，可以想象成表中的主键和值的对应关系，而且不需要经过SQL层的解析，所以性能非常高。
2. **可扩展性** 同样也是因为基于键值对，数据之间没有耦合性，所以非常容易水平扩展。

关系型数据库的优势：
1. **复杂查询** 可以用SQL语句方便的在一个表以及多个表之间做非常复杂的数据查询。
2. **事务支持** 使得对于安全性能很高的数据访问要求得以实现。

对于这两类数据库，对方的优势就是自己的弱势，反之亦然。

---

参考引用：

[NoSQL简介及常用的NoSQL数据库对比（Redis、MongoDB、HBase等）](https://blog.csdn.net/cangchen/article/details/44830087)
