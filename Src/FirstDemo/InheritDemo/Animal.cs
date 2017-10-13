using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritDemo
{
    abstract class Animal
    {
        public string Name { get; set; }

        /// <summary>
        /// 虚方法，派生类可以不进行重写
        /// </summary>
        public virtual void Say()
        {
            Console.WriteLine("你好" + Name);
        }

        /// <summary>
        /// 抽象方法
        /// a）类中有抽象方法的时候，则该类也必须为抽象类，且抽象类无法被实例化
        /// b）抽象方法不需要有方法实现
        /// c）派生类必须实现父类中的抽象方法
        /// </summary>
        public abstract void Eat();

        //抽象类与接口的区别

    }
}
