using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorBillDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BillStructure bs = new BillStructure();
            bs.CheckAllBillDetails();
        }
    }

    /// <summary>
    /// 交易明细的抽象类
    /// </summary>
    public abstract class BillDetail
    {
        public BillDetail(string summary, int amount)
        {
            Amount = amount;
            Summary = summary;
        }
        /// <summary>
        /// 交易发生金额
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 交易摘要，如工资、股票、吃饭。。。
        /// </summary>
        public string Summary { get; set; }

        public abstract void Accept(Visitor v);
    }

    /// <summary>
    /// 支出交易类型
    /// </summary>
    public class OutBillDetail : BillDetail
    {
        public OutBillDetail(string summary, int amount) : base(summary, amount)
        {
        }

        public override void Accept(Visitor v)
        {
            v.Visit(this);
        }
    }

    /// <summary>
    /// 收入交易类型
    /// </summary>
    public class InBillDetail : BillDetail
    {
        public InBillDetail(string summary, int amount) : base(summary, amount)
        {
        }

        public override void Accept(Visitor v)
        {
            v.Visit(this);
        }
    }

    public abstract class Visitor
    {
        public abstract void Visit(OutBillDetail detail);
        public abstract void Visit(InBillDetail detail);
    }

    public class VisitorBoss : Visitor
    {
        /// <summary>
        /// 总收入
        /// </summary>
        public int TotalIn { get; set; }
        /// <summary>
        /// 总支出
        /// </summary>
        public int TotalOut { get; set; }

        /// <summary>
        /// 老板只关心总收入
        /// </summary>
        /// <param name="detail"></param>
        public override void Visit(InBillDetail detail)
        {
            TotalIn += detail.Amount;
        }

        /// <summary>
        /// 老板只关心总支出
        /// </summary>
        /// <param name="detail"></param>
        public override void Visit(OutBillDetail detail)
        {
            TotalOut += detail.Amount;
        }

        /// <summary>
        /// 显示最后统计结果
        /// </summary>
        public void ShowSum()
        {
            Console.WriteLine("共计收入：{0},共计支出：{1}，盈亏：{2}", TotalIn, TotalOut, (TotalIn - TotalOut));
        }
    }

    public class VisitorCPA : Visitor
    {
        int countIn = 1;
        int countOut = 1;
        /// <summary>
        /// CPA关心每一次交易详情
        /// </summary>
        /// <param name="detail"></param>
        public override void Visit(InBillDetail detail)
        {
            Console.WriteLine("第{0}笔收入，摘要：{1}，交易金额：{2}", countIn++, detail.Summary, detail.Amount);
        }

        /// <summary>
        /// CPA关心每一次交易详情
        /// </summary>
        /// <param name="detail"></param>
        public override void Visit(OutBillDetail detail)
        {
            Console.WriteLine("第{0}笔支出，摘要：{1}，交易金额：{2}", countOut++, detail.Summary, detail.Amount);
        }
    }

    /// <summary>
    /// 模拟账单结构
    /// </summary>
    public class BillStructure
    {
        Random rd = new Random();
        public List<BillDetail> LstBillDetails { get; set; }
        public BillStructure()
        {
            LstBillDetails = new List<BillDetail>();
            //构造随机交易流水数据
            for (int i = 0; i < 10; i++)
            {
                if (rd.Next(0, 2) % 2 == 0)
                {
                    LstBillDetails.Add(new InBillDetail("临时工", rd.Next(1, 100)));
                }
                else
                {
                    LstBillDetails.Add(new OutBillDetail("吃饭", rd.Next(1, 21)));
                }
            }
        }

        /// <summary>
        /// 核查所有交易流水
        /// </summary>
        public void CheckAllBillDetails()
        {
            Visitor boss = new VisitorBoss();
            Visitor cpa = new VisitorCPA();
            foreach (var item in LstBillDetails)
            {
                item.Accept(boss);
                item.Accept(cpa);
            }

            //由于boss是Visitor的引用，需要进行类型转换为派生类VisitorBoss，再进行调用ShowSum
            ((VisitorBoss)boss).ShowSum();
        }
    }
}
