
/*执行以下脚本，完成练习题*/

CREATE TABLE student
    (
      sno VARCHAR(10) PRIMARY KEY ,
      sname VARCHAR(20) ,
      sage INT ,
      ssex VARCHAR(5)
    );
CREATE TABLE teacher
    (
      tno VARCHAR(10) PRIMARY KEY ,
      tname VARCHAR(20)
    );
CREATE TABLE course
    (
      cno VARCHAR(10) ,
      cname VARCHAR(20) ,
      tno VARCHAR(20) ,
      CONSTRAINT pk_course PRIMARY KEY ( cno, tno )
    );
CREATE TABLE sc
    (
      sno VARCHAR(10) ,
      cno VARCHAR(10) ,
      score NUMERIC(4, 2) ,
      CONSTRAINT pk_sc PRIMARY KEY ( sno, cno )
    );
/*******初始化学生表的数据******/
INSERT  INTO student
VALUES  ( 's001', '张三', 23, '男' );
INSERT  INTO student
VALUES  ( 's002', '李四', 23, '男' );
INSERT  INTO student
VALUES  ( 's003', '吴鹏', 25, '男' );
INSERT  INTO student
VALUES  ( 's004', '琴沁', 20, '女' );
INSERT  INTO student
VALUES  ( 's005', '王丽', 20, '女' );
INSERT  INTO student
VALUES  ( 's006', '李波', 21, '男' );
INSERT  INTO student
VALUES  ( 's007', '刘玉', 21, '男' );
INSERT  INTO student
VALUES  ( 's008', '萧蓉', 21, '女' );
INSERT  INTO student
VALUES  ( 's009', '陈萧晓', 23, '女' );
INSERT  INTO student
VALUES  ( 's010', '陈美', 22, '女' );
COMMIT;
/******************初始化教师表***********************/
INSERT  INTO teacher
VALUES  ( 't001', '刘阳' );
INSERT  INTO teacher
VALUES  ( 't002', '谌燕' );
INSERT  INTO teacher
VALUES  ( 't003', '胡明星' );
COMMIT;
/***************初始化课程表****************************/
INSERT  INTO course
VALUES  ( 'c001', 'J2SE', 't002' );
INSERT  INTO course
VALUES  ( 'c002', 'Java Web', 't002' );
INSERT  INTO course
VALUES  ( 'c003', 'SSH', 't001' );
INSERT  INTO course
VALUES  ( 'c004', 'Oracle', 't001' );
INSERT  INTO course
VALUES  ( 'c005', 'SQL SERVER 2005', 't003' );
INSERT  INTO course
VALUES  ( 'c006', 'C#', 't003' );
INSERT  INTO course
VALUES  ( 'c007', 'JavaScript', 't002' );
INSERT  INTO course
VALUES  ( 'c008', 'DIV+CSS', 't001' );
INSERT  INTO course
VALUES  ( 'c009', 'PHP', 't003' );
INSERT  INTO course
VALUES  ( 'c010', 'EJB3.0', 't002' );
COMMIT;
/***************初始化成绩表***********************/
INSERT  INTO sc
VALUES  ( 's001', 'c001', 78.9 );
INSERT  INTO sc
VALUES  ( 's002', 'c001', 80.9 );
INSERT  INTO sc
VALUES  ( 's003', 'c001', 81.9 );
INSERT  INTO sc
VALUES  ( 's004', 'c001', 60.9 );
INSERT  INTO sc
VALUES  ( 's001', 'c002', 82.9 );
INSERT  INTO sc
VALUES  ( 's002', 'c002', 72.9 );
INSERT  INTO sc
VALUES  ( 's003', 'c002', 81.9 );
INSERT  INTO sc
VALUES  ( 's001', 'c003', '59' );