<!-- TOC -->

- [SQLServer编程](#sqlserver编程)
    - [视图](#视图)
    - [函数](#函数)
        - [标量函数](#标量函数)
        - [内联表值函数](#内联表值函数)
        - [多语句表值函数](#多语句表值函数)
    - [存储](#存储)

<!-- /TOC -->
# SQLServer编程
## 视图
在 SQL 中，视图是基于 SQL 语句的结果集的可视化的表。

视图包含行和列，就像一个真实的表。可以理解为封装了一个查询，实际每次的调用都是通过使用 SQL 语句来重建数据。

示例：
```sql
--创建视图
CREATE VIEW VIEW_NAME AS
SELECT * FROM XXXX WHERE condition

--修改已存在视图
ALTER VIEW VIEW_NAME AS
SELECT * FROM XXXX WHERE condition;

--删除视图
DROP VIEW VIEW_NAME;
```

## 函数
与编程语言中的函数类似， SQL Server 用户定义函数是接受参数、执行操作（例如复杂计算）并将操作结果以值的形式返回的例程。 返回值可以是单个标量值或结果集。

- 允许模块化程序设计。

只需创建一次函数并将其存储在数据库中，以后便可以在程序中调用任意次。 用户定义函数可以独立于程序源代码进行修改。

- 执行速度更快。

与存储过程相似， Transact-SQL 用户定义函数通过缓存计划并在重复执行时重用它来降低 Transact-SQL 代码的编译开销。 这意味着每次使用用户定义函数时均无需重新解析和重新优化，从而缩短了执行时间。
和用于计算任务、字符串操作和业务逻辑的 Transact-SQL 函数相比，CLR 函数具有显著的性能优势。 Transact-SQL 函数更适用于数据访问密集型逻辑。

- 减少网络流量。

基于某种无法用单一标量的表达式表示的复杂约束来过滤数据的操作，可以表示为函数。 然后，此函数便可以在 WHERE 子句中调用，以减少发送至客户端的数字或行数。

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

### 内联表值函数
课余研究
### 多语句表值函数
课余研究

## 存储


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

