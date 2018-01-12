using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceDemo
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“BookService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 BookService.svc 或 BookService.svc.cs，然后开始调试。
    public class BookService : IBookService
    {
        public void DoWork()
        {
        }

        public Book QueryBook(string bookID)
        {
            return Query(bookID);
        }

        public Book QueryBookInvoke(string bookID)
        {
            return Query(bookID);
        }

        private Book Query(string bookID)
        {
            if (bookID == "1")
            {
                return new Book() { Name = "C#编程基础", Author = "张三" };
            }
            if (bookID == "2")
            {
                return new Book() { Name = "JAVA编程基础", Author = "李四" };
            }
            return new Book() { Name = "未知", Author = "未知" };
        }
    }
}
