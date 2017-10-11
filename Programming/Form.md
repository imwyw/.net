<!-- TOC -->

- [桌面编程](#桌面编程)
    - [WinForm](#winform)
        - [窗体交互](#窗体交互)
        - [数据共享](#数据共享)
        - [多线程示例](#多线程示例)

<!-- /TOC -->
# 桌面编程
## WinForm
### 窗体交互
在窗体间传递数据和方法有很多种，如下图中实现两个窗体之间的互相跳转

![](..\assets\WinForm\QQ截图20171010160139.png)

可以通过实例化时成员的赋值操作实现：

```cs
/// <summary>
/// 父窗体
/// </summary>
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 显示子窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        Form2 frm = new Form2();
        frm.parentForm = this;
        this.Hide();
        frm.Show();
    }
}

/// <summary>
/// 子窗体
/// </summary>
public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 父窗体
    /// </summary>
    public Form parentForm;

    /// <summary>
    /// 返回到父窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        this.Hide();
        parentForm.Show();
    }
}
```

还可以通过方法传递的方式实现：
```cs
/// <summary>
/// 父窗体
/// </summary>
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 显示子窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        Form2 frm = new Form2();
        frm.parentAction = new Action(delegate ()
        {
            this.Show();
        });
        this.Hide();
        frm.Show();
    }
}

/// <summary>
/// 子窗体
/// </summary>
public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 父窗体的方法
    /// </summary>
    public Action parentAction;

    /// <summary>
    /// 返回到父窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        this.Hide();
        parentAction.Invoke();
    }
}
```

### 数据共享
![](..\assets\WinForm\QQ截图20171010165505.png)

窗体之间共享一份数据：
```cs
/// <summary>
/// 父窗体
/// </summary>
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 显示子窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        Form2 frm = new Form2();
        //子窗体隐藏显示主窗体需要执行的方法
        frm.parentAction = new Action(delegate ()
        {
            this.Show();
        });
        frm.parentAction += new Action(RefreshText);

        this.Hide();
        frm.Show();
        frm.RefreshText();
    }

    /// <summary>
    /// 将文本框的值设置到共享数据中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button2_Click(object sender, EventArgs e)
    {
        GlobalData.CurUserName = textBox1.Text;
    }

    /// <summary>
    /// 更新共享数据的值到文本框中
    /// </summary>
    private void RefreshText()
    {
        textBox1.Text = GlobalData.CurUserName;
    }
}

/// <summary>
/// 子窗体
/// </summary>
public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 父窗体的方法
    /// </summary>
    public Action parentAction;

    /// <summary>
    /// 返回到父窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        this.Hide();
        parentAction.Invoke();
    }

    /// <summary>
    /// 将文本框的值设置到共享数据中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button2_Click(object sender, EventArgs e)
    {
        GlobalData.CurUserName = textBox1.Text;
    }

    /// <summary>
    /// 更新共享数据的值到文本框中
    /// </summary>
    public void RefreshText()
    {
        textBox1.Text = GlobalData.CurUserName;
    }
}

/// <summary>
/// 全局共享数据
/// </summary>
public static class GlobalData
{
    public static string CurUserName;
}

```

### 多线程示例






