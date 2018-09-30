-- Create table
create table T_CUSTOMER
(
  customer_id   INTEGER not null,
  company_name  VARCHAR2(50),
  contact_name  CHAR(8),
  phone         VARCHAR2(20),
  address       VARCHAR2(100),
  email_address VARCHAR2(50)
)
tablespace FLY
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    next 1
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table T_CUSTOMER
  is '客户';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_CUSTOMER
  add primary key (CUSTOMER_ID)
  using index 
  tablespace FLY
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
  
  
-- Create table
create table T_EMPLOYEE
(
  employee_id   INTEGER not null,
  employee_name VARCHAR2(50),
  sex           CHAR(2),
  birth_date    DATE,
  hire_date     DATE,
  salary        NUMBER(19,4),
  department_id INTEGER
)
tablespace FLY
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    next 1
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table T_EMPLOYEE
  is '员工';
-- Add comments to the columns 
comment on column T_EMPLOYEE.hire_date
  is '入职日期';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_EMPLOYEE
  add primary key (EMPLOYEE_ID)
  using index 
  tablespace FLY
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

  
-- Create table
create table T_DEPARTMENT
(
  department_id      INTEGER not null,
  department_name    VARCHAR2(30),
  manager            CHAR(8),
  depart_description VARCHAR2(50)
)
tablespace FLY
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    next 1
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table T_DEPARTMENT
  is '部门';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_DEPARTMENT
  add primary key (DEPARTMENT_ID)
  using index 
  tablespace FLY
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
  
  
-- Create table
create table T_PRODUCT
(
  product_id           INTEGER not null,
  product_name         VARCHAR2(50) not null,
  price                NUMBER(18,2),
  product_stock_number INTEGER,
  product_sell_number  INTEGER
)
tablespace FLY
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    next 1
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table T_PRODUCT
  is '产品信息';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_PRODUCT
  add primary key (PRODUCT_ID)
  using index 
  tablespace FLY
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
  
  
-- Create table
create table T_PROVIDER
(
  provider_id      INTEGER not null,
  provider_name    VARCHAR2(50),
  contact_name     CHAR(8),
  provider_address VARCHAR2(100),
  provider_phone   VARCHAR2(15),
  provider_email   VARCHAR2(20)
)
tablespace FLY
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    next 1
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table T_PROVIDER
  is '供应商信息';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_PROVIDER
  add primary key (PROVIDER_ID)
  using index 
  tablespace FLY
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
  
  
-- Create table
create table T_PURCHASE_ORDER
(
  purchase_order_id     INTEGER not null,
  product_id            INTEGER,
  purchase_order_number INTEGER,
  employee_id           INTEGER,
  provider_id           INTEGER,
  purchase_order_date   DATE
)
tablespace FLY
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    next 1
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table T_PURCHASE_ORDER
  is '采购订单';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_PURCHASE_ORDER
  add primary key (PURCHASE_ORDER_ID)
  using index 
  tablespace FLY
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
  
  
-- Create table
create table T_SELL_ORDER
(
  sellorder_id      INTEGER not null,
  product_id        INTEGER,
  sell_order_number INTEGER,
  employee_id       INTEGER,
  customer_id       INTEGER,
  sell_order_date   DATE
)
tablespace FLY
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    next 1
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table T_SELL_ORDER
  is '销售订单';
