using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hwh类库;

namespace XSurvey
{
    class rendezvous : photoGrammetry
    {
        //外方位元素
        public double XS { get; set; }
        public double YS { get; set; }
        public double ZS { get; set; }
        public double φ { get; set; }
        public double ω { get; set; }
        public double K { get; set; }
        public rendezvous(int ctr_potNum, int unknowPotNum, double f, double m, bool rendezvousChecked, double[] x1, double[] y1, double[] x2, double[] y2, double[] X, double[] Y, double[] Z)
               : base(ctr_potNum, unknowPotNum, f, m, rendezvousChecked, x1, y1, x2, y2, X, Y, Z)
        {
        }

        /// <summary>
        /// 后方交会计算
        /// </summary>
        /// <param name="photo">选择第几片相片</param>
        /// <returns>矩阵类型：6个外方位元素</returns>
        public Matrix resection(int photo)
        {
            double[] x = x1,
                     y = y1;
            if (photo == 2)//选择第二片相片
            {
                x = x2;
                y = y2;
            }   
            //解算m比例尺
            if (m == 0)    //用户输入m = 0,代表需要程序就算出m值
            {
                int k = 0;
                int CN2 = math.Cmn(N, 2);
                double[] mArr = new double[CN2];
                for (int i = 0; i < base.N; i++)
                    for (int j = i + 1; j < base.N; j++)
                    {
                        mArr[k] = Math.Sqrt((X[i] - X[j]) * (X[i] - X[j]) + (Y[i] - Y[j]) * (Y[i] - Y[j])) / (Math.Sqrt((x[i] - x[j]) * (x[i] - x[j]) + (y[i] - y[j]) * (y[i] - y[j])) * Math.Pow(10, -3));
                        k++;
                    }
                m = math.avgDArr(mArr);
            }
            //设定外方位元素：XS,YS,ZS 初始值
            XS = math.avgDArr(X, base.N);
            YS = math.avgDArr(Y, base.N);
            ZS = m * f * Math.Pow(10, -3);//单位米
                                           //求lx，ly 单位mm
            Matrix real_X = new Matrix();
            Matrix _f;
            Matrix B;
            Matrix BT;
            Matrix BTB;
            Matrix BTf;
            Matrix dX = new Matrix();
            double[] lx = new double[N];
            double[] ly = new double[N];
            double[] _X = new double[N];
            double[] _Y = new double[N];
            double[] _Z = new double[N];
            double[] Sx = new double[N];     //近似值用共线方程计算
            double[] Sy = new double[N];     //近似值用共线方程计算
            double[] B1 = new double[N * 2];   //系数B第一列，下同
            double[] B2 = new double[N * 2];
            double[] B3 = new double[N * 2];
            double[] B4 = new double[N * 2];
            double[] B5 = new double[N * 2];
            double[] B6 = new double[N * 2];
            double[,] elementf = new double[N * 2, 1];
            double[,] elementB = new double[N * 2, 6];
            double a1 = 0; double a2 = 0; double a3 = 0;
            double b1 = 0; double b2 = 0; double b3 = 0;
            double c1 = 0; double c2 = 0; double c3 = 0;
            double[,] R = new double[3, 3];
            bool dxISOK = false;
            int h = 0;//迭代次数
            do
            {
                R = elementR(φ, K, ω);
                a1 = R[0, 0]; a2 = R[0, 1]; a3 = R[0, 2];
                b1 = R[1, 0]; b2 = R[1, 1]; b3 = R[1, 2];
                c1 = R[2, 0]; c2 = R[2, 1]; c3 = R[2, 2];
                for (int i = 0; i < N; i++)
                {
                    //写入 _X 、_Y、lx[i]、ly[i]
                    _X[i] = a1 * (X[i] - XS) + b1 * (Y[i] - YS) + c1 * (Z[i] - ZS);
                    _Y[i] = a2 * (X[i] - XS) + b2 * (Y[i] - YS) + c2 * (Z[i] - ZS);
                    _Z[i] = a3 * (X[i] - XS) + b3 * (Y[i] - YS) + c3 * (Z[i] - ZS);
                    lx[i] = x[i] + f * _X[i] / _Z[i];
                    ly[i] = y[i] + f * _Y[i] / _Z[i];
                }
                //写入系数
                for (int i = 0; i < N * 2; i++)
                {
                    if (i < N)//前N行写入
                    {
                        B1[i] = (a1 * f + a3 * x[i]) / _Z[i];
                        B2[i] = (b1 * f + b3 * x[i]) / _Z[i];
                        B3[i] = (c1 * f + c3 * x[i]) / _Z[i];
                        B4[i] = y[i] * Math.Sin(ω) - (x[i] / f * (x[i] * Math.Cos(K) - y[i] * Math.Sin(K)) + f * Math.Cos(K)) * Math.Cos(ω);
                        B5[i] = -f * Math.Sin(K) - x[i] / f * (x[i] * Math.Sin(K) + y[i] * Math.Cos(K));
                        B6[i] = y[i];
                        elementf[i, 0] = lx[i];//常数矩阵元素
                    }
                    else//后N行写入
                    {
                        B1[i] = (a2 * f + a3 * y[i - N]) / _Z[i - N];
                        B2[i] = (b2 * f + b3 * y[i - N]) / _Z[i - N]; ;
                        B3[i] = (c2 * f + c3 * y[i - N]) / _Z[i - N];
                        B4[i] = -x[i - N] * Math.Sin(ω) - (y[i - N] / f * (x[i - N] * Math.Cos(K) - y[i - N] * Math.Sin(K)) - f * Math.Sin(K)) * Math.Cos(ω);
                        B5[i] = -f * Math.Cos(K) - y[i - N] / f * (x[i - N] * Math.Sin(K) + y[i - N] * Math.Cos(K));
                        B6[i] = -x[i - N];
                        elementf[i, 0] = ly[i - N];//常数矩阵元素
                    }
                }
                //写入系数矩阵元素B
                for (int i = 0; i < N * 2; i++)
                    for (int j = 0; j < 6; j++)
                    {
                        if (j == 0) { elementB[i, j] = B1[i]; }
                        if (j == 1) { elementB[i, j] = B2[i]; }
                        if (j == 2) { elementB[i, j] = B3[i]; }
                        if (j == 3) { elementB[i, j] = B4[i]; }
                        if (j == 4) { elementB[i, j] = B5[i]; }
                        if (j == 5) { elementB[i, j] = B6[i]; }
                    }
                _f = new Matrix(elementf);
                B = new Matrix(elementB);
                BT = B.Transpose();
                BTB = BT * B;
                BTf = BT * _f;
                dX = new Matrix();
                BTB.InvertGaussJordan();//求逆
                dX = BTB * BTf;
                double[,] elementXo = new double[,] { { XS }, { YS }, { ZS }, { φ }, { ω }, { K } };
                Matrix Xo = new Matrix(elementXo);
                real_X = dX + Xo;
                //开始判断是否符合要求
                double t = 0.3 * Math.Pow(10, -4);
                double _x4 = Math.Abs(dX.GetElement(3, 0));
                double _x5 = Math.Abs(dX.GetElement(4, 0));
                double _x6 = Math.Abs(dX.GetElement(5, 0));
                if (_x4 < t && _x5 < t && _x6 < t)
                {
                    dxISOK = true;
                }
                //修正外方位元素
                XS = real_X.GetElement(0, 0);
                YS = real_X.GetElement(1, 0);
                ZS = real_X.GetElement(2, 0);
                φ =  real_X.GetElement(3, 0);
                ω =  real_X.GetElement(4, 0);
                K  =  real_X.GetElement(5, 0);
                h++;
            }
            while (dxISOK == false);
            //输出结果
            mtx += "\n                      -----------------------后方交会第" + h + "次迭代结果-------------------------";
            mtx += "\nXS = " + Math.Round(XS, 5) + "m\nYS = " + Math.Round(YS, 5) + "m\nZS = " + Math.Round(ZS, 5) + "m\nm   = " + Math.Round(m, 5);
            aidResult(_f, B, BT, BTB, BTf, dX,real_X); //输出
            return real_X;
        }

