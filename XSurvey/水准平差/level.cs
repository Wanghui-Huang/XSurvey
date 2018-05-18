using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using hwh类库;

namespace XSurvey
{
    class level
    {
        private double fh_limt;                            //km
        private int n { get; set; }                        //测段数
        public static bool  isFourthClass   { get; set; }  //四等水准测量，如果不是那么是三等水准
        public static bool  isByDistance    { get; set; }  //按距离定权，否则按距离
        public static bool  isAttachedLevel { get; set; }  //附和水准，否则闭合
        public double fh      { get; set; }         //km
        public double[] D     { get; set; }          //distance
        public double[] h     { get; set; }          //highDifference 
        public double[] V     { get; set; }          //vertifyNum
        public double[] Vh    { get; set; }          //vertifyHighDifference 
        public double[] E     { get; set; }          //elevation
        public double[] SUM   { get; set; }          //各类求和值数组
        public level( double[] D, double[] h)
        {
            
            n = h.Length;
            this.D  = D;
            this.h  = h;
            this.V  = new double[n];
            this.Vh = new double[n];
            this.E  = new double[n + 1];
        }
        /// <summary>
        /// 水准平差调整
        /// </summary>
        public void leverAdjust()
        {
            //定义相关变量
            double sum_D = math.sumOfArr(D);
            double sum_h = math.sumOfArr(h);
            double [] P  = new double[n];
            fh           = sum_h + E[0] - E[n];

            //计算限差fh_limt
            if (isFourthClass)
            {
                fh_limt = 20 * Math.Sqrt(sum_D/1000);
            }
            else
            {
                fh_limt = 12 * Math.Sqrt(sum_D/1000);//三等
            }

            //计算权值，分配改正数，Vh，高程等
            for (int i = 0; i < n; i++)
            {
                P[i]  = D[i] / sum_D;
                //分配改正数，计算改正后Vh，高程
                V[i]  = -fh * P[i];
                Vh[i] = h[i] + V[i];
                if (i < n - 1)
                {
                    E[i + 1] = E[i] + Vh[i];
                }       
            }
            //返回求和值
            double sum_V  = math.sumOfArr(V) ;
            double sum_Vh = math.sumOfArr(Vh);
            double sum_E  = math.sumOfArr(E) ;
                   SUM    = new double[] { sum_D, sum_h, sum_V, sum_Vh, sum_E };
        }
        /// <summary>
        /// 辅助计算信息
        /// </summary>
        public string LeverAidResult()
        {
            string mtx = "";
            mtx += "\n\n                   · 闭合差：fh     = " + Math.Round(fh * 1000,1) + "mm";
            mtx += "\n\n                   · 限差  ：f_limt = " + Math.Round(fh_limt,1)   + "mm";
            if (fh * 1000 < fh_limt)
            {
                mtx += "                         闭合差符合要求!";
            }
            else
            {
                mtx += "                        闭合差不符合要求!";
            }
            return mtx;
        }
    }
}
