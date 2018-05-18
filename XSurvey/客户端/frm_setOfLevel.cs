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
    public partial class frm_setOfLevel : Form
    {
        public frm_setOfLevel()
        {
            InitializeComponent();
        }

        
        private void btn_hide_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            level.isFourthClass   = rb_theFourthClass.Checked;
            level.isByDistance    = rb_byDistance.Checked;
            level.isAttachedLevel = rb_attachedLevel.Checked;
            this.Close();
        }
    }
}
