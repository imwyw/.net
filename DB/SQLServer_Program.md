<!-- TOC -->

- [SQLServer编程](#sqlserver编程)
    - [视图](#视图)
    - [函数](#函数)
        - [标量函数](#标量函数)
        - [内联表值函数](#内联表值函数)
        - [多语句表值函数](#多语句表值函数)
    - [存储过程](#存储过程)
        - [自定义存储过程](#自定义存储过程)
    - [事务](#事务)
        - [编码示例](#编码示例)

<!-- /TOC -->
<a id="markdown-sqlserver编程" name="sqlserver编程"></a>
# SQLServer编程
<a id="markdown-视图" name="视图"></a>
## 视图
在 SQL 中，视图是基于 SQL 语句的结果集的可视化的表。

视图包含行和列，就像一个真实的表。可以理解为封装了一个查询，实际每次的调用都是通过使用 SQL 语句来重建数据。

示例：
```sql
--创建视图
CREATE VIEW VIEW_NAME AS
SELECT * FROM XXXX WHERE condition;

--修改已存在视图
ALTER VIEW VIEW_NAME AS
SELECT * FROM XXXX WHERE condition;

--删除视图
DROP VIEW VIEW_NAME;
```

<a id="markdown-函数" name="函数"></a>
## 函数
与编程语言中的函数类似， SQL Server 用户定义函数是接受参数、执行操作（例如复杂计算）并将操作结果以值的形式返回的例程。 返回值可以是单个标量值或结果集。

- 允许模块化程序设计。

只需创建一次函数并将其存储在数据库中，以后便可以在程序中调用任意次。 用户定义函数可以独立于程序源代码进行修改。

- 执行速度更快。

与存储过程相似， Transact-SQL 用户定义函数通过缓存计划并在重复执行时重用它来降低 Transact-SQL 代码的编译开销。 这意味着每次使用用户定义函数时均无需重新解析和重新优化，从而缩短了执行时间。
和用于计算任务、字符串操作和业务逻辑的 Transact-SQL 函数相比，CLR 函数具有显著的性能优势。 Transact-SQL 函数更适用于数据访问密集型逻辑。

- 减少网络流量。

基于某种无法用单一标量的表达式表示的复杂约束来过滤数据的操作，可以表示为函数。 然后，此函数便可以在 WHERE 子句中调用，以减少发送至客户端的数字或行数。

<a id="markdown-标量函数" name="标量函数"></a>
### 标量函数
所谓标量函数简单点来讲就是返回的结果只是一个标量，也就是说，返回的结果就是一种类型的一个值。

```sql
-- 函数的创建或修改，标量值函数,定义时dbo不可省略，针对传入参数进行 +1 操作，并返回
CREATE/ALTER FUNCTION dbo.F_TESTGET ( @VAL INT )
RETURNS INT
AS
    BEGIN
        RETURN @VAL + 1
    END

--删除该自定义函数
DROP FUNCTION dbo.F_TESTGET;

--调用检查结果，调用时dbo也不可省略
SELECT dbo.F_TESTGET(1);--返回2
```

<a id="markdown-内联表值函数" name="内联表值函数"></a>
### 内联表值函数
课余研究
<a id="markdown-多语句表值函数" name="多语句表值函数"></a>
### 多语句表值函数
课余研究

<a id="markdown-存储过程" name="存储过程"></a>
## 存储过程
存储过程就是SQL Server为了实现特定任务，而将一些需要多次调用的固定操作语句编写成程序段，这些程序段存储在服务器上，有数据库服务器通过程序来调用。

优点：
1. 存储过程只在创建时编译，以后每次执行时不需要重新编译。
2. 存储过程可以封装复杂的数据库操作，简化操作流程，例如对多个表的更新，删除等。
3. 可实现模块化的程序设计，存储过程可以多次调用，提供统一的数据库访问接口，改进应用程序的可维护性。
4. 存储过程可以增加代码的安全性，对于用户不能直接操作存储过程中引用的对象，SQLServer可以设定用户对指定存储过程的执行权限。
5. 存储过程可以降低网络流量，存储过程代码直接存储于数据库中，在客户端与服务器的通信过程中，不会产生大量的T_SQL代码流量。

缺点：
1. 不支持面向对象的设计，无法采用面向对象的方式将逻辑业务进行封装，甚至形成通用的可支持服务的业务逻辑框架。
2. 代码可读性差，不易维护，难以进行版本管理。

<a id="markdown-自定义存储过程" name="自定义存储过程"></a>
### 自定义存储过程
基本语法结构
```sql
ALTER PROCEDURE SP_TEST
AS
    BEGIN	
        SELECT * FROM [dbo].[T_STUDENT];
    END
```

常用语法：
```sql
--定义变量
DECLARE @CNT INT
--变量赋值
SET @CNT = 1;
SELECT @CNT = 8;
SELECT @CNT = COUNT(1) FROM [TABLE_NAME];
UPDATE @CNT = COUNT(1) FROM [TABLE_NAME];
-- 打印变量
PRINT @CNT;

--循环
WHILE ( @CNT < 100 )
    BEGIN
        SET @CNT = @CNT + 1;
    END

--IF...ELSE...
IF ( @INPUT > 0 )
    BEGIN
        SET @RES = '正数';
    END
ELSE
    IF ( @INPUT < 0 )
        BEGIN
            SET @RES = '负数';
        END
    ELSE
        BEGIN
            SET @RES = '零';
        END
--游标，课余研究
```

示例：有参有输出参数，返回输入参数是正数、负数、零
```sql
CREATE PROCEDURE SP_TEST
    (
      @INPUT INT ,
      @RES VARCHAR(20) OUTPUT
    )
AS
    BEGIN
        IF ( @INPUT > 0 )
            BEGIN
                SET @RES = '正数';
            END
        ELSE
            IF ( @INPUT < 0 )
                BEGIN
                    SET @RES = '负数';
                END
            ELSE
                BEGIN
                    SET @RES = '零';
                END
    END

```

示例：无参存储，批量插入测试数据
```sql
CREATE TABLE TABLE_BATCH
    (
      ID INT IDENTITY(1, 1) ,
      NAME VARCHAR(50) ,
      AGE INT ,
      UPDATE_TIME DATETIME
    )
    
--创建存储，如已存在则将CREATE替换为ALTER
ALTER PROCEDURE P_IN_BATCH_DATA
AS
    DECLARE @NUM INT
    SET @NUM = 1

	--10w条记录
    WHILE ( @NUM <= 100000 )
        BEGIN 
            SET @NUM = @NUM + 1

	--插入测试数据，NEWID()产生随机值，RAND()生成随机小数，SYSDATETIME()系统时间
            INSERT  INTO TABLE_BATCH
                    ( NAME ,
                      AGE ,
                      UPDATE_TIME
                    )
            VALUES  ( NEWID() ,
                      CAST(RAND() * 100 AS INT) ,
                      SYSDATETIME()
                    )
        END

--执行该存储
EXEC P_IN_BATCH_DATA;
```

分页存储过程：
```sql
CREATE PROCEDURE [dbo].[sp_paged_data]
    (
      @sqlTable NVARCHAR(200) ,          ----待查询表名
      @sqlColumns NVARCHAR(500) ,    ----待显示字段,eg:*
      @sqlWhere NVARCHAR(1000) ,     ----查询条件,不需where,eg:and id=1
      @sqlSort NVARCHAR(500) ,            ----排序字段(必须有！)，不需order by,eg:id
      @pageIndex INT ,                         ----当前页，索引页从0开始
      @pageSize INT ,                            ----每页显示的记录数
      @rowTotal INT = 1 OUTPUT	         ----返回总记录数
    )
AS
    BEGIN
        --不返回计数（表示受 Transact-SQL 语句影响的行数）
        SET NOCOUNT ON;
        -- 获取记录总数的查询SQL语句
        DECLARE @sqlcount NVARCHAR(1000);

        SET @sqlcount = N' select @rowTotal=count(*) from ' + @sqlTable
            + ' where 1=1 ' + @sqlWhere;
        EXEC sp_executesql @sqlcount, N'@rowTotal int out ', @rowTotal OUT;
		--debug
        --PRINT @sqlcount;
        
		-- 返回数据的查询SQL语句
        DECLARE @sqldata NVARCHAR(1000);
        IF ( @pageIndex = 0 )
            BEGIN
                SET @sqldata = ' select top ' + CAST(@pageSize AS VARCHAR(10))
                    + ' ' + @sqlColumns + ' from ' + @sqlTable + ' where 1=1 '
                    + @sqlWhere + ' order by ' + @sqlSort;
				
            END
        ELSE
            BEGIN
                SET @sqldata = ' select ' + @sqlColumns
                    + ' from (select *,Row_number() over(order by ' + @sqlSort
                    + ' ) as RN from ' + @sqlTable + ' where 1=1 ' + @sqlWhere
                    + ') as TR where RN>'
                    + CAST(@pageSize * @pageIndex AS VARCHAR(20)) + ' and RN<'
                    + CAST(( @pageSize * ( @pageIndex + 1 ) + 1 ) AS VARCHAR(20));
            END
		--debug
        --PRINT @sqldata;
        EXEC sp_executesql @sqldata;
    END
```

存储调用：
```sql
DECLARE @total INT;
EXEC sp_paged_data '[emp]', '*', 'and 1=1', 'ID asc', 2, 10, @total OUT;
```

<a id="markdown-事务" name="事务"></a>
## 事务
在我们的常识中，事务就是要做的或所做的事情。但是在计算机术语中，事务(Transaction)是访问并可能更新数据库中各种数据项的一个程序执行单元(unit)。

事务具有4大特性：原子性、一致性、隔离性、持久性。这四个属性通常称为ACID特性。

- 原子性（atomicity）

事务是一个不可分割的工作单位，事务中包括的诸操作要么都做，要么都不做。

- 一致性（consistency）

事务必须是使数据库从一个一致性状态变到另一个一致性状态。一致性与原子性是密切相关的。

- 隔离性（isolation）

一个事务的执行不能被其他事务干扰。即一个事务内部的操作及使用的数据对并发的其他事务是隔离的，并发执行的各个事务之间不能互相干扰。

- 持久性（durability）

持久性也称永久性（permanence），指一个事务一旦提交，它对数据库中数据的改变就应该是永久性的。接下来的其他操作或故障不应该对其有任何影响。

<a id="markdown-编码示例" name="编码示例"></a>
### 编码示例
简单示例：
```sql
--开始事务
BEGIN TRANSACTION;
--定义变量，记录是否发生异常
DECLARE @ERR_SUM INT;
SET @ERR_SUM = 0;

/*业务操作1 DELETE FROM ..............*/
--记录当前业务是否发生异常
SET @ERR_SUM = @ERR_SUM + @@ERROR;

/*业务操作2 UPDATE XXX SET XXX................*/
--记录当前业务是否发生异常
SET @ERR_SUM = @ERR_SUM + @@ERROR;

--如前面业务操作发生异常则回滚操作，否则进行提交
IF ( @ERR_SUM > 0 )
    ROLLBACK TRANSACTION;
ELSE
    COMMIT TRANSACTION;
```
