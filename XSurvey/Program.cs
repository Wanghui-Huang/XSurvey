using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XSurvey
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            #region 这种先打开登陆窗体的方法，无法进行将登陆窗体值传给主窗体
            //user_LogIn frm_logIn = new user_LogIn();
            //frm_logIn.ShowDialog();                          //这里：下面代码一直等待登陆窗口返回状态？
            //if (frm_logIn.DialogResult == DialogResult.OK )
            //{
            //    frm_logIn.Close();
            //    Application.Run(new frm_main());
            //}
            #endregion
            Application.Run(new frm_main());
        }
    }
}