        /// <summary>
        /// 前方交会计算，返回计算完毕的控制点坐标:[待算同名像对对数*3]阶矩阵
        /// </summary>
        /// <param name="X1">矩阵类型：左片6个外方位元素</param>
        /// <param name="X2">矩阵类型：右片6个外方位元素</param>

        public Matrix forwardRsection(Matrix X1, Matrix X2)
        {
            Matrix XYZ = new Matrix(n, 3);         //待计算的控制点坐标
                                                   //前方交会一些像点空间辅助坐标
            double[] Up1 = new double[n];
            double[] Vp1 = new double[n];
            double[] Wp1 = new double[n];
            double[] Up2 = new double[n];
            double[] Vp2 = new double[n];
            double[] Wp2 = new double[n];
            double[] u1 = new double[n], u2 = new double[n],//φ ω K u γ[Xs Ys Zs φ ω K]
                     v1 = new double[n], v2 = new double[n],
                     w1 = new double[n], w2 = new double[n],
                     N1 = new double[n], N2 = new double[n],//前方交会的N1、N2
                     _X = new double[n], _Y = new double[n],
                     _Z = new double[n];
            //计算旋转矩阵等
            double Xs2 = X2.GetElement(0, 0); double Xs1 = X1.GetElement(0, 0);
            double Ys2 = X2.GetElement(1, 0); double Ys1 = X1.GetElement(1, 0);
            double Zs2 = X2.GetElement(2, 0); double Zs1 = X1.GetElement(2, 0);
            double bu = Xs2 - Xs1;
            double bv = Ys2 - Ys1;
            double bw = Zs2 - Zs1;
            mtx += "\n                  -----------------------以下是后方-前方交会计算结果--------------------------    ";
            Matrix R1 = Getuvw(1,X1, u1, v1, w1);
            Matrix R2 = Getuvw(2,X2, u2, v2, w2);
            for (int i = 0; i < n; i++)
            {
                N1[i] = (bu * w2[i] - bw * u2[i]) / (u1[i] * w2[i] - u2[i] * w1[i]);
                N2[i] = (bu * w1[i] - bw * u1[i]) / (u1[i] * w2[i] - u2[i] * w1[i]);
                Up1[i] = N1[i] * u1[i]; Up2[i] = N2[i] * u2[i];
                Vp1[i] = N1[i] * v1[i]; Vp2[i] = N2[i] * v2[i];
                Wp1[i] = N1[i] * w1[i]; Wp2[i] = N2[i] * w2[i];
                _X[i] = Xs1 + Up1[i];
                _Y[i] = (Ys1 + Vp1[i] + Ys2 + Vp2[i]) / 2;
                _Z[i] = Zs1 + Wp1[i];
                XYZ.SetElement(i, 0, _X[i]);
                XYZ.SetElement(i, 1, _Y[i]);
                XYZ.SetElement(i, 2, _Z[i]);
                mtx += "\n ————☆☆☆ 第 " + (i + 1) + " 对像点:   (" + x1[i + N] + " , " + y1[i + N] + ")  " + ", (" + x2[i + N] + " , " + y2[i + N] + ") 解算：☆☆☆————\n";
                mtx += "\n 旋转矩阵R1 :\n" + Matrix.Print(R1);
                mtx += " 旋转矩阵R2 :\n" + Matrix.Print(R2);
                mtx += "\n 摄影基线bu:  " + bu + "\n 摄影基线bv:  " + bv + "\n 摄影基线bw:  " + bw + "\n";
                mtx += "\n **左右像片像空间坐标分别为 :**\n 左片 ：(" + u1[i] + " , " + v1[i] + " , " + w1[i] + ")\n " + "右片：(" + u2[i] + " , " + u2[i] + " , " + w2[i] + ")\n";
                mtx += "\n 左片像点投影系数N1 : " + N1[i] + "\n 右片像点投影系数N2 : " + N2[i] + "\n";
                mtx += "\n————☆☆☆ 第 " + (i + 1) + " 对地面点:  （" + Math.Round(_X[i], 5) + " , " + Math.Round(_Y[i], 5) + " , " + Math.Round(_Z[i], 5) + ") ☆☆☆——-——\n";
            }
            return XYZ;

        }

