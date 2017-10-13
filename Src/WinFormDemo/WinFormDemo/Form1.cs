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
    public partial class Form1 : Form
    {
        /// <summary>
        /// 窗体构造函数
        /// </summary>
        public Form1()
        {
            //窗体控件初始化
            InitializeComponent();
        }

        /// <summary>
        /// 临时变量保存登录用户
        /// </summary>
        List<User> listUser = new List<User>();

        /// <summary>
        /// 登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string userID = txtUserID.Text;
            string pwd = txtPwd.Text;
            bool res = CheckIsExist(userID, pwd);
            if (res)
            {
                FrmMain frm = new FrmMain();
                
                //将当前窗体对象赋值给新窗体的成员，以便进行再显示
                frm.frmPrev = this;
                
                //将方法传递给新窗体进行委托调用
                frm.actionPrev = new Action(ShowSelf);

                this.Hide();
                frm.Show();
            }
            else
            {
                MessageBox.Show("失败");
            }
        }

        private bool CheckIsExist(string userID, string pwd)
        {
            foreach (User u in listUser)
            {
                if (u.UserID == userID && u.Pwd == pwd)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 模拟用户注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            listUser.Add(new User() { UserID = txtUserID.Text, Pwd = txtPwd.Text });
            MessageBox.Show("注册成功" + txtUserID.Text);
        }

        private void ShowSelf()
        {
            this.Show();
            //todo.......
        }

    }
}
