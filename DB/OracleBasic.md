<!-- TOC -->

- [Oracle基础](#oracle基础)
    - [数据类型](#数据类型)
    - [表（主键、外键、CHECK、UNIQUE、DEFAULT、INDEX）](#表主键外键checkuniquedefaultindex)
    - [存储过程](#存储过程)
    - [变量和赋值](#变量和赋值)

<!-- /TOC -->

<a id="markdown-oracle基础" name="oracle基础"></a>
# Oracle基础
关于Oracle基础，以和SqlServer之间的对比进行展开。

<a id="markdown-数据类型" name="数据类型"></a>
## 数据类型
数字、字符、日期等常见类型的差异对比：

SQL SERVER | ORACLE
-----------|-------
DECIMAL[(P[, S])] | NUMBER[(P[, S])]
NUMERIC[(P[, S])] | NUMBER[(P[, S])]
FLOAT[(N)] | NUMBER[(N)]
INT | NUMBER
SMALLINT | NUMBER
TINYINT | NUMBER
MONEY | NUMBER[19,4]
SMALLMONEY | NUMBER[19,4]
CHAR[(N)] | CHAR[(N)]
VARCHAR[(N)] | VARCHAR2[(N)]
DATETIME | DATE
SMALLDATETIME | DATE
TEXT | CLOB
IMAGE | BLOB
BIT | NUMBER(1)

<a id="markdown-表主键外键checkuniquedefaultindex" name="表主键外键checkuniquedefaultindex"></a>
## 表（主键、外键、CHECK、UNIQUE、DEFAULT、INDEX）
Oracle定义表字段的default属性紧跟字段类型之后
```plsql
CREATE TABLE T_TABLE1(
ID NUMBER PRIMARY KEY,
-- 正确的DEFAULT定义
BIRTH_DATE DATE DEFAULT SYSDATE NOT NULL
);

CREATE TABLE T_TABLE2(
ID NUMBER PRIMARY KEY,
-- 错误的语法，DEFAULT必须跟在类型后
BIRTH_DATE DATE NOT NULL DEFAULT SYSDATE 
);
```

<a id="markdown-存储过程" name="存储过程"></a>
## 存储过程
SqlServer:
```plsql
CREATE PROCEDURE procedure_name
    /*输入、输出参数的声明部分*/
AS
    DECLARE
    /*局部变量的声明部分*/
BEGIN
    /*主体SQL语句部分*/
   /*游标声明、使用语句在此部分*/
END
```

Oracle:
```plsql
CREATE OR REPLACE PROCEDURE procedure_name
   (  /*输入、输出参数的声明部分*/  )
AS
    /*局部变量、游标等的声明部分*/
BEGIN
    /*主体SQL语句部分*/
    /*游标使用语句在此部分*/
EXCEPTION
    /*异常处理部分*/
END ；
```

<a id="markdown-变量和赋值" name="变量和赋值"></a>
## 变量和赋值
SqlServer：
```sql
-- 定义变量
DECLARE @CNT INT
DECLARE @NAME VARCHAR(20)
BEGIN
-- 变量赋值
    SET @CNT = 1;
    SELECT  @NAME = 'hello sqlserver';

    PRINT @CNT;
    PRINT @NAME;
END
```

Oracle:
```plsql
DECLARE
  V_A INTEGER := 123;
  V_B VARCHAR2(64);
BEGIN
  V_B := 'hello oracle';
  DBMS_OUTPUT.PUT_LINE(V_A);
  DBMS_OUTPUT.PUT_LINE(V_B);
END;
/
```


---

参考引用：

[Oracle基础](http://www.oraok.com/oracle/)