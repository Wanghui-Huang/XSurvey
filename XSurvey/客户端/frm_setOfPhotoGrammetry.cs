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
    public delegate void setEventHandle(int ctr_potNum, int unknowPotNum, double f, double m, bool rendezvousChecked);
    public partial class frm_setOfRendezvous : Form
    {
        public event setEventHandle setOkEvent;
        public frm_setOfRendezvous()
        {
            InitializeComponent();
        }
        private void frm_rendezvousSet_Load(object sender, EventArgs e)
        {
        }

        private void btn_rendez_setOK_Click(object sender, EventArgs e)
        {
            if (tb_ctr_num.Text != "" && tb_unknowPotNum.Text != "" && tb_f.Text != "" && tb_m.Text != "")
            {
               int ctr_potNum      = int.Parse(tb_ctr_num.Text);
               int unknowPotNum    = int.Parse(tb_unknowPotNum.Text);
               double f            = double.Parse(tb_f.Text);
               double m            = double.Parse(tb_m.Text);
                if (setOkEvent != null)
                {
                    setOkEvent(ctr_potNum,unknowPotNum,f,m,rb_rendezvous.Checked);
                }
                this.Close();
            }
            else
            {
                lb_messge.Visible = true;
            }
        }
    }
}
