using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hwh类库;

namespace XSurvey
{
    class orientation : photoGrammetry
    {

        //同名像点的像空间辅助坐标(U,V,W)
        public double[] U { get; set; }
        public double[] V { get; set; }
        public double[] W { get; set; }

        //相对定向相关变量
        public orientation(int ctr_potNum, int unknowPotNum, double f, double m, bool rendezvousChecked, double[] x1, double[] y1, double[] x2, double[] y2, double[] X, double[] Y, double[] Z)
               :base(ctr_potNum,  unknowPotNum,  f,  m,rendezvousChecked,  x1,  y1, x2, y2,  X, Y,  Z)
        {
            U = new double[n + N];
            V = new double[n + N];
            W = new double[n + N];
        }

        //相对定向
        public Matrix relative_orientation()
        {
            //定义初始值
            double φ2 = 0;
            double k2  = 0;
            double ω2 = 0;
            double  u  = 0;
            double γ  = 0;
            //基线
            double bu  = x1[0] - x2[0];
            double bv  = 0; 
            double bw  = 0; 
            //定义像点a和像空间辅助坐标A二维数组等
            double[,] elementA1  = new double[3, 1]; Matrix a1  = new Matrix();
            double[,] elementa1  = new double[3, 1]; Matrix a2  = new Matrix();
            double[,] elementA2  = new double[3, 1]; Matrix R2  = new Matrix();
            double[,] elementa2  = new double[3, 1]; Matrix A1  = new Matrix();
            double[] a  = new double[n + N];         Matrix A2  = new Matrix();
            double[] b  = new double[n + N];         Matrix _f  = new Matrix();
            double[] c  = new double[n + N];         Matrix B   = new Matrix();
            double[] d  = new double[n + N];         Matrix BT  = new Matrix();
            double[] e  = new double[n + N];         Matrix BTB = new Matrix();
            double[] Q  = new double[n + N];         Matrix BTf = new Matrix();
            double[] N1 = new double[n + N];         Matrix dX  = new Matrix();
            double[] N2 = new double[n + N];         Matrix X   = new Matrix(5, 1);
            double[] u1 = new double[n + N];
            double[] v1 = new double[n + N];
            double[] w1 = new double[n + N];
            double[] u2 = new double[n + N];
            double[] v2 = new double[n + N];
            double[] w2 = new double[n + N];
            bool vxIsOk = false;          //暂时先取消迭代运算！！
            int k = 0;//记录计算次数
            do
            {            
                //右片旋转矩阵R2
                double[,] elementR2 = elementR(φ2, k2, ω2);
                bv = bu * u;
                bw = bu * γ;
                //求系数矩阵B  
                for (int i = 0; i < n + N; i++)
                {
                    //给像点坐标写入元素
                    elementa1[0, 0] = x1[i]; elementa2[0, 0] = x2[i];
                    elementa1[1, 0] = y1[i]; elementa2[1, 0] = y2[i];
                    elementa1[2, 0] = -f       ; elementa2[2, 0] = -f       ;
                    a1    = new Matrix(elementa1);
                    a2    = new Matrix(elementa2);
                    R2    = new Matrix(elementR2);
                    A2    = R2 * a2;
                    u1[i] = x1[i]; 
                    v1[i] = y1[i]; 
                    w1[i] = -f   ; 
                    u2[i] = A2.GetElement(0, 0);
                    v2[i] = A2.GetElement(1, 0);
                    w2[i] = A2.GetElement(2, 0);
                    N1[i] = (bu * w2[i] - bw * u2[i]) / (u1[i] * w2[i] - u2[i] * w1[i]);
                    N2[i] = (bu * w1[i] - bw * u1[i]) / (u1[i] * w2[i] - u2[i] * w1[i]);
                    //待算像点的像空间辅助坐标(U,V,W)
                    U[i] = N1[i] * u1[i];
                    V[i] = N1[i] * v1[i];
                    W[i] = N1[i] * w1[i];
                    //系数矩阵B的列元素
                    Q[i]  = N1[i] * v1[i] - N2[i] * v2[i] - bv;
                    a[i]  = -u2[i] * v2[i] / w2[i] * N2[i];
                    b[i]  = -(w2[i] + v2[i] * v2[i] / w2[i]) * N2[i];
                    c[i]  = u2[i] * N2[i];
                    d[i]  = bu;
                    e[i]  = -v2[i] / w2[i] * bu;
                }
                double[,] elementB = new double[n + N, 5];
                for (int i = 0; i < n + N; i++)
                    for (int j = 0; j < 5; j++)
                    {
                        if (j == 0) { elementB[i, j] = a[i]; }
                        if (j == 1) { elementB[i, j] = b[i]; }
                        if (j == 2) { elementB[i, j] = c[i]; }
                        if (j == 3) { elementB[i, j] = d[i]; }
                        if (j == 4) { elementB[i, j] = e[i]; }
                    }
                double[,] elementf = new double[n + N, 1];
                for (int i = 0; i < n + N; i++)
                {
                    elementf[i, 0] = Q[i];
                }
                _f  = new Matrix(elementf);
                B   = new Matrix(elementB);
                BT  = B.Transpose();
                BTB = BT * B;
                BTf = BT * _f;
                BTB.InvertGaussJordan();//求逆
                dX  = BTB * BTf;
                X  += dX ;
                //开始判断_x是否满足<0.3*10^-4
                double t    = 0.3 * Math.Pow(10, -4);
                double dφ2 = Math.Abs(dX.GetElement(0, 0));
                double dω2 = Math.Abs(dX.GetElement(1, 0));
                double dk2  = Math.Abs(dX.GetElement(2, 0));
                double du   = Math.Abs(dX.GetElement(3, 0));
                double dγ  = Math.Abs(dX.GetElement(4, 0));
                //不能进行迭代！？？？
                if (dφ2 < t && dω2 < t && dk2 < t && du < t && dγ < t)
                {
                    vxIsOk = true;
                }
                φ2 = X.GetElement(0, 0);
                ω2 = X.GetElement(1, 0);
                k2  = X.GetElement(2, 0);
                u   = X.GetElement(3, 0);
                γ  = X.GetElement(4, 0);
                k++;
            }
            while (vxIsOk == false);
            mtx += "\n          ---------------------------------相对定向第" +k + "次迭代结果------------------------------";
            aidResult( _f, B, BT, BTB, BTf, dX, X);
            return X;
        }

       
        /// <summary>
        /// 根据已知控制点，计算七个绝对定向元素
        /// </summary>
        public Matrix obsolute_orientation(double[] Uc,double[] Vc,double[] Wc)
        {
            double[,] ab_emB = new double[N * 3, 7];
            double[,] ab_emf = new double[N * 3, 1];
            double[,] elementλUcVcWc = new double[3, 1];
            double λ  = 1;
            double φ  = 0;
            double Ω  = 0;
            double K   = 0;
            double Xs  = math.avgDArr(this.X,N);
            double Ys  = math.avgDArr(this.Y,N);
            double Zs  = m * f * Math.Pow(10, -3);
            Matrix λUcVcWc   = new Matrix();
            Matrix λRoUcVcWc = new Matrix();
            Matrix X   = new Matrix();
            Matrix _f  = new Matrix();
            Matrix B   = new Matrix();
            Matrix BT  = new Matrix();
            Matrix BTB = new Matrix();
            Matrix BTf = new Matrix();
            Matrix dX  = new Matrix();
            //写入矩阵元素B
            int g = 0;
            for (int i = 0; i < N * 3; i++)
                for (int j = 0; j < 7; j++)
                {
                    g = i / 3;
                    if ((i + 1) % 3 == 1)
                    {
                        ab_emB[i, 0] = 1; ab_emB[i, 1] = 0; ab_emB[i, 2] = 0; ab_emB[i, 3] = Uc[g]; ab_emB[i, 4] = -Wc[g]; ab_emB[i, 5] = 0; ab_emB[i, 6] = -Vc[g];
                    }
                    if ((i + 1) % 3 == 2)
                    {
                        ab_emB[i, 0] = 0; ab_emB[i, 1] = 1; ab_emB[i, 2] = 0; ab_emB[i, 3] = Vc[g]; ab_emB[i, 4] = 0; ab_emB[i, 5] = -Wc[g]; ab_emB[i, 6] = Uc[g];
                    }
                    if ((i + 1) % 3 == 0)
                    {
                        ab_emB[i, 0] = 0; ab_emB[i, 1] = 0; ab_emB[i, 2] = 1; ab_emB[i, 3] = Wc[g]; ab_emB[i, 4] = Uc[g]; ab_emB[i, 5] = Vc[g]; ab_emB[i, 6] = 0;
                    }
                }
            g = 0;
            bool isOk = false;  //先取消绝对定向的迭代！！！
            int l = 0;
            //循环判断
            do
            {
                double[]  λRoUc = new double[N],
                          λRoVc = new double[N],
                          λRoWc = new double[N];
                double[,] elementR0 = elementR(φ, K, Ω);
                Matrix    Ro        = new Matrix(elementR0);
                for (int i = 0; i < N; i++)
                {
                    elementλUcVcWc[0, 0] = λ * Uc[i];
                    elementλUcVcWc[1, 0] = λ * Vc[i];
                    elementλUcVcWc[2, 0] = λ * Wc[i];
                    λUcVcWc   = new Matrix(elementλUcVcWc);
                    λRoUcVcWc = Ro * λUcVcWc;
                    λRoUc[i] = λRoUcVcWc.GetElement(0, 0);
                    λRoVc[i] = λRoUcVcWc.GetElement(1, 0);
                    λRoWc[i] = λRoUcVcWc.GetElement(2, 0);
                }
                for (int i = 0; i < N*3; i++)
                {
                    g = i / 3;
                    if ((i + 1) % 3 == 1)
                    {
                        ab_emf[i, 0] = this.X[g] - λRoUc[g] - Xs;
                    }
                    if ((i + 1) % 3 == 2)
                    {
                        ab_emf[i, 0] = this.Y[g] - λRoVc[g] - Ys;
                    }
                    if ((i + 1) % 3 == 0)
                    {
                        ab_emf[i, 0] = this.Z[g] - λRoWc[g] - Zs;
                    }
                }
                double[,] elementX = new double[,] { { Xs }, { Ys }, { Zs }, { λ }, { φ }, { Ω }, { K } };
                //矩阵最小二乘法求改正数
                X   = new Matrix(elementX);
                _f  = new Matrix(ab_emf);
                B   = new Matrix(ab_emB);
                BT  = B.Transpose();
                BTB = BT * B;
                BTf = BT * _f;
                dX  = new Matrix();
                BTB.InvertGaussJordan();//求逆
                dX  = BTB * BTf;
                X   = dX + X;
                //开始判断是否符合要求
                double t = 0.3 * Math.Pow(10, -4);
                double dXφ = Math.Abs(dX.GetElement(4, 0));
                double dXΩ = Math.Abs(dX.GetElement(5, 0));
                double dXK  = Math.Abs(dX.GetElement(6, 0));
                if (dXφ < t && dXΩ < t && dXK < t)
                {
                    isOk = true;
                }
                Xs = X.GetElement(0, 0);
                Ys = X.GetElement(1, 0);
                Zs = X.GetElement(2, 0);
                λ = X.GetElement(3, 0); ;
                φ = X.GetElement(4, 0);
                Ω = X.GetElement(5, 0);
                K  = X.GetElement(6, 0);
                l++;
            }
            while (isOk == false);
            mtx += "\n          ---------------------------------绝对定向第" + l + "次迭代结果------------------------------";
            aidResult( _f, B, BT, BTB, BTf, dX, X); //输出
            return X;
        }

