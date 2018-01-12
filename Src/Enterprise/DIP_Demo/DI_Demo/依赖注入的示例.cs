using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Demo
{
    public class UI
    {
        /// <summary>
        /// 模拟调用
        /// </summary>
        public void MethodFoo()
        {
            A3 obj = new A3(new B2());
            A3 obj1 = new A3(new C2());

            AProp app1 = new AProp();
            app1.Opt = new B2();
            app1.Opt = new C2();
            app1.GetData();

        }
    }


    //没有实现依赖注入的情况下：

    /// <summary>
    /// 模拟上层模块，对底层直接依赖
    /// 类A直接依赖于B
    /// </summary>
    public class A1
    {
        private B1 _opt = new B1();
    }

    /// <summary>
    /// 模拟底层模块
    /// </summary>
    public class B1 { }

    public class C1 { }


    //抽象接口，也就是第三方的介入
    public interface ISub2 { }
    public class B2 : ISub2 { }
    public class C2 : ISub2 { }
    public class A2
    {
        private ISub2 _opt = new C2();
        ISub2 _opt1 = new B2();
    }

    /// <summary>
    /// 构造注入
    /// </summary>
    public class A3
    {
        private ISub2 _opt;
        public A3(ISub2 opt)
        {
            _opt = opt;
        }
    }

    public class AProp
    {
        public ISub2 Opt { get; set; }

        internal void GetData()
        {
            Opt.xxx();
        }
    }

}
