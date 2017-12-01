create database tickets;

USE TICKETS;

CREATE TABLE t_tickets
    (
      ID INT IDENTITY(1, 1) ,
      TicketType VARCHAR(20) ,--票类型
      Remainder INT , --余票
      Beginning VARCHAR(20) ,--起点
      Destination VARCHAR(20)--终点
	);


