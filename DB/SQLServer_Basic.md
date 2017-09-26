<!-- TOC -->

- [SQLServer基础](#sqlserver基础)
    - [语句](#语句)
        - [创建测试库](#创建测试库)
        - [DDL-数据定义语言(Data Definition Language)](#ddl-数据定义语言data-definition-language)
            - [常见数据类型](#常见数据类型)
            - [常见数据约束](#常见数据约束)
            - [示例](#示例)
        - [DML-数据操作语言(Data Manipulation Language)](#dml-数据操作语言data-manipulation-language)
        - [DQL-数据查询语言(Data Query Language)](#dql-数据查询语言data-query-language)
            - [投影查询](#投影查询)
            - [选择查询](#选择查询)
            - [聚合查询](#聚合查询)
        - [多表查询](#多表查询)
            - [连接JOIN](#连接join)
            - [合并UNION](#合并union)
        - [其他语句](#其他语句)
            - [提高效率Prompt](#提高效率prompt)
    - [索引](#索引)
        - [什么是索引？](#什么是索引)
        - [聚集索引](#聚集索引)
        - [非聚集索引](#非聚集索引)
        - [索引设计原则](#索引设计原则)
        - [创建索引](#创建索引)
    - [范式(NF)](#范式nf)

<!-- /TOC -->
# SQLServer基础
## 语句
### 创建测试库
```sql
CREATE DATABASE [TEST_DB]
```

### DDL-数据定义语言(Data Definition Language)

包括动词CREATE和DROP。在数据库中创建新表或删除表(CREAT TABLE 或 DROP TABLE)、为表加入索引等。
```sql
-- SQL CREATE TABLE + CONSTRAINT 语法
CREATE TABLE [table_name]
(
	column_name1 data_type(size) constraint_name,
	column_name2 data_type(size) constraint_name,
	column_name3 data_type(size) constraint_name,
	....
);
```

#### 常见数据类型
SQL Server数据类型 | 占用字节数 | 表示范围 | 对应的CLR类型 | 数据类型选择 | 适用场景
---------------|-------|------|----------|--------|-----
char | char(n) |   | System.String | char(2) | 使用char(2)来表示类型或状态(建议用tinyint代替)，长度固定，不管数据长度多少都占用声明大小
varchar | varchar(n) | 1~8000 | System.String | varchar(20) | 包含英文字符的字符串，注意一个汉字占用两个字节，最多8k字符，4k汉字
nvarchar | nvarchar(n) | 1~4000 | System.String | nvarchar(20) | 包含中文字符的字符串，所有字符均占用两个字节，即中英文最大都是4k长度
int | 4个字节 | -2,147,483,648 到 2,147,483,647 | System.Int32 | int | 表示整型，比如自增ID和表示数量
bigint | 8个字节 | -9,223,372,036,854,775,808 到 9,223,372,036,854,775,807 | System.Int64(Long) | bigint | 表示长整型，比如自增ID(数量比较大的情况下)
decimal | 5~17字节 |   | System.Decimal | decimal(18,2) | 金额和價格(和錢相關的)
tinyint | 1字节 | 0~255 | System.Byte | tinyint | 类型和状态，比char(2)扩展性好
bit |   | 0，1或NULL | System.Boolean | bit | 一般用来表示是和否两种情形，比如IsStop
datetime | 8字节 | 1753 年 1 月 1 日到 9999 年 12 月 31 日 | System.DateTime | datetime | 表示日期和时间
time |   |   | System.TimeSpan | time(7) | 表示时间间隔，比如计时和耗時
varbinary |   |   | System.Byte | varbinary(max) | 表示二进制数据

#### 常见数据约束
constraint_name | 说明
----------------|---
NOT NULL | 指示某列不能存储 NULL 值。
UNIQUE | 保证某列的每行必须有唯一的值。
PRIMARY KEY | NOT NULL 和 UNIQUE 的结合。确保某列（或两个列多个列的结合）有唯一标识，有助于更容易更快速地找到表中的一个特定的记录。
FOREIGN KEY | 保证一个表中的数据匹配另一个表中的值的参照完整性。 `FOREIGN KEY([本表字段]) REFERENCES [外键表]([外键表名])`
CHECK | 保证列中的值符合指定的条件。
DEFAULT | 规定没有给列赋值时的默认值。

#### 示例

```sql
CREATE TABLE EMP
(
	[ID] INT IDENTITY(1,1),--自增长列，是标识，标识种子、增量均为1
	[DEPT_ID] INT NULL,--默认运行为null，也可显式写出
	[DEPT_NUMBER] VARCHAR(20) UNIQUE,--唯一值约束 varchar(20)最多20个字符，10个汉字
	[EMP_DATE] DATETIME NULL,
	[PASSWORD] VARCHAR(50) NOT NULL,--非空值约束
	[NAME] VARCHAR(50) NOT NULL,
	[AGE] INT CHECK([AGE]>18 AND [AGE]<150),
	[GENDER] CHAR(2) NULL,
	[IS_DEL] BIT DEFAULT(0),--默认值约束
	PRIMARY KEY([ID]),--主键约束
	FOREIGN KEY([DEPT_ID]) REFERENCES [DEPT]([ID])--外键约束，创建表时就会检查外键表是否有该字段
)
--或者
CREATE TABLE EMP
(
	[ID] INT IDENTITY(1,1) PRIMARY KEY,--自增长列，是标识，标识种子、增量均为1
	[DEPT_ID] INT NULL FOREIGN KEY([DEPT_ID]) REFERENCES [DEPT]([ID]),--默认运行为null，也可显式写出
	[DEPT_NUMBER] VARCHAR(20) UNIQUE,--唯一值约束 varchar(20)最多20个字符，10个汉字
	[EMP_DATE] DATETIME NULL,
	[PASSWORD] VARCHAR(50) NOT NULL,--非空值约束
	[NAME] VARCHAR(50) NOT NULL,
	[AGE] INT CHECK([AGE]>18 AND [AGE]<150),
	[GENDER] CHAR(2) NULL,
	[IS_DEL] BIT DEFAULT(0)--默认值约束
)

--删除表，一定要注意，尽量做备份后再删除
DROP TABLE [EMP];
--可以简单备份，只备份基本表结构和数据
SELECT * INTO EMP_20170925 FROM EMP;
```

### DML-数据操作语言(Data Manipulation Language)

包括动词INSERT，UPDATE和DELETE。它们分别用于添加，修改和删除表中的行。

```sql
--插入数据
INSERT INTO [表名](字段1,字段2,字段3...) VALUES (值1,值2,值3...);
INSERT INTO [表名](字段1,字段2,字段3...) VALUES 
(值1,值2,值3...),
(值1a,值2a,值3a...);
--不指定列名也可以，即VALUES后写明所有字段，但不推荐此方式，否则表结构变动时很容易导致bug
INSERT INTO [表名] VALUES (值1,值2,值3...);

--更新数据
UPDATE [表名] SET 字段1 = 值1, 字段2 = 值2... WHERE 1 = 1 AND 其他条件...;

--删除数据
DELETE FROM [表名] WHERE 1 = 0 OR 其他条件...;
TRUNCATE TABLE [表名];
```

### DQL-数据查询语言(Data Query Language)

用以从表中获得数据，确定数据怎样在应用程序给出。保留字SELECT是DQL(也是所有SQL)用得最多的动词，其他DQL常用的保留字有WHERE，ORDER BY，GROUP BY和HAVING。

#### 投影查询
投影查询，从列的角度，即选择表中全部列或部分列

```sql
SELECT * FROM [TABLE_NAME];
SELECT FIELD1， FIELD2， FIELD3... FROM [TABLE_NAME];

--给字段起别名
SELECT FIELD1 AS NEW_NAME1, FIELD2 AS NEW_NAME2, FIELD3 AS NEW_NAME3... FROM [TABLE_NAME];
SELECT FIELD1 NEW_NAME1, FIELD2 NEW_NAME2, FIELD3 NEW_NAME3... FROM [TABLE_NAME];
```

#### 选择查询
选择查询，从行的角度，通过WHERE关键字筛选行
```sql
SELECT * FROM [TABLE_NAME] WHERE FIELD1 = 'XXX';
-- AND/OR/IN/BETWEEN...AND...的使用
SELECT * FROM [TABLE_NAME] WHERE FIELD1 = 'XXX' AND FIELD2 = 'XXXX';
SELECT * FROM [TABLE_NAME] WHERE FIELD1 = 'XXX' OR FIELD2 = 'XXXX';
SELECT * FROM [TABLE_NAME] WHERE FIELD1 IN ('X','XX','XXX');
SELECT * FROM [TABLE_NAME] WHERE FIELD1 BETWEEN 1 AND 3;
-- LIKE 模糊匹配，通配符：百分号(%)是任意数量的未知字符的替身，下划线(_)是一个未知字符的替身
SELECT * FROM [TABLE_NAME] WHERE FIELD1 LIKE '%XXX';
SELECT * FROM [TABLE_NAME] WHERE FIELD1 LIKE 'XXX%';
SELECT * FROM [TABLE_NAME] WHERE FIELD1 LIKE 'XXX_';
-- ORDER BY 排序，默认升序ASC
SELECT * FROM [TABLE_NAME] WHERE FIELD1 = 'XXX' ORDER BY FIELD2 DESC;
```
#### 聚合查询
聚合查询，对查询做聚合操作，即统计，如何求和，取平均值等

常用聚合函数：

 语法 | 说明
----|---
AVG([DISTINCT]列名) | AVG函数用于计算精确型或近似型数据类型的平均值，bit类型除外，忽略null值。AVG函数计算时将计算一组数的总和，然后除以为null的个数，得到平均值。
COUNT([DISTINCT]列名) | count函数用于计算满足条件的数据项数，返回int数据类型的值。
SUM([DISTINCT]列名) | SUM函数用于求和，只能用于精确或近似数字类型列(bit类型除外)，忽略null值
MAX(列名) | MAX函数用于计算最大值，忽略null值。max函数可以使用于numeric、char、varchar、money、smallmoney、或datetime列，但不能用于bit列。
MIN(列名) | MIN函数用于计算最小值，MIN函数可以适用于numeric、char、varchar或datetime、money或smallmoney列，但不能用于bit列。

```sql
SELECT AVG(FIELD1) FROM [TABLE_NAME];
SELECT COUNT(*) FROM [TABLE_NAME];
SELECT COUNT(1) FROM [TABLE_NAME];
SELECT COUNT(0) FROM [TABLE_NAME];
SELECT SUM(FIELD1) FROM [TABLE_NAME];
SELECT MAX(FIELD1) FROM [TABLE_NAME];
SELECT MIN(FIELD1) FROM [TABLE_NAME];
```

**特别注意**：

`COUNT(*),COUNT(1),COUNT(0)` : 数据行中是否有NULL值，返回统计均一样。仅当COUNT(列名)会判断属性值是否为NULL。

```sql
CREATE TABLE TABLE_1
(
	ID INT,
	NAME VARCHAR(20)
)

INSERT INTO TABLE_1 (ID,NAME) VALUES(NULL,'王');
INSERT INTO TABLE_1 (ID,NAME) VALUES(NULL,'张');

SELECT COUNT(*) FROM TABLE_1;--2
SELECT COUNT(1) FROM TABLE_1;--2
SELECT COUNT(NAME) FROM TABLE_1;--2
SELECT COUNT(ID) FROM TABLE_1;--0
```

**GROUP BY...HAVING...**

HAVING用于分组后的筛选
```sql
CREATE TABLE TABLE_1
(
	ID INT IDENTITY(1,1),
	NAME VARCHAR(20)
)

INSERT INTO TABLE_1 (NAME) VALUES('王');
INSERT INTO TABLE_1 (NAME) VALUES('张');
INSERT INTO TABLE_1 (NAME) VALUES('张');
INSERT INTO TABLE_1 (NAME) VALUES('张');

--按列找出重复的数据，并统计重复的数目
SELECT NAME,COUNT(NAME) CNT FROM TABLE_1 GROUP BY NAME HAVING COUNT(NAME) > 1;
```

### 多表查询
#### 连接JOIN
```sql
CREATE TABLE T_STUDENT
    (
      ID INT NOT NULL ,
      C_ID INT ,
      NAME VARCHAR(20)
    );

CREATE TABLE T_COURSE ( ID INT, NAME VARCHAR(20) );

INSERT INTO dbo.T_STUDENT
        ( ID, C_ID, NAME )
VALUES  ( 1, 1, '赵一'),(2,2,'钱二'),(2,2,'孙三'),(3,3,'周四');

INSERT INTO dbo.T_COURSE
        ( ID, NAME )
VALUES  ( 1, 'BigData'),(2,'AI'),(5,'SINGING')
```

数据如下所示：

![](..\assets\SqlServer\join_demo_1.png)

```sql
-- 笛卡尔积 cross join 

-- 左连接 left join 或 left outer join

-- 右连接 right join 或 right outer join

-- 内连接 join 或 inner join

-- 全外连接  full join 或 full outer join
```
#### 合并UNION
将多个结果集进行合并，使用关键字时候 `UNION [ALL]`，有以下需要注意的地方：

1. 两个结果集的数据列数量和数据类型必须保持一致；
2. 如果有ALL则不会移除重复的行，也不会自动排序，仅仅做合并操作；

### 其他语句
- 事务处理语言(TPL)

确保被DML语句影响的表的所有行及时得以更新。TPL语句包括BEGIN TRANSACTION，COMMIT和ROLLBACK。

- 数据控制语言(DCL)

通过GRANT或REVOKE获得许可，确定单个用户和用户组对数据库对象的访问。某些RDBMS可用GRANT或REVOKE控制对表单个列的访问。

- 指针控制语言(CCL)

像DECLARE CURSOR，FETCH INTO和UPDATE WHERE CURRENT用于对一个或多个表单独行的操作。

#### 提高效率Prompt

## 索引
### 什么是索引？
SQL索引在数据库优化中占有一个非常大的比例， 一个好的索引的设计，可以让你的效率提高几十甚至几百倍。

SQL索引有两种，聚集索引和非聚集索引，索引主要目的是提高了SQL Server系统的性能，加快数据的查询速度与减少系统的响应时间。

新华字典例子，我们查一个汉字可以通过拼音也可以通过笔画来进行查询，那么这样通过目录的查询方式就是一种索引思想，大大提高了查询效率。

按照拼音查询时，和字典的内容顺序保持一致，相当于聚集索引，同样的一个数据集合顺序是固定的，所以对于一张表而言，只能有一个聚集索引。

按照笔画查询时，笔画顺序和字典内容顺序是不一致的，相当于非聚集索引。

索引的优点主要有以下几条：
1. 通过创建唯一索引，可以保证数据库表的每一行数据的唯一性。
2. 可以大大加快数据的查询速度，这也是创建索引的最主要的原因。
3. 实现数据的参照完整性，可以速表和表之间的连接。
4. 在使用分组和排序子句进行查询时，也可以显著减少查询中分组和排序的时间。

带来的一些问题：
1. 创建索引和维护索引要耗费时间，并且随着数据量的增加所耗费的时间也会增加。
2. 索引需要占磁盘空间，除了数据表占数据空间之外，每一个索引还要占一定的物理空间，如果有大量的索引，索引文件可能比数据文件更快达到做大文件尺寸。
3. 当对表中的数据进行增加，删除和修改的时候，索引也要动态地维护，这样就就降低了数据的维护速度。

### 聚集索引
聚集索引基于数据行的键值，在表内排序和存储这些数据行。每个表只能有一个聚集索引，因为数据行本身只能按一个顺序存储。
1. 每个表只能有一个聚集索引；
2. 表中的物理顺序和索引中行的物理顺序是相同的，创建任何非聚集索引之前要首先创建聚集索引，这是因为非聚集索引改变了表中行的物理顺序；
3. 关键值的唯一性使用UNIQUE关键字或者由内部的唯一标识符明确维护。
4. 在索引的创建过程中，SQL Server临时使用当前数据库的磁盘空间，所以要保证有足够的空间创建索引。

### 非聚集索引
非聚集索引具有完全独立于数据行的结构，使用非聚集索引不用将物理数据页中的数据按列排序，非聚集索引包含索引键值和指向表数据存储位置的行定位器。

可以对表或索引视图创建多个非聚集索引。通常，设计非聚集索引是为了改善经常使用的、没有建立聚集索引的查询的性能。

查询优化器在搜索数据值时，先搜索非聚集索引以找到数据值在表中的位置，然后直接从该位置检索数据。这使得非聚集索引成为完全匹配查询的最佳选择，因为索引中包含搜索的数据值在表中的精确位置的项。

具有以下特点的查询可以考虑使用非聚集索引：
1. 使用JOIN或者GROUP BY子句，应为连接和分组操作中所涉及的列创建多个非聚集索引，为任何外键创建一个聚集索引.
2. 包含大量唯一值的字段。
3. 不返回大型结果集的查询。创建筛选索引以覆盖从大型表中返回定义完善的的行子集的查询。
4. 经常包含在查询的搜索条件(如返回完全匹配的WHERE子句)中的列。

### 索引设计原则
索引设计不合理或者缺少索引都会对数据库和应用程序的性能造成障碍，高效的索引对于获得良好的性能非常重要。需要参考以下原则：
1. 索引并非越多越好，以空间换取时间，不仅占用空间增加，而且会影响DML语句的效率。表中内容的更改需要索引做出同步修改，例如新华字典的修改。
2. 对于经常查询的字段需要添加索引，以提高效率。频繁变动的列或频繁更新的表不建议索引过多，索引需要尽可能的少。
3. 在存在大量重复值的字段，增加索引没什么太大意义。比如类似性别的字段，只有两种值，使用索引可能造成效率更低。
4. 字段里的数据量太大，最好也不要加索引。比如行数据的该字段都保存几百个字符，则添加索引没有意义。
5. 外键字段建议添加索引，以增加关联效率

### 创建索引
例如员工表EMP中需要经常按照[NAME]进行查询的话，则需要针对[NAME]添加索引
```sql
CREATE [UNIQUE] [CLUSTERED|NONCLUSTERRED] INDEX IDX_TABLE_FIELD1 ON TABLE(FIELD1);

--查看对应表有什么索引
EXEC sp_helpindex 'TABLE_NAME';
--删除对应的索引
DROP INDEX TABLE_NAME.IDX_NAME;

```

## 范式(NF)
范式是“符合某一种级别的关系模式的集合，表示一个关系内部各属性之间的联系的合理化程度”。晦涩难懂，暂且简单理解成"一张数据表的表结构所符合的某种设计标准的级别。"

- 1NF
定义为：符合1NF的关系中的每个属性都不可再分。不符合1NF的表也无法录入到数据库，如果不符合这个最基本的要求那么操作是无法成功的。

- 2NF
2NF在1NF的基础之上，消除了非主属性对于码的部分函数依赖。

比如员工管理系统中员工表的部门电话等，就属于部分依赖。

- 3NF
3NF在2NF的基础之上，消除了非主属性对于码的传递函数依赖。

比如员工管理系统中，有关部门的信息，只需要部分编号即可，其余信息均可以通过关联查询得到。


