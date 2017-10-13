using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlWriterDemo();
        }

        /// <summary>
        /// 通过XmlWriter创建xml文件，并写入内容
        /// </summary>
        static void XmlWriterDemo()
        {
            string xmlPath = @"..\..\XML\stu.xml";
            XmlWriterSettings xmlSet = new XmlWriterSettings();
            //开启缩进，默认是false关闭的
            xmlSet.Indent = true;
            //设置新行字符为环境换行字符
            xmlSet.NewLineChars = System.Environment.NewLine;

            using (XmlWriter xr = XmlWriter.Create(xmlPath, xmlSet))
            {
                //新建根元素 students
                xr.WriteStartElement("students");

                //新增子节点元素
                xr.WriteStartElement("stu");
                //添加属性
                xr.WriteAttributeString("age","12");
                xr.WriteAttributeString("course", "BigData");
                //添加innerText内容
                xr.WriteString("王富贵");
                xr.WriteEndElement();

                xr.WriteStartElement("stu");
                xr.WriteAttributeString("age","18");
                xr.WriteAttributeString("course","AI");
                xr.WriteString("赵有才");
                xr.WriteEndElement();
                
                //将缓冲区中的所有内容刷新到基础流，并同时刷新基础流。
                xr.Flush();
            }
        }
    }
}
