using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取类型
            Student s1 = new Student("");

            Type type1 = typeof(Student);
            Type type2 = s1.GetType();
            Type type3 = Type.GetType("ReflectionDemo.Student");

            Console.WriteLine(type1 == type2);
            Console.WriteLine(type2 == type3);

            Console.WriteLine("=============================================");
            /*FieldInfo[] arrFields = type1.GetFields();
            foreach (FieldInfo item in arrFields)
            {
                Console.WriteLine("字段名称：" + item.Name);
            }

            PropertyInfo[] arrProp = type1.GetProperties();
            foreach (PropertyInfo item in arrProp)
            {
                Console.WriteLine("属性名称：" + item.Name);
            }

            MethodInfo[] arrMethods = type1.GetMethods();
            foreach (MethodInfo item in arrMethods)
            {
                Console.WriteLine("方法：" + item.Name);
            }*/

            MemberInfo[] arrMembers = type1.GetMembers();
            foreach (MemberInfo item in arrMembers)
            {
                Console.WriteLine("MemberType:{0},Name:{1}", item.MemberType, item.Name);
            }

            Console.WriteLine("=============================================");
            object obj = Activator.CreateInstance(typeof(Student), new object[] { "宋小宝" });
            MethodInfo sayMethod = typeof(Student).GetMethod("Say");
            object result = sayMethod.Invoke(obj, new object[] { "王富贵" });
            Console.WriteLine(result);
        }
    }
}
