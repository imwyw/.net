using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormDemo
{
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 窗体-代表上一个窗体
        /// </summary>
        public Form frmPrev;

        /// <summary>
        /// 无参无返回值的委托
        /// </summary>
        public Action actionPrev;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            //frmPrev.Show();
            actionPrev();
            //actionPrev.Invoke();
        }
    }
}
