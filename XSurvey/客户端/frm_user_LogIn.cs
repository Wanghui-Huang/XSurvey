using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Drawing.Drawing2D;

namespace XSurvey
{
    public delegate void logInHandler(string userName);               //建立处理登陆委托
    public partial class frm_user_LogIn : Form
    {
        public event logInHandler succssfulLanding;                  //成功登陆事件
        public frm_user_LogIn()
        {
            InitializeComponent();
            this.KeyDown += key_enterDown;      //绑定enter按下处理方法
            this.Shown += frm_user_show;        //绑定窗体打开方法
        }
    
        private void btn_logIn_Click(object sender, EventArgs e)
        {

            logIn();
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        public void logIn()
        {
           
            string userName = tb_userName.Text,
                   passWord = tb_password.Text;
            lb_tip.Text = "此版本已取消数据库连接，任意账户登录";
            if (userName == "" || passWord == "")//提示未输入用户名/密码
            {
                lb_tip.Text = "请输入完整用户名和密码！";
            }
            else
            { //开始进行数据库连接
                try
                {
                    /*ATTENTION
                     *   此版本暂时【取消】数据库连接--因数据库为本地数据库,TA人使用无法连接数据库
                     *   待更新数据库--放置远程服务器
                     */
                     /*
                    string      constr = @"Data Source=(local)\HWHSQL;Initial Catalog=XSurvey;Integrated Security=True";   //设置连接对象所需通讯录路径（参数）
                    SqlConnection con = new SqlConnection(constr);                                   //初始化连接对象
                    StringBuilder sql = new StringBuilder();                                         //初始化stringBuilder对象（可以格式化适合sql语句）                                        
                    sql.AppendFormat("select count(*) from XUser where userName  = '{0}' And userPassWord = '{1}'", tb_userName.Text, tb_password.Text);
                    SqlCommand cmd = new SqlCommand(sql.ToString(), con);                            //初始化sql命名对象,一个sql语句参数，一个连接对象参数    
                    con.Open();                                                                      //初始工作完成，打开连接对象方法，开始执行sql语句
                    int result = (int)cmd.ExecuteScalar();
                    con.Close();
                    */
                    
                    if (true)//成功登陆
                    {
                        this.DialogResult = DialogResult.OK;            //返回一个登陆成功状态
                        succssfulLanding(userName);                     //处理事件实例（参数指定），事件处理方法在主窗体中，这样成功将用户名参数传递到主窗体处理方法中
                        this.Close();
                       
                    }
                    else
                    {
                        lb_tip.Text = "用户不存在 OR 密码不正确！";
                    }
                }
                catch (Exception ep)
                {
                    MessageBox.Show(ep.ToString(), "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// enter键按下直接登陆
        /// </summary>
        private void key_enterDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    logIn();
                    break;
            }
        }

        /// <summary>
        /// 窗体加载时把用户头像框设置为圆形
        /// </summary>
        private void frm_user_show(object sender, EventArgs e)
        {
            //重画头像为圆形
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(pic_user.ClientRectangle, 0, 360);
            Region region = new Region(gp);
            pic_user.Region = region;
            gp.Dispose();
            region.Dispose();
        }
        private void btn_close_fm_logIn_Click(object sender, EventArgs e)
        {
            this.Close();       //关闭登陆窗体
        }

        private void pic_user_Click(object sender, EventArgs e)
        {

        }

        private void frm_user_LogIn_Load(object sender, EventArgs e)
        {

        }
    }
}
