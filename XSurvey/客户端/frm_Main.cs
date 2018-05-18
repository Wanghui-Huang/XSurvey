using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using hwh类库;

namespace XSurvey
{
    public partial class frm_main : Form
    {
        string title;           //保存交会计算界面的标题

        //摄影测量 
        //内方位元素
        public int n { get; set; } //待算像点对数
        public int N { get; set; } //控制点数
        public double f { get; set; }
        public double m { get; set; }
        public bool rendezvousChecked { get; set; }

        //已知数据
        public double[] x1 { get; set; }
        public double[] y1 { get; set; }
        public double[] x2 { get; set; }
        public double[] y2 { get; set; }
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public double[] Z { get; set; }

        /// <summary>
        /// 构造函数，用于一开始隐藏主窗体，显示用户登陆界面
        /// </summary>
        public frm_main()
        {
            InitializeComponent();
            title = rb_message.Text;
            //一运行先立即隐藏主窗体,this.hide()没有用！
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            btn_user_();
            if (frm_userLogIn.DialogResult == DialogResult.OK)//登陆成功关闭登陆窗体，打开主窗体。
            {
                frm_userLogIn.Close();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
        }


        /// <summary>
        /// 导线平差        
        /// </summary>
        private void traverseAdjust()
        {
            double fB, K, f, fx, fy;
            //进行步骤②
            #region 界面控件数组
            TextBox[] tb_viewAngle   = new TextBox[16] { VA1, VA2, VA3, VA4, VA5, VA6, VA7, VA8, VA9, VA10, VA11, VA12, VA13, VA14, VA15, VA16 };
            TextBox[] tb_VA_adj_num  = new TextBox[16] { _V1, _V2, _V3, _V4, _V5, _V6, _V7, _V8, _V9, _V10, _V11, _V12, _V13, _V14, _V15, _V16 };
            TextBox[] tb_VA_adj      = new TextBox[16] { dA1, dA2, dA3, dA4, dA5, dA6, dA7, dA8, dA9, dA10, dA11, dA12, dA13, dA14, dA15, dA16 };
            TextBox[] tb_Azimuth_adj = new TextBox[17] { A0, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15, A16 };
            TextBox[] tb_distance    = new TextBox[16] { _D1, _D2, _D3, _D4, _D5, _D6, _D7, _D8, _D9, _D10, _D11, _D12, _D13, _D14, _D15, _D16 };
            TextBox[] tb_detx        = new TextBox[16] { dx1, dx2, dx3, dx4, dx5, dx6, dx7, dx8, dx9, dx10, dx11, dx12, dx13, dx14, dx15, dx16 };
            TextBox[] tb_dety        = new TextBox[16] { dy1, dy2, dy3, dy4, dy5, dy6, dy7, dy8, dy9, dy10, dy11, dy12, dy13, dy14, dy15, dy16 };
            TextBox[] tb_adj_detx    = new TextBox[16] { Vx1, Vx2, Vx3, Vx4, Vx5, Vx6, Vx7, Vx8, Vx9, Vx10, Vx11, Vx12, Vx13, Vx14, Vx15, Vx16 };
            TextBox[] tb_adj_dety    = new TextBox[16] { Vy1, Vy2, Vy3, Vy4, Vy5, Vy6, Vy7, Vy8, Vy9, Vy10, Vy11, Vy12, Vy13, Vy14, Vy15, Vy16 };
            TextBox[] tb_X           = new TextBox[16] { X1, X2, X3, X4, X5, X6, X7, X8, X9, X10, X11, X12, X13, X14, X15, X16 };
            TextBox[] tb_Y           = new TextBox[16] { Y1, Y2, Y3, Y4, Y5, Y6, Y7, Y8, Y9, Y10, Y11, Y12, Y13, Y14, Y15, Y16 };
            #endregion
            double[] viewAngel = new double[16];
            double[] distance = new double[16];
            //进行步骤③
            string tempVA, tempD;
            int VA_num = 0; //观测角个数
            angleTransform AT = new angleTransform();

            /*
             * 1.将观测角转换成度表达形式 & 将距离写入double[] 数组
             */

            for (int i = 0; i < tb_viewAngle.Length; i++)
            {
                if (tb_viewAngle[i].Text != "")
                {
                    tempVA = Convert.ToString(AT.AMStoA(tb_viewAngle[i].Text));
                    tempD = tb_distance[i].Text;
                    viewAngel[VA_num] = double.Parse(tempVA);
                    if (tempD != "")
                    {
                        distance[VA_num] = double.Parse(tempD);
                    }
                    VA_num++;
                }
            }
            /*
             * 2.求观测角之和 
             */

            double sum_viewAngle = 0;
            sum_viewAngle        = math.sumOfArr(viewAngel);      //求和函数

            /*
             * 3.计算未改正前坐标方位角
             */

            wire W = new wire(viewAngel, distance, VA_num);
            double[] Coodinate_Azi     = new double[VA_num + 1];
            double[] adj_Coodinate_Azi = new double[VA_num + 1];
            //获取初始坐标方位角
            if (A0.Text == "") { Coodinate_Azi[0] = W.Azimuth1(X0.Text, Y0.Text, X1.Text, Y1.Text); }
            else               { Coodinate_Azi[0] = AT.AMStoA(A0.Text);                             }
            //获取终坐标方位角
            if (A16.Text == "") { Coodinate_Azi[VA_num] = W.Azimuth1(X16.Text, Y16.Text, X17.Text, Y17.Text); }
            else                { Coodinate_Azi[VA_num] = AT.AMStoA(A16.Text);                                }
            //写入剩余坐标方位角
            W.fB = fB = W.adj_Azimuth(Coodinate_Azi, viewAngel);

            /*
             * 4.修正方位角
             */

            //得出各观测角对应改正数和改正角
            int[] adj_VA_num = W.VA_adj_num(fB, VA_num);
            double[] adj_VA  = new double[VA_num];
            for (int i = 0; i < VA_num; i++)
            {
                adj_VA[i] = viewAngel[i] + adj_VA_num[i] / 3600.0;
            }

            //写入改正数和改正角 
            int sum_VA_adj_num = math.sumOfArr(adj_VA_num);
            double sum_VA_adj  = math.sumOfArr(adj_VA);
            //计算修正后方位角
            double adj_fB = W.adj_Azimuth(Coodinate_Azi, adj_VA);
    
            /*
             * 5.计算detx，dety
             */

            //求距离之和
            double[] D    = new double[VA_num - 1];
            double[] detx = new double[VA_num - 1];
            double[] dety = new double[VA_num - 1];
            double Radian = 0;      //弧度      
            double sumD   = 0;
            double sum_detx = 0;
            double sum_dety = 0;
            for (int i = 0; i < D.Length; i++)
            {
                D[i] = Convert.ToDouble(tb_distance[i].Text);
            }
            //计算detx，dety
            for (int i = 0; i < D.Length; i++)
            {
                Radian  = AT.AtoR(Coodinate_Azi[i + 1]);       //方法AtoR：度转换成弧度
                detx[i] = Math.Round(D[i] * Math.Cos(Radian), 3);
                dety[i] = Math.Round(D[i] * Math.Sin(Radian), 3);
            }
            //求和
            sumD     = math.sumOfArr(D);
            sum_detx = math.sumOfArr(detx);
            sum_dety = math.sumOfArr(dety);

            /*
             * 6.改正detx，dety
             */

            double[] adj_detx   = new double[VA_num - 1];
            double[] adj_dety   = new double[VA_num - 1];
            double sum_adj_detx = 0;
            double sum_adj_dety = 0;
            //计算各类限差
            double XB = Convert.ToDouble(X1.Text);
            double YB = Convert.ToDouble(Y1.Text);
            double XC = Convert.ToDouble(X16.Text);
            double YC = Convert.ToDouble(Y16.Text);
            W.fx = fx = sum_detx - (XC - XB);
            W.fy = fy = sum_dety - (YC - YB);
            W.f  = f  = Math.Sqrt(fx * fx + fy * fy);
            W.K  = K  = Math.Round(sumD / f);
            for (int i = 0; i < adj_detx.Length; i++)
            {
                adj_detx[i] = Math.Round((detx[i] - fx / sumD * D[i]), 3);
                adj_dety[i] = Math.Round((dety[i] - fy / sumD * D[i]), 3);
            }
            sum_adj_detx = math.sumOfArr(adj_detx);
            sum_adj_dety = math.sumOfArr(adj_dety);

            /*
             * 7.计算坐标X,坐标Y
             */

            //计算
            double accum_Vx = 0;  //累加x
            double accum_Vy = 0;  //累加y
            double[] X = new double[VA_num - 1];
            double[] Y = new double[VA_num - 1];
            for (int i = 0; i < VA_num - 2; i++)
            {
                accum_Vx += adj_detx[i];
                accum_Vy += adj_dety[i];
                X[i + 1]  = Math.Round(XB + accum_Vx, 3);
                Y[i + 1]  = Math.Round(YB + accum_Vy, 3);
            }

            /* 8.输出 */

            //输出每列计算
            W.print     (0, tb_VA_adj_num, adj_VA_num);
            W.printByAMS(0, tb_VA_adj, adj_VA);
            W.printByAMS(0, tb_Azimuth_adj, Coodinate_Azi, false);
            W.print(0, tb_detx, detx, false);
            W.print(0, tb_dety, dety, false);
            W.print(0, tb_adj_detx, adj_detx, false);
            W.print(0, tb_adj_dety, adj_dety, false);
            W.print(1, tb_X, X, false);
            W.print(1, tb_Y, Y, false);

            //输出求和值
            SUM_VA.Text   = AT.AtoAMS(sum_viewAngle);  //将观测角之和值写入
            SUM_Vnum.Text = sum_VA_adj_num.ToString();
            SUM_dA.Text   = AT.AtoAMS(sum_VA_adj);
            SUM_D_.Text   = sumD.ToString();
            SUM_dx.Text   = sum_detx.ToString();
            SUM_dy.Text   = sum_dety.ToString();
            SUM_Vx.Text   = sum_adj_detx.ToString();
            SUM_Vy.Text   = sum_adj_dety.ToString();

            //输出辅助计算信息
            lb_aidMessage.Text = W.WireAidResult();
        }


        /// <summary>
        /// 水准平差
        /// </summary>
        private void lever()
        {
            //先将界面控件组成数组
            TextBox[] tb_D  = new TextBox[] { D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30, D31, D32, D33, D34 };
            TextBox[] tb_h  = new TextBox[] { h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23, h24, h25, h26, h27, h28, h29, h30, h31, h32, h33, h34 };
            TextBox[] tb_V  = new TextBox[] { V1, V2, V3, V4, V5, V6, V7, V8, V9, V10, V11, V12, V13, V14, V15, V16, V17, V18, V19, V20, V21, V22, V23, V24, V25, V26, V27, V28, V29, V30, V31, V32, V33, V34 };
            TextBox[] tb_Vh = new TextBox[] { Vh1, Vh2, Vh3, Vh4, Vh5, Vh6, Vh7, Vh8, Vh9, Vh10, Vh11, Vh12, Vh13, Vh14, Vh15, Vh16, Vh17, Vh18, Vh19, Vh20, Vh21, Vh22, Vh23, Vh24, Vh25, Vh26, Vh27, Vh28, Vh29, Vh30, Vh31, Vh32, Vh33, Vh34 };
            TextBox[] tb_E  = new TextBox[] { E0, E1, E2, E3, E4, E5, E6, E7, E8, E9, E10, E11, E12, E13, E14, E15, E16, E17, E18, E19, E20, E21, E22, E23, E24, E25, E26, E27, E28, E29, E30, E31, E32, E33, E34 };
            //计算路段数
            int n = 0;
            for (int i = 0; i < tb_h.Length; i++)
            {
                if (tb_h[i].Text != "")
                {
                    n++;
                }
            }
            //定义相应数组
            double[] D  = new double[n];
            double[] h  = new double[n];
            //写入已知数据
            for (int i = 0; i < n - 1; i++)
            {
                if (tb_h[i].Text != "")
                {
                    D[i] = double.Parse(tb_D[i].Text);
                    h[i] = double.Parse(tb_h[i].Text);
                }
            }
            //写入最后一个数据
            D[n - 1] = double.Parse(tb_D[33].Text);
            h[n - 1] = double.Parse(tb_h[33].Text);

            //水准高差计算
            level l = new level( D, h);
            l.E[0]  = double.Parse(E0.Text);
            l.E[n]  = double.Parse(E34.Text);
            l.leverAdjust();

            //在界面写入数据
            //求和数据
            SUM_D .Text  = SUM_D1 .Text = Math.Round(l.SUM[0], 3).ToString();
            SUM_h .Text  = SUM_h1 .Text = Math.Round(l.SUM[1], 3).ToString();
            SUM_V .Text  = SUM_V1 .Text = Math.Round(l.SUM[2], 3).ToString();
            SUM_Vh.Text  = SUM_Vh1.Text = Math.Round(l.SUM[3], 3).ToString();
            SUM_E .Text  = SUM_E1 .Text = Math.Round(l.SUM[4], 3).ToString();
            //列写入
            for (int i = 0;i < n - 1;i++)
            {
                tb_V [i].Text    = Math.Round(l.V[i]   , 4).ToString();
                tb_Vh[i].Text    = Math.Round(l.Vh[i]  , 3).ToString();
                tb_E [i+1].Text  = Math.Round(l.E[i+1] , 3).ToString();
            }
            tb_V [33].Text = Math.Round(l.V[n - 1], 4 ).ToString();
            tb_Vh[33].Text = Math.Round(l.Vh[n - 1],4 ).ToString();
            //辅助计算
            lb_level_aidResult.Text = l.LeverAidResult();
        }


        /// <summary>
        ///后-前方交会计算
        /// </summary>
        private void rendezvous()
        {
            //后方分别计算两片相片的外方位元素
            rendezvous r = new rendezvous(N,n,f,m,rendezvousChecked,x1,y1,x2,y2,X,Y,Z);
            r.mtx += "\n          -----------------------------以下是左片后方交会计算结果-------------------------------";
            Matrix X1 = r.resection(1);
            r.mtx += "\n          -----------------------------以下是右片后方交会计算结果-------------------------------";
            Matrix X2 = r.resection(2);
            //前方计算
            r.mtx += "\n                    ---------------------------- 后方交会 E N D--------------------------------";
            r.forwardRsection(X1, X2);
            //显示后方/前方计算结果
            r.mtx += "\n   -----------------------------------------  E   N   D-------------------------------------------";
            rb_message.Text += r.mtx;

        }


        /// <summary>
        /// 和 frm_rendezvousSet中setOK事件绑定，实现窗体传值，生成格网等
        /// </summary>
        public void setOk(int ctr_potNum, int unknowPotNum, double f, double m, bool rendezvousChecked)
        {
            this.N = ctr_potNum;
            this.n = unknowPotNum;
            this.f = f;
            this.m = m;
            this.rendezvousChecked = rendezvousChecked;
            if (rendezvousChecked)
            {
                lb_title.Text = "后 - 前 方 交 会";
            }
            else
            {
                lb_title.Text = "相 对 - 绝 对 定 向";
            }
            //先删除原有格网和数据
            int countRows = dgv_date.Rows.Count;             //dgv_date.Rows.Count值其实在不断减少！
            int countCols = dgv_date.ColumnCount;
            for (int i = 0; i < countRows; i++)
            {
                for (int j = 1; j < countCols; j++)
                {
                    dgv_date.Rows[0].Cells[j].Value = "";       //删除行所有数据
                }
                dgv_date.Rows.RemoveAt(0);              //坑啊！！这里不能写i，假设要删除最后一个（第8个），但是其实前面7个全部删除，此时第八个索引是0！而不是7！
            }
            //显示格网
            for (int i = 0; i < ctr_potNum + unknowPotNum; i++)
            {    
                dgv_date.Rows.Add();
                dgv_date.Rows[i].Cells[0].Style = new DataGridViewCellStyle { BackColor = Color.MintCream};
                if (i < ctr_potNum)
                {
                    dgv_date.Rows[i].Cells[0].Value = "GCP" + (i + 1);
                }
                else
                {
                    dgv_date.Rows[i].Cells[0].Value =   (i - ctr_potNum + 1) ;
                }
            }
            //输入测试数据
            string[,] testDate1 = new string[,] { { "16.012","79.963","-73.93","78.706","5083.205","5852.099","527.925"},
                                                 { "88.56","81.134","-5.252","78.184","5780.02","5906.365","571.549" },
                                                 {"13.362","-79.37","-79.122","-78.879","5210.879","4258.446","461.81" },
                                                 {"82.24","-80.027","-9.887","-80.089","5909.264","4314.283","455.484" },
                                                 {"51.758","81.555","-39.953","78.463","","" ,""  } ,
                                                 {"14.618","-0.231","-76.016","0.036","","" ,"" } ,
                                                 { "49.88","-0.792","-42.201","-1.022","","" ,""},
                                                 { "86.243","-1.346","-7.706","-2.112","","" ,""},
                                                 { "48.135","-79.962","-44.438","-79.736","","" ,""} };

            for (int i = 0; i <9; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    dgv_date.Rows[i].Cells[j].Value = testDate1[i, j -1];
                }
            }
        }


