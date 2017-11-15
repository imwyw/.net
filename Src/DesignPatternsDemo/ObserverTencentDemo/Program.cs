using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverTencentDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Tencent nongyao = new TencentGame("王者荣耀");

            ITencentObserver jack = new ConcreteObserver("jack");
            ITencentObserver lucy = new ConcreteObserver("lucy");
            nongyao.AddObserver(jack);
            nongyao.AddObserver(lucy);

            nongyao.Publish("发布新皮肤");

            Console.WriteLine("=============================");
            nongyao.RemoveObserver(jack);
            nongyao.Publish("双11活动，全场五折");
        }
    }

    public abstract class Tencent
    {
        /// <summary>
        /// 订阅号的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 订阅号需要发布的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 保存观察者(订阅者)列表
        /// </summary>
        List<ITencentObserver> lstObs = new List<ITencentObserver>();
        public Tencent(string name) { Name = name; }
        public void AddObserver(ITencentObserver obs)
        {
            lstObs.Add(obs);
        }
        public void RemoveObserver(ITencentObserver obs)
        {
            lstObs.Remove(obs);
        }
        public void NotifyObserver()
        {
            foreach (var item in lstObs)
            {
                item.Update(this);
            }
        }
        /// <summary>
        /// 发布消息
        /// </summary>
        public abstract void Publish(string msg);
    }

    public interface ITencentObserver
    {
        void Update(Tencent tencent);
    }

    public class TencentGame : Tencent
    {
        /// <summary>
        /// string msg = "" 表示该参数可缺省，有一点必须要注意，这样的参数必须写在形参的最后
        /// </summary>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        public TencentGame(string name) : base(name) { }
        public override void Publish(string msg)
        {
            Message = msg;
            Console.WriteLine("{0}发布了消息：{1}", Name, Message);
            NotifyObserver();
        }
    }

    /// <summary>
    /// 具体的观察者类
    /// </summary>
    public class ConcreteObserver : ITencentObserver
    {
        public string Name { get; set; }
        public ConcreteObserver(string name) { Name = name; }
        public void Update(Tencent tencent)
        {
            Console.WriteLine("{0}收到消息：{1}", Name, tencent.Message);
            Console.WriteLine("{0}的消息{1}已发送至{2}的邮箱", tencent.Name, tencent.Message, Name);
        }
    }
}
