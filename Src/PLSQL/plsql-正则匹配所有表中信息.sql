create table WK_GJC
(
  TABLE_NAME  VARCHAR2(30) not null,
  COLUMN_NAME VARCHAR2(30) not null,
  NR          VARCHAR2(4000)
);

DECLARE
  V_SQL VARCHAR(4000);
  V_T   VARCHAR2(10);
  CURSOR C_BM IS
    SELECT TABLE_NAME
      FROM USER_TABLES
     ORDER BY TABLE_NAME
    --WHERE ROWNUM < 10
    ;

  A_B C_BM%ROWTYPE;
BEGIN

  EXECUTE IMMEDIATE 'TRUNCATE TABLE WK_GJC';
  OPEN C_BM;
  LOOP
    FETCH C_BM
      INTO A_B;
    EXIT WHEN C_BM%NOTFOUND;
    --DBMS_OUTPUT.PUT_LINE(A_B.TABLE_NAME);
    DECLARE
      CURSOR C_LM IS
        SELECT COLUMN_NAME
          FROM ALL_TAB_COLUMNS
         WHERE TABLE_NAME = A_B.TABLE_NAME
           AND DATA_TYPE LIKE '%CHAR%';
      A_C C_LM%ROWTYPE;
    BEGIN
      OPEN C_LM;
      LOOP
        FETCH C_LM
          INTO A_C;
        EXIT WHEN C_LM%NOTFOUND;
        --DBMS_OUTPUT.PUT_LINE(A_B.TABLE_NAME || ' ' || A_C.COLUMN_NAME);
        V_SQL := 'INSERT INTO WK_GJC SELECT ' || '''' || A_B.TABLE_NAME || '''' || ',' || '''' ||
                 A_C.COLUMN_NAME || '''' || ',' || A_C.COLUMN_NAME ||
                 ' FROM ' || A_B.TABLE_NAME || ' WHERE REGEXP_LIKE(' ||
                 A_C.COLUMN_NAME || ',' || '''' ||
                 '((百人)|(千人)|(万人)|(特支)|(特殊支持))' || '''' || ')' /*|| CHR(13) ||
                                                                                         'COMMIT;'*/
         ;
      
        EXECUTE IMMEDIATE V_SQL;
        COMMIT;
        --DBMS_OUTPUT.PUT_LINE(V_SQL);
      END LOOP;
    END;
  
  END LOOP;
  CLOSE C_BM;
END;
