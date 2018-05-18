using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XSurvey
{

    public partial class frm_limtSetOfWire : Form
    {
        public frm_limtSetOfWire()
        {
            InitializeComponent();
            tb_f.TextChanged += textChange_checkIsNum;
            tb_fx.TextChanged += textChange_checkIsNum;
            tb_fy.TextChanged += textChange_checkIsNum;
            tb_K.TextChanged += textChange_checkIsNum;
            tb_fβ.TextChanged += textChange_checkIsNum;
        }
        private void btn_hide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btn__Click(object sender, EventArgs e)
        {
            completSet();
        }
        /// <summary>
        /// 限差设定完成
        /// </summary>
        bool isOk = false ;
        private void completSet()
        {
            try
            {
                double fβ_limt, fx_limt, fy_limt, f_limt, K_limt;
                fβ_limt = Convert.ToDouble(tb_fβ.Text); f_limt = Convert.ToDouble(tb_f.Text); fx_limt = Convert.ToDouble(tb_fx.Text); fy_limt = Convert.ToDouble(tb_fy.Text); K_limt = Convert.ToDouble(tb_K.Text);
                if ( isOk ) 
                {
                    wire.fB_limt = fβ_limt;
                    wire.K_limt  = K_limt;
                    wire.f_limt  = f_limt;
                    wire.fx_limt = fx_limt;
                    wire.fy_limt = fy_limt;
                    wire.isLeft  = rb_leftAngle.Checked ;
                }
                this.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 用户输入时检测是否是double类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textChange_checkIsNum(Object sender, EventArgs e)
        {
            double tmp;
            string s   = (sender as TextBox).Text;
            if (!double.TryParse(s, out tmp))
            {
                isOk = false;
                lb_messge.Visible = true;
                (sender as TextBox).Text = "";
            }
            else
            {
                lb_messge.Visible = false;
                isOk = true;
            }
        }
    }
}
