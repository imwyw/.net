<!-- TOC -->

- [SQLServer编程](#sqlserver编程)
    - [视图](#视图)
    - [函数](#函数)
        - [标量函数](#标量函数)
        - [内联表值函数](#内联表值函数)
        - [多语句表值函数](#多语句表值函数)
    - [存储过程](#存储过程)
        - [自定义存储过程](#自定义存储过程)
        - [分页查询](#分页查询)
        - [常用数据库分页方法](#常用数据库分页方法)
            - [定位法(利用ID大于多少)](#定位法利用id大于多少)
            - [利用TOP NOT IN](#利用top-not-in)
            - [ROW_NUMBER()函数](#row_number函数)
            - [通用分页存储过程](#通用分页存储过程)
            - [扩展后千万级分页](#扩展后千万级分页)
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
与编程语言中的函数类似， SQL Server 用户定义函数是接受参数、执行操作(例如复杂计算)并将操作结果以值的形式返回的例程。 返回值可以是单个标量值或结果集。

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

示例：有参有输出存储，返回输入参数是正数、负数、零？
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

<a id="markdown-分页查询" name="分页查询"></a>
### 分页查询
考虑数据库中保存大量数据的情况下，如万级、千万级等，一次将所有数据查询出显示在页面上，对服务器和客户端都是一个考验，显示不是一种合理的方式。

所以要把数据进行分批查询出来，每页显示一定量的数据，这就是分页查询。

<a id="markdown-常用数据库分页方法" name="常用数据库分页方法"></a>
### 常用数据库分页方法

假设有一张表如下：
```sql
CREATE TABLE TEST
    (
      ID INT PRIMARY KEY NOT NULL IDENTITY(1,1) ,
      NAMES VARCHAR(64)
    )
```

创建一个存储，添加测试数据：
```sql
-- 添加测试数据的存储
CREATE PROC PROC_ADD_TEST_DATA
AS
    DECLARE @NN INT = 0;
    BEGIN
        -- 插入100w条记录
        WHILE ( @NN < 1000000 )
            BEGIN
                SET @NN = @NN + 1;
                INSERT  INTO DBO.TEST
                        ( NAMES )
                VALUES  ( SYSDATETIME()  -- NAMES - VARCHAR(20)
                          );
            END

    END
```

查询窗口中执行该存储，插入测试数据：
```sql
--执行测试存储进行插入测试数据
BEGIN
EXEC PROC_ADD_TEST_DATA;
END

-- 测试插入是否成功
SELECT COUNT(1) FROM dbo.TEST;
```

下面我们以每页10条数据，在ID升序的情况下查询第3w页数据为例，即查询第300001~3000010条数据( `index > 30000*10 and index <= (30000+1)*10` )

```sql
-- 即实现以下SQL查询结果，此示例为极端情况(ID为有序递增)，实际应用情况下是无法使用 某字段直接进行大小比较得出结果的
SELECT * FROM dbo.TEST WHERE id>300000 AND id <= 300010 ORDER BY ID ;
```

<a id="markdown-定位法利用id大于多少" name="定位法利用id大于多少"></a>
#### 定位法(利用ID大于多少)

```sql
-- 1、找出排序后的前 30w条数据
SELECT TOP ( 30000 * 10 ) ID FROM    TEST ORDER BY ID;

-- 2、找出前30w条数据中最大的ID
SELECT MAX(T.ID) FROM (SELECT TOP ( 30000 * 10 ) ID FROM    TEST ORDER BY ID) AS T;

-- 3、使用ID大于xxx的方式实现分页查询，得出分页结果
SELECT TOP 10 * FROM TEST WHERE ID > 
(SELECT MAX(T.ID) FROM (SELECT TOP ( 30000 * 10 ) ID FROM    TEST ORDER BY ID) AS T)
ORDER BY ID
```

<a id="markdown-利用top-not-in" name="利用top-not-in"></a>
#### 利用TOP NOT IN

```sql
-- 1、找出当前页前面的数据
SELECT TOP (30000*10) * FROM TEST ORDER BY ID;

-- 2、使用NOT IN 的方式过滤
SELECT TOP 10 * FROM TEST WHERE ID NOT IN 
(SELECT TOP (30000*10) ID FROM TEST ORDER BY ID)
ORDER BY ID;
```

<a id="markdown-row_number函数" name="row_number函数"></a>
#### ROW_NUMBER()函数
需要注意：SqlServer2005以上版本开始支持ROW_NUMBER()函数。

```sql
-- 1、使用ROW_NUMBER()开窗获取行号
SELECT *,ROW_NUMBER() OVER(ORDER BY ID) RN FROM TEST;

-- 2、按照行号筛选得出当前页对应的数据
SELECT * FROM (SELECT *,ROW_NUMBER() OVER(ORDER BY ID) RN FROM TEST) AS T
WHERE T.RN > (30000*10) AND T.RN <= (30000+1)*10;
```

---
参考引用：

[Sql Server 数据分页](http://www.cnblogs.com/qqlin/archive/2012/11/01/2745161.html)

<a id="markdown-通用分页存储过程" name="通用分页存储过程"></a>
#### 通用分页存储过程

使用ROW_NUMBER()开窗函数简单实现分页查询
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
        -- 设置不返回计数(受Transact-SQL语句影响的行数)
        SET NOCOUNT ON;
        -- 定义查询记录总数的SQL语句
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
DECLARE @total INT = 0;
EXEC DBO.SP_PAGED_DATA @SQLTABLE = N'TEST', -- NVARCHAR(200) 表名
    @SQLCOLUMNS = N'*', -- NVARCHAR(500) 投影查询的字段 如：*
    @SQLWHERE = N'', -- NVARCHAR(1000) where条件 需要加and 如：AND 1=1
    @SQLSORT = N'ID', -- NVARCHAR(500) 排序 必填 ID/ID DESC,names asc
    @PAGEINDEX = 30000, -- INT 当前页码 从0开始
    @PAGESIZE = 10, -- INT 每页显示数据数目
    @ROWTOTAL = @total OUT -- INT 总记录数

SELECT  @total;
```

<a id="markdown-扩展后千万级分页" name="扩展后千万级分页"></a>
#### 扩展后千万级分页

增加容错扩展：
```sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--分页存储过程  
CREATE PROCEDURE [DBO].[SP_PAGING]
    (
      @TABLES NVARCHAR(1000) ,                --表名/视图名
      @PRIMARYKEY NVARCHAR(100) ,             --主键
      @SORT NVARCHAR(200) = NULL ,            --排序字段(不带ORDER BY)
      @PAGEINDEX INT = 1 ,                    --当前页码
      @PAGESIZE INT = 10 ,                    --每页记录数
      @FIELDS NVARCHAR(1000) = N'*' ,         --输出字段
      @FILTER NVARCHAR(1000) = NULL ,         --WHERE过滤条件(不带WHERE)
      @GROUP NVARCHAR(1000) = NULL ,          --GROUP语句(不带GROUP BY)
      @TOTALCOUNT INT OUTPUT                 --总记录数
    )
AS
    DECLARE @SORTTABLE NVARCHAR(100) 
    DECLARE @SORTNAME NVARCHAR(100) 
    DECLARE @STRSORTCOLUMN NVARCHAR(200) 
    DECLARE @OPERATOR CHAR(2) 
    DECLARE @TYPE NVARCHAR(100) 
    DECLARE @PREC INT 

    --设定排序语句
    IF @SORT IS NULL
        OR @SORT = ''
        SET @SORT = @PRIMARYKEY      
    IF CHARINDEX('DESC', @SORT) > 0
        BEGIN         
            SET @STRSORTCOLUMN = REPLACE(@SORT, 'DESC', '')         
            SET @OPERATOR = '<='     
        END 
    ELSE
        BEGIN                
            SET @STRSORTCOLUMN = REPLACE(@SORT, 'ASC', '')                
            SET @OPERATOR = '>='     
        END 
    IF CHARINDEX('.', @STRSORTCOLUMN) > 0
        BEGIN         
            SET @SORTTABLE = SUBSTRING(@STRSORTCOLUMN, 0,
                                       CHARINDEX('.', @STRSORTCOLUMN))
            SET @SORTNAME = SUBSTRING(@STRSORTCOLUMN,
                                      CHARINDEX('.', @STRSORTCOLUMN) + 1,
                                      LEN(@STRSORTCOLUMN))     
        END 
    ELSE
        BEGIN         
            SET @SORTTABLE = @TABLES         
            SET @SORTNAME = @STRSORTCOLUMN  
        END 

    --设置排序字段类型和精度 
    SELECT  @TYPE = T.NAME ,
            @PREC = C.PREC
    FROM    SYSOBJECTS O
            JOIN SYSCOLUMNS C ON O.ID = C.ID
            JOIN SYSTYPES T ON C.XUSERTYPE = T.XUSERTYPE
    WHERE   O.NAME = @SORTTABLE
            AND C.NAME = @SORTNAME
        
    IF CHARINDEX('CHAR', @TYPE) > 0
        SET @TYPE = @TYPE + '(' + CAST(@PREC AS VARCHAR) + ')'
   
    DECLARE @STRPAGESIZE NVARCHAR(50) 
    DECLARE @STRSTARTROW NVARCHAR(50) 
    DECLARE @STRFILTER NVARCHAR(1000) 
    DECLARE @STRSIMPLEFILTER NVARCHAR(1000) 
    DECLARE @STRGROUP NVARCHAR(1000)  
 
    IF @PAGEINDEX < 1
        SET @PAGEINDEX = 1  
    SET @STRPAGESIZE = CAST(@PAGESIZE AS NVARCHAR(50)) 
    
    --设置开始分页记录数 
    SET @STRSTARTROW = CAST(( ( @PAGEINDEX - 1 ) * @PAGESIZE + 1 ) AS NVARCHAR(50))  
    
    --筛选以及分组语句
    IF @FILTER IS NOT NULL
        AND @FILTER != ''
        BEGIN         
            SET @STRFILTER = ' WHERE ' + @FILTER + ' ' 
            SET @STRSIMPLEFILTER = ' AND ' + @FILTER + ' ' 
        END 
    ELSE
        BEGIN         
            SET @STRSIMPLEFILTER = ''         
            SET @STRFILTER = ''     
        END 
    IF @GROUP IS NOT NULL
        AND @GROUP != ''
        SET @STRGROUP = ' GROUP BY ' 
    
    --计算总记录数
    DECLARE @TOTALCOUNTSQL NVARCHAR(1000)
    SET @TOTALCOUNTSQL = N'SELECT @TOTALCOUNT=COUNT(*)' + N' FROM ' + @TABLES
        + @STRFILTER
    EXEC SP_EXECUTESQL @TOTALCOUNTSQL, N'@TOTALCOUNT INT OUTPUT',
        @TOTALCOUNT OUTPUT
    
    --执行查询语句    
    EXEC(
    '
    DECLARE @SORTCOLUMN ' + @TYPE + '
    SET ROWCOUNT ' + @STRSTARTROW + '
    SELECT @SORTCOLUMN=' + @STRSORTCOLUMN + ' FROM ' + @TABLES + @STRFILTER + ' ' + @STRGROUP + ' ORDER BY ' + @SORT + '
    SET ROWCOUNT ' + @STRPAGESIZE + '
    SELECT ' + @FIELDS + ' FROM ' + @TABLES + ' WHERE ' 
    + @STRSORTCOLUMN + @OPERATOR + ' @SORTCOLUMN ' + @STRSIMPLEFILTER + ' ' + @STRGROUP + ' ORDER BY ' + @SORT + '
    '
    )
```

```sql
--存储调用
DECLARE @total INT;
EXEC dbo.SP_PAGING @TABLES = N'TEST', -- nvarchar(1000) 表名
    @PRIMARYKEY = N'', -- nvarchar(100) 主键字段 可为空
    @SORT = N'ID', -- nvarchar(200) 排序字段 必填 如： ID
    @PAGEINDEX = 2, -- int  当前页码，从1开始
    @PAGESIZE = 10, -- int 每页记录数
    @FIELDS = N'*', -- nvarchar(1000) 输出字段
    @FILTER = N'', -- nvarchar(1000) where条件 如:(1=1 AND 1=1)
    @GROUP = N'', -- nvarchar(1000) 分组(不带group by) 
    @TOTALCOUNT = @total OUT -- int
SELECT  @total;
```

<a id="markdown-事务" name="事务"></a>
## 事务
在我们的常识中，事务就是要做的或所做的事情。但是在计算机术语中，事务(Transaction)是访问并可能更新数据库中各种数据项的一个程序执行单元(unit)。

事务具有4大特性：原子性、一致性、隔离性、持久性。这四个属性通常称为ACID特性。

- 原子性(atomicity)

事务是一个不可分割的工作单位，事务中包括的诸操作要么都做，要么都不做。

- 一致性(consistency)

事务必须是使数据库从一个一致性状态变到另一个一致性状态。一致性与原子性是密切相关的。

- 隔离性(isolation)

一个事务的执行不能被其他事务干扰。即一个事务内部的操作及使用的数据对并发的其他事务是隔离的，并发执行的各个事务之间不能互相干扰。

- 持久性(durability)

持久性也称永久性(permanence)，指一个事务一旦提交，它对数据库中数据的改变就应该是永久性的。接下来的其他操作或故障不应该对其有任何影响。

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
