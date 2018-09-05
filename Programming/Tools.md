<!-- TOC -->

- [工具篇](#工具篇)
    - [SVN](#svn)
        - [简介](#简介)
        - [概念](#概念)
        - [控制图标](#控制图标)
        - [svn pre-commit](#svn-pre-commit)
    - [Visual Studio Code](#visual-studio-code)
        - [列模式](#列模式)

<!-- /TOC -->

<a id="markdown-工具篇" name="工具篇"></a>
# 工具篇

<a id="markdown-svn" name="svn"></a>
## SVN

<a id="markdown-简介" name="简介"></a>
### 简介
Subversion(SVN) 是一个开源的版本控制系統, 也就是说 Subversion 管理着随时间改变的数据。 这些数据放置在一个中央资料档案库(repository) 中。 这个档案库很像一个普通的文件服务器, 不过它会记住每一次文件的变动。 这样你就可以把档案恢复到旧的版本, 或是浏览文件的变动历史。

其最主要的功能是：目录版本控制和版本历史。

<a id="markdown-概念" name="概念"></a>
### 概念
- repository（源代码库）:源代码仓库
- checkout（签出）:首次从仓库获取代码时，需要从respository checkout
- commit（提交）:本地代码有修改时，需要将本地的变更commit到repository，即将改动上传
- update (更新):在本地仓库进行更新，即从reponsitory进行下载代码

首次从仓库获取代码时进行checkout即可，不用每次使用都进行checkout。

1. 适当频率进行update。通常每天开始第一件事是进行更新(update)
2. 当本地有代码变更时，一定要编译通过再进行提交(commit)，且commit前最好进行update

**update->编译通过->commit**

<a id="markdown-控制图标" name="控制图标"></a>
### 控制图标

![](../assets/SVN/1.png)

<a id="markdown-svn-pre-commit" name="svn-pre-commit"></a>
### svn pre-commit
在仓库右键选中Properties->Hooks->pre-commit，设置提交前验证，必须填写提交日志：
```bat

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

<a id="markdown-visual-studio-code" name="visual-studio-code"></a>
## Visual Studio Code

<a id="markdown-列模式" name="列模式"></a>
### 列模式
列模式选择

为所有相同的字符串实例添加光标：将光标移动到某一字符串（字符串出现灰色背景），按Ctrl+Shift+L（字符串出现蓝色背景）【批量修改变量名】