-- Create/Recreate primary, unique and foreign key constraints 
alter table T_SELL_ORDER
  add primary key (SELLORDER_ID)
  using index 
  tablespace FLY
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

  
prompt Importing table T_CUSTOMER...
set feedback off
set define off
insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('1', '三川实业有限公司', '刘明    ', '030-88355547', '上海市大崇明路 50 号', 'guy1@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('2', '远东科技有限公司', '王丽丽  ', '030-88355547', '大连市沙河区承德西路 80 号', 'kevin0@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('3', '坦森行贸易有限公司', '王炫皓  ', '0321-88755539', '上海市黄台北路 780 号', 'roberto0@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('4', '国顶有限公司', '方小峰  ', '0571-87465557', '杭州市海淀区天府东街 30 号', 'rob0@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('5', '通恒机械有限公司', '黄国栋  ', '0921-85791234', '天津市南开区东园西甲 30 号', 'robme@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('6', '森通科技有限公司', '张孔苗  ', '030-88300584', '大连市沙河区常保阁东 80 号', 'yund@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('7', '国皓科技有限公司', '黄雅玲  ', '0671-68788601', '杭州市海淀区广发北路 10 号', 'yalin@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('8', '迈多贸易科技有限公司', '李丽珊  ', '0533-87855522', '天津市南开区临翠大街 80 号', 'lishan@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('9', '祥通科技有限公司', '姚苗波  ', '0678-85912445', '大连市沙河区花园东街 90 号', 'miaopo@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('10', '广通网络信息有限公司', '谢久久  ', '0478-85955547', '大连市沙河区平谷嘉石大街 38 号', 'jiujiu@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('11', '光明杂志社', '谢丽秋  ', '0571-87545551', '上海市黄石路 50 号', 'liqiu@163.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('12', '威航货运有限公司', '黄小欧  ', '0610-80113555', '天津市经七纬二路 13 号', 'xiaoou@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('13', '三捷实业有限公司', '徐慧    ', '0616-86155533', '重庆市英雄山路 84 号', 'xuhui@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('14', '浩天旅行社', '洪瑞强  ', '0306-67300765', '杭州市海淀区白广路 314 号', 'huidf@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('15', '同恒软件有限公司', '陈云海  ', '0306-78355576', '天津市七一路 37 号', 'yunhai@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('16', '万海房产有限公司', '台融岛  ', '0717- 87455522', '天津市劳动路 23 号', 'rongdo@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('17', '世邦房产有限公司', '高强    ', '0241- 83410391', '天津市南开区光明东路 395 号', 'gaoq@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('18', '迈策船舶有限公司', '郑莉晓  ', '0567-56306788', '天津市南开区沉香街 329 号', 'lixiao@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('19', '中通软件有限公司', '张小平  ', '0307- 45555502', '厦门市光复北路 895 号', 'xiaopin@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('20', '正人资源有限公司', '张耀    ', '0571-45576753', '杭州市临江东街 62 号', 'zhanoq@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('21', '红阳事业有限公司', '杨晓鹏  ', '0571-45275559', '杭州市海淀区外滩西路 238 号', 'zgab1@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('22', '嘉元实业有限公司', '李霞    ', '0919-87255594', '武汉市东湖大街 28 号', 'xiao34@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('23', '嘉业房产有限公司', '黄丹和  ', '0321-78820161', '瑞安市经三纬二路 8 号', 'danhe@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('24', '五洲信托投资有限公司', '张玉    ', '0876-67695346', '天津市沿江北路 942 号', 'zhany34@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('25', '友恒信托投资有限公司', '戴瑶    ', '0896-67387731', '天津市经二路 9 号', 'dail4@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('26', '国银贸易有限公司', '吴晓贵  ', '0876-76703221', '福州市嘉禾区辅城街 42 号', 'xiaogui@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('27', '文成软件有限公司', '汤蓬蓬  ', '0569-67349882', '福州市嘉禾区临江街 32 号', 'penpen@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('28', '康浦房产有限公司', '黄振    ', '0687- 67435425', '厦门市授业路 361 号', 'huangzheng@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('29', '东旗有限公司', '莫笑笑  ', '0571-26760334', '温州市尊石路 238 号', 'xiao3@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('30', '建资房产有限公司', '赵凯    ', '0922-67755582', '广州市广惠东路 38 号', 'zhaokai@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('31', '温州中学', '李德奇  ', '0577-68235423', '温州市市府路23号', 'deqi4@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('32', '均瑶航空公司', '何燕    ', '0577-82635423', '温州市学院路24号', 'heyan3@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('33', '清华大学出版社', '李明明  ', '010-92929332', '北京市朝阳路23号', 'mingmin3@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('35', '北京秦峰昕鑫商贸有限公司', '李俊峰  ', '0135-53583789', '北京市 北京复兴路83号东九楼504大厦１8c', 'rr45@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('36', '深圳市金丰达科技有限公司', '董小姐  ', '0755-83789290', '中国 广东 深圳市 福田区福华路２９号京海大厦１８Ｃ', 'hjyu7@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('37', '上海友谊卷笔刀有限公司', '马颖达  ', '021-58033688', '上海市南汇区南六公路1125弄三灶工业园区发展西路11号', '5er6@mail.zjitc.com');

insert into T_CUSTOMER (CUSTOMER_ID, COMPANY_NAME, CONTACT_NAME, PHONE, ADDRESS, EMAIL_ADDRESS)
values ('38', '林川中学', '毛梅捷  ', '13858235423', '新居市学院路24号', 'lincun@lczxmail.com.c');

prompt Done.




prompt Importing table T_EMPLOYEE...
set feedback off
set define off
insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('1', '章宏', '男', to_date('28-10-1969', 'dd-mm-yyyy'), to_date('30-04-2005', 'dd-mm-yyyy'), '3100', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('2', '李立三  ', '女', to_date('13-05-1980', 'dd-mm-yyyy'), to_date('20-01-2003', 'dd-mm-yyyy'), '3460.2', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('3', '王孔若  ', '女', to_date('17-12-1974', 'dd-mm-yyyy'), to_date('11-08-2000', 'dd-mm-yyyy'), '3800.8', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('4', '余杰    ', '男', to_date('11-07-1973', 'dd-mm-yyyy'), to_date('23-09-2002', 'dd-mm-yyyy'), '3315', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('5', '蔡慧敏  ', '男', to_date('12-08-1957', 'dd-mm-yyyy'), to_date('22-07-2001', 'dd-mm-yyyy'), '3453.7', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('6', '孔高铁  ', '男', to_date('17-11-1974', 'dd-mm-yyyy'), to_date('10-09-2000', 'dd-mm-yyyy'), '3600.5', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('7', '姚晓力  ', '女', to_date('14-08-1969', 'dd-mm-yyyy'), to_date('18-07-2001', 'dd-mm-yyyy'), '3313.8', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('8', '宋振辉  ', '男', to_date('16-05-1975', 'dd-mm-yyyy'), to_date('11-11-2000', 'dd-mm-yyyy'), '3376.6', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('9', '刘丽    ', '男', to_date('21-08-1960', 'dd-mm-yyyy'), to_date('15-01-2002', 'dd-mm-yyyy'), '3421.9', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('10', '姜玲娜  ', '女', to_date('02-08-1980', 'dd-mm-yyyy'), to_date('09-08-2006', 'dd-mm-yyyy'), '3200', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('11', '崔军利  ', '男', to_date('23-07-1966', 'dd-mm-yyyy'), to_date('19-12-2003', 'dd-mm-yyyy'), '3392', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('12', '金林皎  ', '男', to_date('22-08-1968', 'dd-mm-yyyy'), to_date('05-11-2001', 'dd-mm-yyyy'), '3366', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('13', '唐军芳  ', '男', to_date('15-06-1978', 'dd-mm-yyyy'), to_date('22-04-2004', 'dd-mm-yyyy'), '3304.1', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('14', '郑阿齐  ', '女', to_date('04-08-1960', 'dd-mm-yyyy'), to_date('07-02-2004', 'dd-mm-yyyy'), '3409.8', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('15', '刘启芬  ', '男', to_date('19-11-1970', 'dd-mm-yyyy'), to_date('19-09-2001', 'dd-mm-yyyy'), '3432.5', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('16', '吴昊    ', '男', to_date('13-09-1963', 'dd-mm-yyyy'), to_date('23-12-2002', 'dd-mm-yyyy'), '3361.3', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('17', '何文华  ', '女', to_date('13-01-1965', 'dd-mm-yyyy'), to_date('19-12-2002', 'dd-mm-yyyy'), '3306.2', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('18', '李萍    ', '女', to_date('28-04-1974', 'dd-mm-yyyy'), to_date('11-04-2003', 'dd-mm-yyyy'), '3295.7', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('19', '宋广科  ', '男', to_date('29-06-1965', 'dd-mm-yyyy'), to_date('07-08-2003', 'dd-mm-yyyy'), '3384.5', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('20', '罗耀祖  ', '女', to_date('23-03-1975', 'dd-mm-yyyy'), to_date('17-05-2003', 'dd-mm-yyyy'), '3286', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('21', '吴晓松  ', '男', to_date('28-12-1969', 'dd-mm-yyyy'), to_date('07-09-2002', 'dd-mm-yyyy'), '3282.5', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('22', '钱其娜  ', '女', to_date('15-12-1964', 'dd-mm-yyyy'), to_date('21-02-2002', 'dd-mm-yyyy'), '3378.3', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('23', '章明铁  ', '女', to_date('24-02-1958', 'dd-mm-yyyy'), to_date('19-01-2001', 'dd-mm-yyyy'), '3400', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('24', '张晓明  ', '男', to_date('18-01-1960', 'dd-mm-yyyy'), to_date('15-05-2002', 'dd-mm-yyyy'), '3376', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('25', '李丽丽  ', '男', to_date('20-09-1962', 'dd-mm-yyyy'), to_date('06-09-2004', 'dd-mm-yyyy'), '3408.8', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('26', '欧阳天民', '女', to_date('17-03-1964', 'dd-mm-yyyy'), to_date('13-08-2004', 'dd-mm-yyyy'), '3359.9', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('27', '陈晓东  ', '女', to_date('15-02-1972', 'dd-mm-yyyy'), to_date('06-01-2002', 'dd-mm-yyyy'), '3374.2', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('28', '金恰亦  ', '女', to_date('12-09-1961', 'dd-mm-yyyy'), to_date('01-03-2003', 'dd-mm-yyyy'), '3318.5', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('29', '王辉    ', '男', to_date('08-09-1980', 'dd-mm-yyyy'), to_date('05-06-2005', 'dd-mm-yyyy'), '3450', '3');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('30', '柯小於', '男', to_date('29-12-1994', 'dd-mm-yyyy'), to_date('09-04-2008', 'dd-mm-yyyy'), '3566', '3');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('31', '吴玲    ', '女', to_date('04-03-1979', 'dd-mm-yyyy'), to_date('21-07-2005', 'dd-mm-yyyy'), '3410', '3');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('32', '任洁    ', '女', to_date('02-04-1982', 'dd-mm-yyyy'), to_date('21-07-2005', 'dd-mm-yyyy'), '3340', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('33', '洪皓    ', '男', to_date('03-04-1967', 'dd-mm-yyyy'), to_date('21-07-2001', 'dd-mm-yyyy'), '3410', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('34', '金圆圆  ', '女', to_date('02-03-1980', 'dd-mm-yyyy'), to_date('21-07-2005', 'dd-mm-yyyy'), '3410', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('35', '柯敏    ', '女', to_date('01-10-1982', 'dd-mm-yyyy'), to_date('02-02-2006', 'dd-mm-yyyy'), '3410', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('36', '姚安娜', '女', to_date('26-04-1983', 'dd-mm-yyyy'), to_date('26-04-2008', 'dd-mm-yyyy'), '3456', '3');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('37', '张希希', '男', to_date('02-03-1978', 'dd-mm-yyyy'), to_date('20-07-2001', 'dd-mm-yyyy'), '3400', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('38', '张琪琪', '女', to_date('26-04-1958', 'dd-mm-yyyy'), to_date('26-04-2008', 'dd-mm-yyyy'), '4000', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('39', '李亮    ', '男', to_date('02-03-1968', 'dd-mm-yyyy'), to_date('20-07-2001', 'dd-mm-yyyy'), '3400', '3');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('40', '高思修', '女', to_date('07-06-1980', 'dd-mm-yyyy'), to_date('20-07-2001', 'dd-mm-yyyy'), '3400', '3');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('41', '凃米明', '女', to_date('02-09-1989', 'dd-mm-yyyy'), to_date('02-09-2007', 'dd-mm-yyyy'), '3200', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('42', '金一名', '男', to_date('22-03-1970', 'dd-mm-yyyy'), to_date('01-01-2008', 'dd-mm-yyyy'), '3200', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('43', '童金星', '男', to_date('02-02-1958', 'dd-mm-yyyy'), to_date('02-02-2005', 'dd-mm-yyyy'), '3300.2', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('44', '李利明', '男', to_date('03-03-1964', 'dd-mm-yyyy'), to_date('02-02-2004', 'dd-mm-yyyy'), '5300', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('45', '贾振旺', '女', to_date('05-04-1975', 'dd-mm-yyyy'), to_date('07-09-2007', 'dd-mm-yyyy'), '5000', '2');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('46', '王百静', '男', to_date('26-04-1998', 'dd-mm-yyyy'), to_date('26-04-2008', 'dd-mm-yyyy'), '5000', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('47', '吴剑波', '男', to_date('30-04-1995', 'dd-mm-yyyy'), to_date('30-04-2008', 'dd-mm-yyyy'), '6443', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('48', '田大海', '男', to_date('26-04-1988', 'dd-mm-yyyy'), to_date('26-04-2008', 'dd-mm-yyyy'), '4800', '1');

insert into T_EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, SEX, BIRTH_DATE, HIRE_DATE, SALARY, DEPARTMENT_ID)
values ('49', '李央', '女', to_date('02-05-1988', 'dd-mm-yyyy'), to_date('04-05-2009', 'dd-mm-yyyy'), '3000', '1');

prompt Done.



prompt Importing table T_DEPARTMENT...
set feedback off
set define off
insert into T_DEPARTMENT (DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER, DEPART_DESCRIPTION)
values ('1', '销售部', '王丽丽  ', '主管销售');

insert into T_DEPARTMENT (DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER, DEPART_DESCRIPTION)
values ('2', '采购部', '李嘉明  ', '主管公司的产品采购');

insert into T_DEPARTMENT (DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER, DEPART_DESCRIPTION)
values ('3', '人事部', '蒋柯南  ', '主管公司的人事关系');

insert into T_DEPARTMENT (DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER, DEPART_DESCRIPTION)
values ('4', '后勤部', '张绵荷  ', '主管公司的后勤工作');

prompt Done.



prompt Importing table T_PRODUCT...
set feedback off
set define off
insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('1', '路由器', '4.5', '100', '40');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('2', '果冻', '1', '2000', '1000');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('3', '打印纸', '20', '100', '1000');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('4', '墨盒', '80', '3400', '3000');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('5', '鼠标', '40', '4566', '4500');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('6', '键盘', '80', '500', '500');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('7', '优盘', '40', '9000', '7000');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('8', '牙刷', '6.05', '9000', '8900');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('9', 'Usb鼠标', '50', '870', '80');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('10', '圆珠笔', '.61', '8000', '450');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('11', '水笔', '.3', '5400', '4000');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('12', '水彩笔', '16.5', '100', '10');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('13', '蜡笔', '7', '20', '10');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('14', '橡皮', '2', '30', '10');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('15', '苹果汁', '4.24', '70', '30');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('16', '橘子汁', '4.54', '100', '20');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('17', '鼠标垫', '2', '100', '10');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('18', '订书机', '5', '600', '30');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('19', '铅笔', '2', '500', '500');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('20', '彩色显示器', '700', '-400', '1000');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('21', '液晶显示器', '800', '600', '300');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('22', '直尺', '.89', '900', '0');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('23', '无线鼠标', '50', '800', '30');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('24', '2G优盘', '40', '760', '300');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('25', '毛巾', '10', '3400', '2000');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('26', '实践报告本', '5', '10000', '2000');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('27', '圆规', '.2', '900', '300');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('28', '键盘保护膜', '5', '700', '700');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('29', '白板', '24', '560', '300');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('30', '鼠标垫', '34', '870', '300');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('31', '蛋奶', '3', '900', '100');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('32', '牛奶', '5', '900', '10');

insert into T_PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRICE, PRODUCT_STOCK_NUMBER, PRODUCT_SELL_NUMBER)
values ('33', '玻璃茶杯', '5', '300', '50');

prompt Done.



prompt Importing table T_PROVIDER...
set feedback off
set define off
insert into T_PROVIDER (PROVIDER_ID, PROVIDER_NAME, CONTACT_NAME, PROVIDER_ADDRESS, PROVIDER_PHONE, PROVIDER_EMAIL)
values ('1', '上海友谊卷笔刀有限公司', '陈云海  ', '上海市南汇区南六公路1125弄三灶工业园区发展西路11号', '0577-88335572', '5er6@mail.zjitc.com');

insert into T_PROVIDER (PROVIDER_ID, PROVIDER_NAME, CONTACT_NAME, PROVIDER_ADDRESS, PROVIDER_PHONE, PROVIDER_EMAIL)
values ('2', '深圳市金丰达科技有限公司', '张小平  ', '中国 广东 深圳市 福田区福华路２９号京海大厦１８Ｃ', '0577-88335573', 'hjyu7@mail.zjitc.com');

insert into T_PROVIDER (PROVIDER_ID, PROVIDER_NAME, CONTACT_NAME, PROVIDER_ADDRESS, PROVIDER_PHONE, PROVIDER_EMAIL)
values ('3', '文成软件有限公司', '汤蓬蓬  ', '福州市嘉禾区临江街 32 号', '0569-67349882', 'liqiu@163.com');

insert into T_PROVIDER (PROVIDER_ID, PROVIDER_NAME, CONTACT_NAME, PROVIDER_ADDRESS, PROVIDER_PHONE, PROVIDER_EMAIL)
values ('4', '温州神话软件有限公司', '吴慧    ', '温州东游大夏1203', '0577-89574833', 'shenghua@163.com');

prompt Done.



prompt Importing table T_PURCHASE_ORDER...
set feedback off
set define off
insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('1', '9', '210', '18', '1', to_date('16-03-2004', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('2', '8', '210', '26', '2', to_date('16-07-2004', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('3', '9', '110', '32', '2', to_date('16-08-2004', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('4', '2', '210', '7', '2', to_date('15-09-2005', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('5', '3', '110', '26', '2', to_date('11-09-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('6', '4', '610', '18', '2', to_date('15-11-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('7', '5', '210', '32', '1', to_date('22-08-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('8', '2', '210', '7', '1', to_date('16-05-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('9', '4', '110', '18', '1', to_date('22-08-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('10', '5', '30', '34', '2', to_date('03-12-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('11', '6', '60', '46', '3', to_date('04-12-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('12', '6', '60', '8', '3', to_date('04-12-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('13', '8', '22', '46', '2', to_date('03-12-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('14', '5', '21', '18', '1', to_date('03-12-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('15', '10', '21', '9', '1', to_date('04-12-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('16', '7', '66', '26', '1', to_date('15-02-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('17', '6', '77', '32', '1', to_date('17-05-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('18', '7', '99', '7', '3', to_date('18-08-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('19', '5', '410', '34', '3', to_date('14-09-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('20', '6', '344', '34', '2', to_date('14-03-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('21', '6', '40', '7', '2', to_date('14-03-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('22', '9', '50', '47', '3', to_date('15-04-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('23', '7', '60', '32', '2', to_date('15-04-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('24', '8', '50', '7', '1', to_date('15-03-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('25', '10', '79', '47', '1', to_date('18-07-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('26', '10', '79', '24', '2', to_date('12-02-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('27', '10', '70', '8', '3', to_date('13-02-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('28', '13', '40', '47', '1', to_date('16-12-2007', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('29', '11', '70', '34', '3', to_date('18-07-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('30', '11', '50', '9', '3', to_date('13-11-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('31', '11', '100', '8', '2', to_date('15-11-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('32', '7', '50', '32', '2', to_date('11-01-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('33', '5', '510', '18', '2', to_date('12-12-2006', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('34', '9', '60', '32', '1', to_date('16-12-2007', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('35', '2', '110', '24', '1', to_date('10-05-2008', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('36', '6', '600', '18', '1', to_date('02-05-2008', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('37', '8', '900', '7', '2', to_date('10-05-2008', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('38', '5', '98', '8', '3', to_date('10-05-2008', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('39', '4', '108', '24', '3', to_date('10-05-2008', 'dd-mm-yyyy'));

insert into T_PURCHASE_ORDER (PURCHASE_ORDER_ID, PRODUCT_ID, PURCHASE_ORDER_NUMBER, EMPLOYEE_ID, PROVIDER_ID, PURCHASE_ORDER_DATE)
values ('40', '19', '33', '24', '3', to_date('02-05-2008', 'dd-mm-yyyy'));

prompt Done.


prompt Importing table T_SELL_ORDER...
set feedback off
set define off
insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('1', '8', '200', '3', '1', to_date('06-03-2004', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('2', '7', '200', '3', '2', to_date('06-07-2004', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('3', '8', '100', '3', '2', to_date('06-08-2004', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('4', '1', '200', '5', '5', to_date('05-09-2005', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('5', '2', '100', '15', '8', to_date('01-09-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('6', '3', '600', '14', '20', to_date('05-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('7', '4', '200', '20', '4', to_date('12-08-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('8', '1', '200', '23', '4', to_date('06-05-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('9', '3', '100', '21', '4', to_date('12-08-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('10', '4', '20', '12', '5', to_date('23-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('11', '5', '50', '25', '6', to_date('24-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('12', '5', '50', '25', '6', to_date('24-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('13', '7', '12', '27', '13', to_date('23-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('14', '4', '11', '28', '16', to_date('23-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('15', '9', '11', '3', '20', to_date('24-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('16', '6', '56', '4', '1', to_date('05-02-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('17', '5', '67', '1', '1', to_date('07-05-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('18', '6', '89', '2', '3', to_date('08-08-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('19', '4', '400', '6', '7', to_date('04-09-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('20', '5', '334', '7', '3', to_date('04-03-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('21', '5', '30', '2', '12', to_date('04-03-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('22', '8', '40', '5', '13', to_date('05-04-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('23', '6', '50', '6', '24', to_date('05-04-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('24', '7', '40', '17', '15', to_date('05-03-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('25', '9', '69', '19', '17', to_date('08-07-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('26', '9', '69', '20', '20', to_date('02-02-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('27', '9', '60', '20', '21', to_date('03-02-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('28', '12', '30', '5', '4', to_date('06-12-2007', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('29', '10', '60', '21', '24', to_date('08-07-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('30', '10', '40', '22', '26', to_date('03-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('31', '10', '90', '2', '20', to_date('05-11-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('32', '6', '40', '5', '21', to_date('01-01-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('33', '4', '500', '6', '7', to_date('02-12-2006', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('34', '8', '50', '3', '31', to_date('06-12-2007', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('35', '1', '100', '1', '1', to_date('30-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('36', '5', '590', '6', '5', to_date('22-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('37', '7', '890', '7', '4', to_date('30-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('38', '4', '88', '7', '5', to_date('30-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('39', '3', '98', '3', '2', to_date('30-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('40', '18', '23', '43', '29', to_date('22-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('41', '6', '90', '48', '3', to_date('30-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('42', '4', '87', '3', '3', to_date('30-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('43', '5', '90', '7', '15', to_date('28-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('44', '15', '8000', '16', '5', to_date('30-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('45', '3', '300', '11', '24', to_date('01-05-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('46', '10', '3000', '5', '1', to_date('01-05-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('47', '4', '300', '7', '6', to_date('02-05-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('48', '32', '100', '6', '11', to_date('13-05-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('49', '6', '233', '6', '24', to_date('03-04-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('50', '29', '2000', '6', '7', to_date('03-05-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('51', '3', '344', '6', '7', to_date('08-05-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('52', '32', '560', '35', '33', to_date('08-05-2008', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('53', '20', '400', '10', '38', to_date('05-09-2009', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('54', '3', '200', '4', '5', to_date('10-09-2009', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('55', '3', '200', '4', '5', to_date('10-09-2009', 'dd-mm-yyyy'));

insert into T_SELL_ORDER (SELLORDER_ID, PRODUCT_ID, SELL_ORDER_NUMBER, EMPLOYEE_ID, CUSTOMER_ID, SELL_ORDER_DATE)
values ('56', '3', '200', '4', '5', to_date('10-09-2009', 'dd-mm-yyyy'));

prompt Done.
