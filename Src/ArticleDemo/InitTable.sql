/*用户表*/
CREATE TABLE t_users
    (
      id INT IDENTITY(0, 1) PRIMARY KEY,
      zh_name VARCHAR(20) ,--昵称
      name VARCHAR(20) ,--登录名
      pwd VARCHAR(20)--密码
    )

/*文章类别*/
CREATE TABLE t_category
    (
      id INT IDENTITY(0, 1) PRIMARY KEY,
      name VARCHAR(20)
    )

/*文章内容*/
CREATE TABLE t_articles
    (
      id INT IDENTITY(0, 1) PRIMARY KEY,
	  cate_id INT FOREIGN KEY REFERENCES dbo.t_category(id),
      title VARCHAR(20) ,
      content VARCHAR(2000) ,
      update_time DATETIME,
	  create_user INT FOREIGN KEY REFERENCES dbo.t_users(id)
    )

/*查询文章视图*/
ALTER VIEW v_get_articles
AS
    SELECT  a.id ,
            a.cate_id ,
            ct.name cate_name ,
            a.title ,
            a.content ,
            a.update_time ,
            a.create_user ,
            u.zh_name AS user_name
    FROM    t_articles a
            LEFT JOIN t_category ct ON a.cate_id = ct.id
            LEFT JOIN t_users u ON a.create_user = u.id

			
/*用户表 t_users 新增权限字段*/
alter table t_users add roles varchar(100);

--修改用户表密码字段长度
ALTER TABLE dbo.t_users ALTER COLUMN pwd VARCHAR(100);

--修改文章表title字段长度
ALTER TABLE dbo.t_articles ALTER COLUMN title VARCHAR(200);

/*  新增权限管理相关表 2018-1-1 START */
--菜单表
CREATE TABLE T_MENUS
    (
      ID VARCHAR(50),
	  PARENT_ID VARCHAR(50),--父级菜单的ID
	  ORDER_IDNEX INT,-- 排序的先后 10>20>30
      NAME VARCHAR(100) ,-- 菜单名称
      [URL] VARCHAR(100) ,--请求地址 eg:/Home/Index
      VERSION VARCHAR(100) ,--菜单版本
      DISABLED BIT DEFAULT (0) --是否禁用 1-禁用
    );

--角色表
CREATE TABLE T_ROLES
    (
      ID INT PRIMARY KEY IDENTITY(1000, 1) ,
      NAME VARCHAR(100),
	  REMARK VARCHAR(100)
    );

--角色用户关系表
CREATE TABLE T_ROLES_USERS
    (
      ROLE_ID INT ,
      [USER_ID] INT
    );

--角色配置，即每个角色具有访问菜单的权限
CREATE TABLE T_ROLES_MENUS
    (
      ROLE_ID INT ,
      MENU_ID VARCHAR(50)
    );

/*  新增权限管理相关表 2018-1-1 END */


-- 测试脚本，为角色id为1000和10001的角色添加测试可访问菜单
INSERT INTO DBO.T_ROLES_MENUS
        ( ROLE_ID, MENU_ID )
SELECT 1000,T.ID FROM DBO.T_MENUS T WHERE T.VERSION='V1' AND T.DISABLED=0 AND T.ID LIKE 'M01%';

INSERT INTO DBO.T_ROLES_MENUS
        ( ROLE_ID, MENU_ID )
SELECT 1001,T.ID FROM DBO.T_MENUS T WHERE T.VERSION='V1' AND T.DISABLED=0;