using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using hwh类库;

namespace XSurvey
{
   public class photoGrammetry 
    {
        public string mtx { get; set; }
        //内方位元素
        public bool rendezvousChecked { get; set; }
        public int N { get; set; }
        public int n { get; set; }
        public double f { get; set; }
        public double m { get; set; }

        //已知数据
        public double[] x1 { get; set; }
        public double[] y1 { get; set; }
        public double[] x2 { get; set; }
        public double[] y2 { get; set; }
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public double[] Z { get; set; }
        public photoGrammetry(int ctr_potNum, int unknowPotNum, double f, double m, bool rendezvousChecked, double[] x1, double[] y1, double[] x2, double[] y2, double[] X, double[] Y, double[] Z)
        {
            this.N = ctr_potNum;
            this.n = unknowPotNum;
            this.f = f;
            this.m = m;
            this.rendezvousChecked = rendezvousChecked;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        /// <summary>
        /// 公共方法：将返回R旋转矩阵组成二维类型数组
        /// </summary>
        public double[,] elementR(double φ, double k, double ω)
        {
            double a1 = Math.Cos(φ) * Math.Cos(k) - Math.Sin(φ) * Math.Sin(ω) * Math.Sin(k);
            double a2 = -Math.Cos(φ) * Math.Sin(k) - Math.Sin(φ) * Math.Sin(ω) * Math.Cos(k);
            double a3 = -Math.Sin(φ) * Math.Cos(ω);
            double b1 = Math.Cos(ω) * Math.Sin(k);
            double b2 = Math.Cos(ω) * Math.Cos(k);
            double b3 = -Math.Sin(ω);
            double c1 = Math.Sin(φ) * Math.Cos(k) + Math.Cos(φ) * Math.Sin(ω) * Math.Sin(k);
            double c2 = -Math.Sin(φ) * Math.Sin(k) + Math.Cos(φ) * Math.Sin(ω) * Math.Cos(k);
            double c3 = Math.Cos(φ) * Math.Cos(ω);
            double[,] elementR = new double[,] {
                            { a1,a2,a3 },
                            { b1,b2,b3 },
                            { c1,c2,c3 }
                                                };
            return elementR;

        }
        /// <summary>
        ///  输出各种矩阵
        /// </summary>

        public void aidResult( Matrix _f, Matrix B, Matrix BT, Matrix BTB, Matrix BTf, Matrix dX, Matrix X)
        {
            mtx += "\n常数矩阵为：\n" + Matrix.Print(_f);
            mtx += "\n系数矩阵B为：\n" + Matrix.Print(B);
            mtx += "\n转置矩阵BT为：\n" + Matrix.Print(BT);
            mtx += "\n(Nbb)-1矩阵为：\n" + Matrix.Print(BTB, 8);
            mtx += "\nBTf矩阵为：\n" + Matrix.Print(BTf);
            mtx += "\n改正数矩阵[dXs dYs dZs dλ dΩ dφ dK]T为：\n" + Matrix.Print(dX, 8);
            mtx += "\n改正后定向元素矩阵[Xs Ys Zs λ Ω φ K]T 为：\n" + Matrix.Print(X, 8);
        }

        //

        //public void writeToDateGridView(DataGridView dgv)
    }
}
