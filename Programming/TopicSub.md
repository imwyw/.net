# 专题代码

## MD5加密
MD5的全称是Message-Digest Algorithm 5（信息-摘要算法），在90年代初由MIT Laboratory for Computer Science和RSA Data Security Inc的Ronald L. Rivest开发出来，经MD2、MD3和MD4发展而来。

MD5 是不可逆算法，解密只能通过暴力破解的方式。

```cs
string clearText = "hello world";

// 构造一个MD5对象
MD5 md5 = new MD5CryptoServiceProvider();
string cipherText = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(clearText))).Replace("-", "");
```


