<a id="markdown-svn" name="svn"></a>
# svn

<a id="markdown-svn-pre-commit" name="svn-pre-commit"></a>
## svn pre-commit
强制写提交消息：
```bat
@echo off  <!-- TOC -->

- [svn](#svn)
    - [svn pre-commit](#svn-pre-commit)

<!-- /TOC -->
 setlocal 

 set REPOS=%1  
 set TXN=%2          

 

rem 保证输入8个字符 
 svnlook log %REPOS% -t %TXN% | findstr "........" > nul 
 if %errorlevel% gtr 0 goto :err_action

 

rem 过滤空格字符 
svnlook log %REPOS% -t %TXN% | findstr /ic:"        " > nul 
 if %errorlevel% gtr 0 goto :success 
  
 :err_action 
 echo 你本次版本提交未填写任何变更的日志说明信息.      >&2 
 echo 请补充日志说明信息后再提交代码,例如:功能说明等.  >&2 
 echo 输入的日志信息不少于8个字符说明(或4个汉字),谢谢! >&2 
 echo *******************禁止空格数据***************** >&2


 goto :err_exit

 

:err_exit 
 exit 1 
  
 :success 
 exit 0
```

