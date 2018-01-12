using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceDemo
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IBookService”。
    [ServiceContract]
    public interface IBookService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        //对应url：localhost:xxxx/BookService.svc/QueryBook/xxx
        //[WebGet(UriTemplate = "/QueryBook/{bookID}", ResponseFormat = WebMessageFormat.Json)]
        //默认url：localhost:xxxx/BookService.svc/QueryBook?bookID=xxx
        //[WebGet(ResponseFormat = WebMessageFormat.Json)]//即 UriTemplate="QueryBook?bookID={bookID}"
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Book QueryBook(string bookID);

        [OperationContract]
        //[WebInvoke(Method = "GET", UriTemplate = "/QueryBookInvoke?bookID={bookID}"
        //    , RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Book QueryBookInvoke(string bookID);
    }


    [DataContract]
    public class Book
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Author { get; set; }
    }
}