        /// <summary>
        /// 相对-绝对定向
        /// <summary>
        public void Orientation()
        {
            //相对定向
            orientation O = new orientation(N, n, f, m, rendezvousChecked, x1, y1, x2, y2, X, Y, Z);
            O.mtx += "\n     ----------------------------------------相  对  定  向---------------------------------------";
            Matrix X1 =  O.relative_orientation();
            O.mtx += "\n        ------------------------------------相对定向 E   N   D-------------------------------------\n";
            //前方交会计算控制点像空间辅助坐标
            O.mtx += "\n      -------------------------------前方交会计算像点像空间辅助坐标---------------------------- \n";
            for (int i = 0; i < n + N; i++)
            {
                O.mtx += "\n         第 " + (i + 1) + " 对模型点像空间辅助坐标为 :  （" + Math.Round(O.U[i], 5) + " , " + Math.Round(O.V[i], 5) + " , " + Math.Round(O.W[i], 5) + ")";
            }
            O.mtx += "\n\n    ----------------------------------------------------------------------------------------------- \n";
            double[] Uc = new double[N];
            double[] Vc = new double[N];
            double[] Wc = new double[N];
            double[] Up = new double[n];
            double[] Vp = new double[n];
            double[] Wp = new double[n];
            O.getUVW(true , Uc, Vc, Wc);
            O.getUVW(false, Up, Vp, Wp);
            //绝对定向
            O.mtx += "\n       ----------------------------------------绝  对  定  向---------------------------------------";
            Matrix X2 = O.obsolute_orientation(Uc,Vc,Wc);
            O.mtx += "\n    ------------------------------------绝对定向 E   N   D-------------------------------------";
            //计算待算点的摄影测量坐标
            O.mtx += "\n          -------------------------------地面摄影测量坐标为------------------------------- \n";
            O.getPhotogrammetricCoordinate(X2, Up, Vp, Wp);
            O.mtx += "\n\n        --------------------------------------E  N  D-------------------------------------\n";
            //显示辅助计算数据
            rb_message.Text += O.mtx;
        }

