SELECT stu.sno,stu.sname,stu.sage,stu.ssex,sc.cno,score
 FROM dbo.student stu LEFT JOIN sc ON stu.sno = sc.sno;

 -- 视图的创建
 CREATE VIEW V_GET_STU_SCORE
 AS
 SELECT stu.sno,stu.sname,stu.sage,stu.ssex,sc.cno,score
 FROM dbo.student stu LEFT JOIN sc ON stu.sno = sc.sno;

 SELECT * FROM dbo.V_GET_STU_SCORE;

 ALTER VIEW V_GET_STU_SCORE
 AS
  SELECT stu.sno,stu.sname,stu.sage,stu.ssex,sc.cno,score
 FROM dbo.student stu LEFT JOIN sc ON stu.sno = sc.sno
 WHERE stu.sname = '张三'
 ;

 -- 函数的创建
 ALTER FUNCTION dbo.F_TEST ( @INPUT INT )
RETURNS INT
 AS
        BEGIN
            SET @INPUT = @INPUT + 1;
			RETURN @INPUT;
        END

SELECT dbo.F_TEST(21) ;

--存储过程
CREATE PROCEDURE P_TEST
AS
BEGIN

CREATE TABLE T_USERS
(
	ID INT IDENTITY(1,1),
	NAME VARCHAR(20)
);

INSERT dbo.T_USERS (NAME) VALUES ('JACK');
INSERT dbo.T_USERS (NAME) VALUES ('ROSE');
END

-- 执行存储
EXEC P_TEST;

-- 常见语法

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
    BEGIN--{
        SET @CNT = @CNT + 1;
    END--}

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