        /// <summary>
        /// 获取左/右像片的像空间辅助坐标
        /// </summary>
        /// <param name="index"></param>
        /// <param name="X">后方交会元素矩阵</param>
        /// <returns></returns>
        public Matrix Getuvw(int index, Matrix X,  double[] u, double[] v, double[] w)
        {
            double[] x = x1;
            double[] y = y1;
            if (index == 2)
            {
                x = x2; y = y2;
            }
            //计算旋转矩阵等
            double φ = X.GetElement(3, 0);
            double ω = X.GetElement(4, 0);
            double K  = X.GetElement(5, 0);
            double[,] elementR1 = elementR(φ, K, ω);
            double[,] elementxyf = new double[3, 1];
            Matrix R1 = new Matrix(elementR1);
            for (int i = 0; i < n; i++)
            {
                elementxyf[0, 0] = x[i + N];
                elementxyf[1, 0] = y[i + N];
                elementxyf[2, 0] = -f;
                Matrix xyf = new Matrix(elementxyf);
                Matrix uvw = new Matrix();
                uvw = R1 * xyf;
                u[i] = uvw.GetElement(0, 0);
                v[i] = uvw.GetElement(1, 0);
                w[i] = uvw.GetElement(2, 0);
            }
            return R1;
        }
    }
}