        #region 实现截图保存、清除数据的方法
        /// <summary>
        /// 清除已输入的观测数据和成果
        /// </summary>
        private void clean()
        {
            //提示用户是否清除数据

            foreach (Control c in tc_main.SelectedTab.Controls)
            {
                if (c is TextBox && !c.Name.Contains("tb_P"))
                {
                    c.Text = "";
                }
                if (c is Label && c.Name.Contains("aid"))
                {
                    c.Text = "";
                }
            }
        }
        /// <summary>
        /// 实现截取当前page页面，然后保存
        /// </summary>
        private void saveBy_pic()
        {
            //隐藏按钮，方便截图
            foreach (Control c in tc_main.SelectedTab.Controls)
            {
                if (c is Button)
                {
                    c.Visible = false;
                }
            }
            //开始截图
            Bitmap bmp = new Bitmap(this.tc_main.Width, this.tc_main.Height);
            this.tc_main.DrawToBitmap(bmp, new Rectangle(0, 0, this.tc_main.Width, this.tc_main.Height));
            //显示按钮
            foreach (Control c in tc_main.SelectedTab.Controls)
            {
                if (c is Button)
                {
                    c.Visible = true;
                }
            }
            //保存图片
            string savePath = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "内业计算成果保存";
            sfd.FileName = "内业计算成果.bmp";
            sfd.Filter = "bmp图片（*.bmp）|*.bmp";   //设置文件类型        
            sfd.FilterIndex = 1;                      //设置默认文件类型显示顺序       
            sfd.RestoreDirectory = true;             //保存对话框是否记忆上次打开的目录
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                savePath = sfd.FileName.ToString();                                           //获得文件路径 
                bmp.Save(savePath);
            }
        }

