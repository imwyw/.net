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
        - [其他语句](#其他语句)
            - [提高效率Prompt](#提高效率prompt)
    - [索引](#索引)

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

```

### 其他语句
- 事务处理语言(TPL)

确保被DML语句影响的表的所有行及时得以更新。TPL语句包括BEGIN TRANSACTION，COMMIT和ROLLBACK。

- 数据控制语言(DCL)

通过GRANT或REVOKE获得许可，确定单个用户和用户组对数据库对象的访问。某些RDBMS可用GRANT或REVOKE控制对表单个列的访问。

- 指针控制语言(CCL)

像DECLARE CURSOR，FETCH INTO和UPDATE WHERE CURRENT用于对一个或多个表单独行的操作。

#### 提高效率Prompt

## 索引
