namespace XSurvey
{
    partial class frm_user_LogIn
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_user_LogIn));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.btn_logIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_tip = new System.Windows.Forms.Label();
            this.btn_close_fm_logIn = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pic_user = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_user)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "登陆 (1).png");
            this.imageList1.Images.SetKeyName(1, "登陆.png");
            this.imageList1.Images.SetKeyName(2, "用户 (1).png");
            this.imageList1.Images.SetKeyName(3, "用户.png");
            this.imageList1.Images.SetKeyName(4, "密码 (1).png");
            this.imageList1.Images.SetKeyName(5, "密码.png");
            this.imageList1.Images.SetKeyName(6, "密码 (1).png");
            this.imageList1.Images.SetKeyName(7, "密码 (2).png");
            this.imageList1.Images.SetKeyName(8, "密码成功.png");
            this.imageList1.Images.SetKeyName(9, "密码-锁.png");
            this.imageList1.Images.SetKeyName(10, "关闭 (1).png");
            this.imageList1.Images.SetKeyName(11, "关闭.png");
            this.imageList1.Images.SetKeyName(12, "bitbug_favicon.ico");
            this.imageList1.Images.SetKeyName(13, "logo.png");
            // 
            // tb_userName
            // 
            this.tb_userName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_userName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_userName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_userName.Location = new System.Drawing.Point(66, 167);
            this.tb_userName.Multiline = true;
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(190, 27);
            this.tb_userName.TabIndex = 1;
            this.tb_userName.Text = "hwh";
            // 
            // tb_password
            // 
            this.tb_password.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_password.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_password.Location = new System.Drawing.Point(66, 243);
            this.tb_password.Multiline = true;
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(190, 29);
            this.tb_password.TabIndex = 2;
            this.tb_password.Text = "123456";
            // 
            // btn_logIn
            // 
            this.btn_logIn.BackColor = System.Drawing.Color.Transparent;
            this.btn_logIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_logIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_logIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_logIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_logIn.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_logIn.Location = new System.Drawing.Point(35, 315);
            this.btn_logIn.Name = "btn_logIn";
            this.btn_logIn.Size = new System.Drawing.Size(221, 36);
            this.btn_logIn.TabIndex = 3;
            this.btn_logIn.Text = "登   陆";
            this.btn_logIn.UseVisualStyleBackColor = false;
            this.btn_logIn.Click += new System.EventHandler(this.btn_logIn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(41, 539);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "欢迎使用X-测量系统";
            // 
            // lb_tip
            // 
            this.lb_tip.BackColor = System.Drawing.Color.Transparent;
            this.lb_tip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_tip.ForeColor = System.Drawing.Color.Red;
            this.lb_tip.Location = new System.Drawing.Point(41, 197);
            this.lb_tip.Name = "lb_tip";
            this.lb_tip.Size = new System.Drawing.Size(220, 30);
            this.lb_tip.TabIndex = 8;
            this.lb_tip.Text = "数据库连接取消，任意账户登录";
            // 
            // btn_close_fm_logIn
            // 
            this.btn_close_fm_logIn.BackColor = System.Drawing.Color.Transparent;
            this.btn_close_fm_logIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_close_fm_logIn.FlatAppearance.BorderSize = 0;
            this.btn_close_fm_logIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btn_close_fm_logIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_close_fm_logIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_fm_logIn.ImageKey = "关闭.png";
            this.btn_close_fm_logIn.ImageList = this.imageList1;
            this.btn_close_fm_logIn.Location = new System.Drawing.Point(280, -2);
            this.btn_close_fm_logIn.Name = "btn_close_fm_logIn";
            this.btn_close_fm_logIn.Size = new System.Drawing.Size(33, 23);
            this.btn_close_fm_logIn.TabIndex = 7;
            this.btn_close_fm_logIn.UseVisualStyleBackColor = false;
            this.btn_close_fm_logIn.Click += new System.EventHandler(this.btn_close_fm_logIn_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Location = new System.Drawing.Point(0, 539);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(36, 23);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(36, 243);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 29);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(35, 167);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 27);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pic_user
            // 
            this.pic_user.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_user.BackgroundImage")));
            this.pic_user.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_user.Image = global::毕设_测量平差程序设计.Properties.Resources.头像;
            this.pic_user.Location = new System.Drawing.Point(111, 62);
            this.pic_user.Name = "pic_user";
            this.pic_user.Size = new System.Drawing.Size(85, 65);
            this.pic_user.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_user.TabIndex = 9;
            this.pic_user.TabStop = false;
            this.pic_user.Click += new System.EventHandler(this.pic_user_Click);
            // 
            // frm_user_LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(311, 574);
            this.Controls.Add(this.pic_user);
            this.Controls.Add(this.lb_tip);
            this.Controls.Add(this.btn_close_fm_logIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btn_logIn);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_userName);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frm_user_LogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登陆";
            this.Load += new System.EventHandler(this.frm_user_LogIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_user)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btn_logIn;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_close_fm_logIn;
        private System.Windows.Forms.Label lb_tip;
        private System.Windows.Forms.PictureBox pic_user;
    }
}