# SQLServer基础
## 创建测试库
```sql
CREATE DATABASE [TEST_DB]
```

## 常见数据类型
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

## 常见数据约束

唯一值约束（unique）

非空值约束（not null）

默认值约束（default <值>）

检查值约束（check  [字段]>值 and [字段]<值)

主码约束（primary key）

外码约束（references [表]( [字段] )）

## SQL语句结构
- 数据查询语言（DQL:Data Query Language）

用以从表中获得数据，确定数据怎样在应用程序给出。保留字SELECT是DQL（也是所有SQL）用得最多的动词，其他DQL常用的保留字有WHERE，ORDER BY，GROUP BY和HAVING。

- 数据操作语言（DML：Data Manipulation Language）

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
```

- 事务处理语言（TPL）

确保被DML语句影响的表的所有行及时得以更新。TPL语句包括BEGIN TRANSACTION，COMMIT和ROLLBACK。

- 数据控制语言（DCL）

通过GRANT或REVOKE获得许可，确定单个用户和用户组对数据库对象的访问。某些RDBMS可用GRANT或REVOKE控制对表单个列的访问。

- 数据定义语言（DDL）

包括动词CREATE和DROP。在数据库中创建新表或删除表（CREAT TABLE 或 DROP TABLE）；为表加入索引等。DDL包括许多与人数据库目录中获得数据有关的保留字。它也是动作查询的一部分。

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
	FOREIGN KEY([DEPT_ID]) REFERENCES [DEPT]([ID])--外键约束
)

DROP TABLE [EMP];
```

- 指针控制语言（CCL）

像DECLARE CURSOR，FETCH INTO和UPDATE WHERE CURRENT用于对一个或多个表单独行的操作。

## 其他
### 提高效率Prompt


