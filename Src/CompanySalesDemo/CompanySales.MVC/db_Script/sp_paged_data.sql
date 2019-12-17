USE [CompanySales]
GO
/****** Object:  StoredProcedure [dbo].[sp_paged_data]    Script Date: 2019/11/14 10:04:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_paged_data]
    (
      @sqlTable NVARCHAR(200) ,          ----待查询表名
      @sqlColumns NVARCHAR(500) ,    ----待显示字段,eg:*
      @sqlWhere NVARCHAR(1000) ,     ----查询条件,不需where,eg:and id=1
      @sqlSort NVARCHAR(500) ,            ----排序字段(必须有！)，不需order by,eg:id
      @pageIndex INT ,                         ----当前页，索引页从0开始
      @pageSize INT ,                            ----每页显示的记录数
      @rowTotal INT = 1 OUTPUT             ----返回总记录数
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
        PRINT @sqlcount;

        -- 返回数据的查询SQL语句
        DECLARE @sqldata NVARCHAR(1000);
        SET @sqldata = ' select ' + @sqlColumns
            + ' from (select *,Row_number() over(order by ' + @sqlSort
            + ' ) as RN from ' + @sqlTable + ' where 1=1 ' + @sqlWhere
            + ') as TR where RN>'
            + CAST(@pageSize * @pageIndex AS VARCHAR(20)) + ' and RN<'
            + CAST(( @pageSize * ( @pageIndex + 1 ) + 1 ) AS VARCHAR(20));

        --debug
        PRINT @sqldata;
        EXEC sp_executesql @sqldata;
    END