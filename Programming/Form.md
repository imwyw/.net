<!-- TOC -->

- [桌面编程](#桌面编程)
    - [WinForm](#winform)
        - [窗体交互](#窗体交互)
        - [数据共享](#数据共享)
        - [异步编程](#异步编程)

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
        frm.frmPrev = this;
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
    public Form frmPrev;

    /// <summary>
    /// 返回到父窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        this.Hide();
        frmPrev.Show();
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
        frm.actionPrev = new Action(delegate ()
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
    public Action actionPrev;

    /// <summary>
    /// 返回到父窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        this.Hide();
        actionPrev.Invoke();
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
        frm.actionPrev = new Action(delegate ()
        {
            this.Show();
        });
        frm.actionPrev += new Action(RefreshText);

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
    public Action actionPrev;

    /// <summary>
    /// 返回到父窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        this.Hide();
        actionPrev.Invoke();
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

### 异步编程
实现检索硬盘内的图片信息，异步编程实现：

![](..\assets\WinForm\异步编程案例.gif)

**APM**

.NET最早出现的异步编程模式被称为APM(Asynchronous Programming Model)。这种模式主要由一对Begin/End开头的组成。BeginXXX方法用于启动一个耗时操作（需要异步执行的代码段），相应的调用EndXXX来结束BeginXXX方法开启的异步操作。BeginXXX方法和EndXXX方法之间的信息通过一个IAsyncResult对象来传递。这个对象是BeginXXX方法的返回值。如果直接调用EndXXX方法，则将以阻塞的方式去等待异步操作完成。另一种更好的方法是在BeginXXX倒数第二个参数指定的回调函数中调用EndXXX方法，这个回调函数将在异步操作完成时被触发，回调函数的第二个参数即EndXXX方法所需要的IAsyncResult对象。

APM使用简单明了，虽然代码量稍多，但也在合理范围之内。APM两个最大的缺点是不支持进度报告以及不能方便的“取消”。



参考：

[C#5.0异步编程巅峰](https://www.bbsmax.com/A/VkPzOV7dxn/)






