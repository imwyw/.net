<!-- TOC -->

- [专题代码](#专题代码)
    - [MD5加密](#md5加密)
    - [上传文件](#上传文件)
    - [下载文件](#下载文件)
    - [echart统计图表](#echart统计图表)
    - [富文本编辑](#富文本编辑)

<!-- /TOC -->

<a id="markdown-专题代码" name="专题代码"></a>
# 专题代码

<a id="markdown-md5加密" name="md5加密"></a>
## MD5加密
MD5的全称是Message-Digest Algorithm 5（信息-摘要算法），在90年代初由MIT Laboratory for Computer Science和RSA Data Security Inc的Ronald L. Rivest开发出来，经MD2、MD3和MD4发展而来。

MD5 是不可逆算法，解密只能通过暴力破解的方式。

```cs
string clearText = "hello world";

// 构造一个MD5对象
MD5 md5 = new MD5CryptoServiceProvider();
string cipherText = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(clearText))).Replace("-", "");
```


<a id="markdown-上传文件" name="上传文件"></a>
## 上传文件

<a id="markdown-下载文件" name="下载文件"></a>
## 下载文件

<a id="markdown-echart统计图表" name="echart统计图表"></a>
## echart统计图表

<a id="markdown-富文本编辑" name="富文本编辑"></a>
## 富文本编辑