        /// <summary>
        /// 根据绝对定向元素计算模型点摄影测量坐标
        /// </summary>
        public void getPhotogrammetricCoordinate(Matrix X,double[] Up,double[] Vp,double[] Wp)
        {
            double Xs = X.GetElement(0, 0);
            double Ys = X.GetElement(1, 0);
            double Zs = X.GetElement(2, 0);
            double λ = X.GetElement(3, 0); ;
            double φ = X.GetElement(4, 0);
            double Ω = X.GetElement(5, 0);
            double  K = X.GetElement(6, 0);
            //获取最后的绝对定向元素算出像点(X,Y,Z)
            Matrix XYZ = new Matrix();
            double[,] elementλUVW = new double[3, 1];
            double[,] elementR1 = elementR(φ, K, Ω);
            double[,] elemenXsYsZs = new double[3, 1] { { Xs }, { Ys }, { Zs } };
            Matrix XsYsZs = new Matrix(elemenXsYsZs);
            Matrix R = new Matrix(elementR1);
            for (int i = 0; i < 5; i++)
            {
                elementλUVW[0, 0] = λ * Up[i];
                elementλUVW[1, 0] = λ * Vp[i];
                elementλUVW[2, 0] = λ * Wp[i];
                Matrix λUVW = new Matrix(elementλUVW);
                XYZ = R * λUVW + XsYsZs;
                mtx += "\n   第" + (i + 1) + "对摄影测量坐标为：（" + XYZ.GetElement(0, 0) + ", " + XYZ.GetElement(1, 0) + ", " + XYZ.GetElement(2, 0) + " )";
            }
        }

        /// <summary>
        /// 分别计算控制点和待算点在S1-U1V1W1中的坐标(U,V,W),且大约放大成实地
        /// </summary>
        public void getUVW(bool iSctrUVW,double[] U,double[] V,double[] W)
        {
            int len   = 0;
            int count = N;
            if (!iSctrUVW)
            {
                len  = N;
                count= n ;
            }
            for (int i = 0; i < count; i++)
            {
                U[i] = this.U[i + len] * m / 1000;
                V[i] = this.V[i + len] * m / 1000;
                W[i] = this.W[i + len] * m / 1000;
            }
        }
         }

    }
