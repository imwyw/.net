CREATE TABLE TABLE_1
(
    ID INT IDENTITY(1,1),
    NAME VARCHAR(20)
)

INSERT INTO TABLE_1 (NAME) VALUES('王');
INSERT INTO TABLE_1 (NAME) VALUES('张');
INSERT INTO TABLE_1 (NAME) VALUES('张');
INSERT INTO TABLE_1 (NAME) VALUES('张');

SELECT * FROM TABLE_1;

-- 统计不同NAME的数量
SELECT NAME,COUNT(1) FROM dbo.TABLE_1 GROUP BY NAME;

-- 统计重复的NAME，也就是COUNT()结果大于1
SELECT NAME,COUNT(1) FROM dbo.TABLE_1 GROUP BY NAME HAVING COUNT(1)>1;

-- DISTINCT
-- 显示不同的NAME
SELECT DISTINCT NAME FROM dbo.TABLE_1;

-- 统计不同NAME的数目
SELECT COUNT(DISTINCT NAME) FROM dbo.TABLE_1;

/**********************************************************************************************/

CREATE TABLE T_STUDENT
    (
      ID INT NOT NULL,
      C_ID INT ,
      NAME VARCHAR(20)
    );

CREATE TABLE T_COURSE ( ID INT, NAME VARCHAR(20) );

INSERT INTO dbo.T_STUDENT
        ( ID, C_ID, NAME )
VALUES  ( 1, 1, '赵一'),(2,2,'钱二'),(3,2,'孙三'),(4,3,'周四');

INSERT INTO dbo.T_COURSE
        ( ID, NAME )
VALUES  ( 1, 'BigData'),(2,'AI'),(5,'SINGING')

SELECT * FROM dbo.T_STUDENT;
SELECT * FROM dbo.T_COURSE;

-- 输出这样的返回结果  学生编号，学生姓名，课程编号，课程名称

--笛卡尔积 4*3 12行结果
SELECT A.ID AS '学生编号',A.NAME AS '学生姓名',B.ID AS '课程编号',B.NAME AS '课程名称'
 FROM dbo.T_STUDENT A CROSS JOIN dbo.T_COURSE B ; 

-- 左外连接 左连接  LEFT JOIN/LEFT OUTER JOIN
SELECT A.ID AS '学生编号',A.NAME AS '学生姓名',B.ID AS '课程编号',B.NAME AS '课程名称'
 FROM dbo.T_STUDENT A LEFT JOIN dbo.T_COURSE B ON a.C_ID = b.ID; 

-- 右连接 
SELECT A.ID AS '学生编号',A.NAME AS '学生姓名',B.ID AS '课程编号',B.NAME AS '课程名称'
 FROM dbo.T_STUDENT A RIGHT JOIN dbo.T_COURSE B ON a.C_ID = b.ID; 

-- 内连接
SELECT A.ID AS '学生编号',A.NAME AS '学生姓名',B.ID AS '课程编号',B.NAME AS '课程名称'
 FROM dbo.T_STUDENT A INNER JOIN dbo.T_COURSE B ON a.C_ID = b.ID; 

-- 全连接
SELECT A.ID AS '学生编号',A.NAME AS '学生姓名',B.ID AS '课程编号',B.NAME AS '课程名称'
 FROM dbo.T_STUDENT A FULL JOIN dbo.T_COURSE B ON A.C_ID = B.ID; 


SELECT 1 AS id ,'张' AS NAME
UNION
SELECT 89,'ABC';


SELECT NEWID();