        /// <summary>
        /// 保存辅助输出结果为txt文档
        /// </summary>
        private void saveBy_txt()
        {
            //保存txt
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "计算成果保存";
            sfd.FileName = "计算成果.txt";
            sfd.Filter = "txt文档（*.txt）|*.txt";   //设置文件类型        
            sfd.FilterIndex = 1;                      //设置默认文件类型显示顺序       
            sfd.RestoreDirectory = true;             //保存对话框是否记忆上次打开的目录
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string saveTXT = sfd.FileName;
                //在指定目录创建了一个txt文档
                FileStream fs =  File.Create(saveTXT);
                fs.Close();        
                using (StreamWriter sw = new StreamWriter(saveTXT))
                {
                    sw.Write(rb_message.Text.Replace("\n","\r\n"));  //txt文档，/r/n才是换行符！！！
                }
            }
        }


        #endregion

        /*
         * 菜单栏的功能按钮事件
         */

        frm_user_LogIn frm_userLogIn;
        public void succss_land(string userName)
        {
            lb_aid_左下角.Text = "  欢迎您: " + userName;
        }

        private void btn_user_()
        {
            //实现将登录信息传给  
            frm_userLogIn = new frm_user_LogIn();
            frm_userLogIn.succssfulLanding += new logInHandler(succss_land);   //在主窗体事件绑定方法，因为方法在主窗体中
            frm_userLogIn.ShowDialog();
        }

        private void btn_user_Click(object sender, EventArgs e)
        {
            btn_user_();
        }

        private void Menu_save_Click(object sender, EventArgs e)
        {
            if(tc_main.SelectedIndex == 0 || tc_main.SelectedIndex == 1)
            {
                saveBy_pic();
            }
            if (tc_main.SelectedIndex == 2)
            {
                saveBy_txt();
            }
        }

        private void Menu_exit_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("是否退出程序？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Menu_aboutME_Click(object sender, EventArgs e)
        {
            MessageBox.Show("    主要功能：\n\n       · 附和导线计算\n       · 附和水准计算\n       · 后前方交会 \n       · 相对绝对定向\n \n   版权信息：\n\n        ·本软件系 JXUST 测绘141黄旺辉自主原创开发\n        ·原为18年毕业设计 ", "帮 助", MessageBoxButtons.OK);
        }

        private void Menu_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("    · 数据输入：输入数据可参照附带的测试样图 \n\n    · 联系我：QQ 380141202", "帮 助", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        /*
         * 对窗体最小化，最大化，关闭按钮事件
         */

        bool isNorm = true;
        Point P1, P2, P3;  //分别记录 最小化，最大化，关闭，这三件按钮位置
        private void btn_minSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;     //最小化
        }

        private void btn_max_Click    (object sender, EventArgs e)
        {
            //最大化/正常状态切换 
            if (isNorm)
            {
                //记录下正常状态下的三个按钮位置
                P1 = btn_minSize.Location; P2 = btn_maxSize.Location; P3 = btn_close.Location;
                this.WindowState = FormWindowState.Maximized;    //最大化,然后X值增大
                btn_minSize.Location = new Point(P1.X + 85, P1.Y);
                btn_maxSize.Location = new Point(P2.X + 85, P2.Y);
                btn_close.Location = new Point(P3.X + 85, P3.Y);
                isNorm = false;
            }
            else
            {
                btn_minSize.Location = new Point(P1.X, P1.Y);
                btn_maxSize.Location = new Point(P2.X, P2.Y);
                btn_close.Location = new Point(P3.X, P3.Y);
                this.WindowState = FormWindowState.Normal;
                isNorm = true;
            }
        }

        private void btn_close_Click  (object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("是否退出程序？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /*
         * 主界面左栏按钮事件
         */
        private void btn_wire_Click(object sender, EventArgs e)
        {
            tc_main.SelectTab(0);
        }

        private void btn_level_Click(object sender, EventArgs e)
        {
            tc_main.SelectTab(1);
        }

        private void btn_rendezvous_Click(object sender, EventArgs e)
        {
            tc_main.SelectTab(2);
        }

        /*
         * 附和导线tablePage页面按钮事件
         */

        private void tb_wire_clean_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("是否清除数据？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                clean();
            }
        }

        private void tb_wire_save_Click(object sender, EventArgs e)
        {
            saveBy_pic();
        }
        private void btn_wire_cacul_Click(object sender, EventArgs e)
        {
            frm_limtSetOfWire f_limt = new frm_limtSetOfWire();
            f_limt.ShowDialog();
            try
            {
                traverseAdjust();
            }
            catch(FormatException f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        /*
         * 附和水准tablePage页面按钮事件
         */

        private void btn_level_clean_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("是否清除数据？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                clean();
            }
        }

        private void btn_level_save_Click(object sender, EventArgs e)
        {
            saveBy_pic();
        }

        private void btn_level_cacul_Click(object sender, EventArgs e)
        {
            frm_setOfLevel f = new frm_setOfLevel();
            f.ShowDialog();
            if (XSurvey.level.isByDistance)
            {
                lb_D_N_title.Text = "距 离";
            }
            else
            {
                lb_D_N_title.Text = "测站数";
            }
            if (XSurvey.level.isAttachedLevel)
            {
                lb_lever_title.Text = "附 合 水 准 计 算";
                tb_P34.Text = tb_P_34.Text = "BM2";
            }
            else
            {
                lb_lever_title.Text = "闭 合 水 准 计 算";
                tb_P34.Text = tb_P_34.Text = "BM1";
            }
            try
            {
                lever();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*
         * 交会定向tablePage页面按钮事件
         */

        private void btn_rend_cacul_Click(object sender, EventArgs e)
        {
            rb_message.Text = title;     //清空计算结果,只保存标题
            //设定初始数据
            int N = n + this.N;
            x1 = new double[N];
            y1 = new double[N];
            x2 = new double[N];
            y2 = new double[N];
            X = new double[N];
            Y = new double[N];
            Z = new double[N];
            for (int i = 0; i < N; i++) //遍历每行数据,写入数组
            {
                x1[i] = Convert.ToDouble(dgv_date.Rows[i].Cells[1].Value);
                y1[i] = Convert.ToDouble(dgv_date.Rows[i].Cells[2].Value);
                x2[i] = Convert.ToDouble(dgv_date.Rows[i].Cells[3].Value);
                y2[i] = Convert.ToDouble(dgv_date.Rows[i].Cells[4].Value);
                if (i < 4)
                {
                    X[i] = Convert.ToDouble(dgv_date.Rows[i].Cells[5].Value);
                    Y[i] = Convert.ToDouble(dgv_date.Rows[i].Cells[6].Value);
                    Z[i] = Convert.ToDouble(dgv_date.Rows[i].Cells[7].Value);
                }
            }
            try
            {
                if (rendezvousChecked)
                {
                    rendezvous();
                }
                else
                {
                    Orientation();
                }

            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }

        }

        private void btn_rend_set_Click(object sender, EventArgs e)
        {
            //显示后/前方交会设定界面
            frm_setOfRendezvous f_set = new frm_setOfRendezvous();
            f_set.setOkEvent += setOk;
            f_set.ShowDialog();
        }

        private void btn_RO_save_Click(object sender, EventArgs e)
        {
            saveBy_txt();
        }

        /*
         * 实现窗体移动重写的相关事件 
         */

        private bool m_isMouseDown = false;
        private Point m_mousePos = new Point();
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_mousePos = Cursor.Position;
            m_isMouseDown = true;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_isMouseDown = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_isMouseDown)
            {
                Point tempPos = Cursor.Position;
                this.Location = new Point(Location.X + (tempPos.X - m_mousePos.X), Location.Y + (tempPos.Y - m_mousePos.Y));
                m_mousePos = Cursor.Position;
            }
        }


    }
}
