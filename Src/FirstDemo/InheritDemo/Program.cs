using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dd = new Dog();
            dd.Name = "二哈";
            dd.Age = 1;
            dd.Say();

            List<Animal> lstAnimal = new List<Animal>();
            lstAnimal.Add(new Dog());
            lstAnimal.Add(new Cat());

            Console.WriteLine("=================================================");
            foreach (Animal item in lstAnimal)
            {
                //虚方法重写
                item.Say();
            }

            List<Animal> lstAA = new List<Animal>();
            lstAA.Add(new Dog());
            lstAA.Add(new Cat());
            Console.WriteLine("=================================================");
            foreach (var item in lstAA)
            {
                //抽象方法重写
                item.Eat();
            }

            Console.ReadKey();
        }
    }
}
