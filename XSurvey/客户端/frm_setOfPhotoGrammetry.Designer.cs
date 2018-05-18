namespace XSurvey
{
    partial class frm_setOfRendezvous
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_setOfRendezvous));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_ = new System.Windows.Forms.RadioButton();
            this.rb_rendezvous = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_messge = new System.Windows.Forms.Label();
            this.tb_unknowPotNum = new System.Windows.Forms.TextBox();
            this.tb_ctr_num = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_m = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_f = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_rendez_setOK = new System.Windows.Forms.Button();
            this.btn_hide = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_);
            this.groupBox1.Controls.Add(this.rb_rendezvous);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(24, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方 式";
            // 
            // rb_
            // 
            this.rb_.AutoSize = true;
            this.rb_.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb_.Location = new System.Drawing.Point(216, 35);
            this.rb_.Name = "rb_";
            this.rb_.Size = new System.Drawing.Size(117, 24);
            this.rb_.TabIndex = 1;
            this.rb_.Text = "相对-绝对定向";
            this.rb_.UseVisualStyleBackColor = true;
            // 
            // rb_rendezvous
            // 
            this.rb_rendezvous.AutoSize = true;
            this.rb_rendezvous.Checked = true;
            this.rb_rendezvous.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb_rendezvous.Location = new System.Drawing.Point(50, 35);
            this.rb_rendezvous.Name = "rb_rendezvous";
            this.rb_rendezvous.Size = new System.Drawing.Size(117, 24);
            this.rb_rendezvous.TabIndex = 0;
            this.rb_rendezvous.TabStop = true;
            this.rb_rendezvous.Text = "后方-前方交会";
            this.rb_rendezvous.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_messge);
            this.groupBox2.Controls.Add(this.tb_unknowPotNum);
            this.groupBox2.Controls.Add(this.tb_ctr_num);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tb_m);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tb_f);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(23, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(402, 158);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "基 本 设 定";
            // 
            // lb_messge
            // 
            this.lb_messge.BackColor = System.Drawing.Color.Transparent;
            this.lb_messge.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_messge.ForeColor = System.Drawing.Color.Red;
            this.lb_messge.Location = new System.Drawing.Point(141, 121);
            this.lb_messge.Name = "lb_messge";
            this.lb_messge.Size = new System.Drawing.Size(117, 29);
            this.lb_messge.TabIndex = 7;
            this.lb_messge.Text = "请输入合法数据！";
            this.lb_messge.Visible = false;
            // 
            // tb_unknowPotNum
            // 
            this.tb_unknowPotNum.Location = new System.Drawing.Point(113, 75);
            this.tb_unknowPotNum.Name = "tb_unknowPotNum";
            this.tb_unknowPotNum.Size = new System.Drawing.Size(71, 26);
            this.tb_unknowPotNum.TabIndex = 1;
            this.tb_unknowPotNum.Text = "5";
            // 
            // tb_ctr_num
            // 
            this.tb_ctr_num.Location = new System.Drawing.Point(113, 34);
            this.tb_ctr_num.Name = "tb_ctr_num";
            this.tb_ctr_num.Size = new System.Drawing.Size(71, 26);
            this.tb_ctr_num.TabIndex = 1;
            this.tb_ctr_num.Text = "4";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(4, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "待算同名像对:";
            // 
            // tb_m
            // 
            this.tb_m.Location = new System.Drawing.Point(292, 82);
            this.tb_m.Name = "tb_m";
            this.tb_m.Size = new System.Drawing.Size(71, 26);
            this.tb_m.TabIndex = 1;
            this.tb_m.Text = "10000";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(21, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "控制点个数 :";
            // 
            // tb_f
            // 
            this.tb_f.Location = new System.Drawing.Point(292, 31);
            this.tb_f.Name = "tb_f";
            this.tb_f.Size = new System.Drawing.Size(71, 26);
            this.tb_f.TabIndex = 1;
            this.tb_f.Text = "152.000";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(239, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = " f = ";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(239, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "m = ";
            // 
            // btn_rendez_setOK
            // 
            this.btn_rendez_setOK.BackColor = System.Drawing.Color.Transparent;
            this.btn_rendez_setOK.FlatAppearance.BorderSize = 0;
            this.btn_rendez_setOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_rendez_setOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_rendez_setOK.Location = new System.Drawing.Point(23, 318);
            this.btn_rendez_setOK.Name = "btn_rendez_setOK";
            this.btn_rendez_setOK.Size = new System.Drawing.Size(402, 41);
            this.btn_rendez_setOK.TabIndex = 1;
            this.btn_rendez_setOK.Text = "设   定   完   成";
            this.btn_rendez_setOK.UseVisualStyleBackColor = false;
            this.btn_rendez_setOK.Click += new System.EventHandler(this.btn_rendez_setOK_Click);
            // 
            // btn_hide
            // 
            this.btn_hide.BackColor = System.Drawing.Color.Transparent;
            this.btn_hide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_hide.BackgroundImage")));
            this.btn_hide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_hide.FlatAppearance.BorderSize = 0;
            this.btn_hide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_hide.ImageKey = "关闭.png";
            this.btn_hide.Location = new System.Drawing.Point(414, 3);
            this.btn_hide.Name = "btn_hide";
            this.btn_hide.Size = new System.Drawing.Size(29, 14);
            this.btn_hide.TabIndex = 7;
            this.btn_hide.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(1, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 26);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(83, 327);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // frm_setOfRendezvous
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 384);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btn_hide);
            this.Controls.Add(this.btn_rendez_setOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_setOfRendezvous";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "交会定向设定";
            this.Load += new System.EventHandler(this.frm_rendezvousSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_;
        private System.Windows.Forms.RadioButton rb_rendezvous;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_f;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_ctr_num;
        private System.Windows.Forms.TextBox tb_m;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_rendez_setOK;
        private System.Windows.Forms.Button btn_hide;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lb_messge;
        private System.Windows.Forms.TextBox tb_unknowPotNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}