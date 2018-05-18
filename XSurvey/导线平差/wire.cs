using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using hwh类库;

namespace XSurvey
{
    class wire
    {
    
        //限差
        public static double fB_limt { get; set; }
        public static double K_limt  { get; set; }
        public static double f_limt  { get; set; }
        public static double fx_limt { get; set; }
        public static double fy_limt { get; set; }
        public static bool   isLeft  { get; set; }

        //计算的误差
        public  double fB { get; set; }
        public  double K  { get; set; }
        public  double f  { get; set; }
        public  double fx { get; set; }
        public  double fy { get; set; }

        //各类角度数组
        public static int      VA_num     { get; set; } //观测角个数
        public static double[] view_angle { get; set; }
        public static double[] distance   { get; set; }

        public wire(double[] view_angle, double[] distance, int VA_num)
        {
            wire.view_angle = view_angle;
            wire.distance   = distance;
            wire.VA_num     = VA_num;
        }

        /// <summary>
        /// 计算坐标方位角(coodinate Azimuth),同时返回闭合差
        /// </summary>
        /// <param name="Coodinate_Azi">包含初始坐标方位角的double数组</param>
        /// <param name="viewAngle">观测角信息</param>
        public double adj_Azimuth(double[] Coodinate_Azi, double[] viewAngle )
        {
            double sum_viewAngle = math.sumOfArr(viewAngle);
            if (wire.isLeft)                                //如果是观测左角转换成右角进行计算坐标方位角
            {
                for (int i = 0; i < VA_num; i++)
                {
                    viewAngle[i] = 360 - viewAngle[i];
                }
            }
            int n     = 0;                      //记录每次出现折角时，180的倍数
            int N     = 0;                      //记录倍数总和
            for (int i = 0; i < VA_num; i++)//计算出各个未修正观测角的坐标方位角
            {
                if (Math.Cos(Coodinate_Azi[i]) > 0)//直线方向向右时
                {
                    if (360 - viewAngle[i] > 0 && 180 - Coodinate_Azi[i] > 360 - viewAngle[i])
                    {                        
                        n = 3;
                    }
                    else
                    {
                        n = 1;
                    }                    
                }
                else//直线方向向左时
                {
                    if (viewAngle[i] > 0 && Coodinate_Azi[i] - 180 > viewAngle[i])
                    {
                        n = -1;
                    }
                    else
                    {
                        n = 1;
                    }
                }
                if (i + 1 < VA_num) //防止终坐标方位角被修改，根据文献【附合导线近似平差新方法】终坐标方位角不应该被修改
                {
                    Coodinate_Azi[i + 1] = Coodinate_Azi[i] - viewAngle[i] + 180 * n;
                }
                N += n;
            }
            //计算闭合差fB
            double fB;//单位为秒
            if (wire.isLeft)
            {
                fB = Math.Round((sum_viewAngle + Coodinate_Azi[0] - Coodinate_Azi[VA_num] - N * 180) * 3600);
            }
            else
            {
                fB = Math.Round((sum_viewAngle - Coodinate_Azi[0] + Coodinate_Azi[VA_num] - N * 180) * 3600);
            }
            //修正此前计算坐标方位角被修改的观测角值
            if (wire.isLeft)
            {
                for (int i = 0; i < VA_num; i++)
                {
                    viewAngle[i] = 360 - viewAngle[i];
                }
            }
            return fB;
        }

        /// <summary>
        /// 根据已知的两点坐标计算坐标方位角，返回度表达形式(double)
        /// </summary>
        public double Azimuth1(string x1, string y1, string x2, string y2)//
        {
            double dx1 = Convert.ToDouble(x1);
            double dy1 = Convert.ToDouble(y1);
            double dx2 = Convert.ToDouble(x2);
            double dy2 = Convert.ToDouble(y2);
            if (dy2 > dy1) { return Math.Acos((dx2 - dx1) / Math.Sqrt((dy2 - dy1) * (dy2 - dy1) + (dx2 - dx1) * (dx2 - dx1))) / Math.PI * 180; }
            else { return 360 - Math.Acos((dx2 - dx1) / Math.Sqrt((dy2 - dy1) * (dy2 - dy1) + (dx2 - dx1) * (dx2 - dx1))) / Math.PI * 180; }
        }

        /// <summary>
        /// 分配改正数
        /// </summary>
        public int[] VA_adj_num(double fB,int VA_num)
        {
            //开始分配改正数
            int avg_num = -(int)(fB / VA_num);  //平均要分配的改正数
            int rv_num = -(int)(fB % VA_num);//剩下要分配的改正数之和
            int N = Math.Abs(rv_num);
            double max = -1;                           //用该数不断和数组比较
            int[] index_VA = new int[N];           //从大到小记录前N位观测角的位置index
            int max_index = -1;                         //标记最大值位置
            double[] view_angle1 = (double [])view_angle.Clone();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < VA_num; j++)
                {
                    if (max < view_angle1[j])
                    {
                        max = view_angle1[j];           //当前循环最大值
                        max_index = j;
                    }
                }
                index_VA[i] = max_index;                //将前N位大小的观测角位置记录下来
                view_angle1[max_index] = -1;       
                max = -1;
            }
            int[] VA_adj_num = new int[VA_num];
            //首先改正数都取平均数
            for (int i = 0; i < VA_num; i++)
            {
                VA_adj_num[i] = avg_num;
            }
            //然后将剩余改正数分配
            for (int i = 0; i < N; i++)
            {

                VA_adj_num[index_VA[i]] += rv_num/N;   //+1 或许-1
            }
            return VA_adj_num;

        }

        /// <summary>
        /// 在一列textbox控件上打印出数据,采用度分秒形式打印出来
        /// </summary>
        public void printByAMS(int Startindex, TextBox[] tbArr,double[] dArr,bool isRemain = true)
        {
            angleTransform AT = new angleTransform();
            int i;
            for ( i = Startindex; i < wire.VA_num - 1; i++)
            {
                tbArr[i].Text = AT.AtoAMS(dArr[i]);
            }
            if (isRemain)        //打印出最后一个数据          
            {
                tbArr[15].Text = AT.AtoAMS(dArr[i]);
            }
            else
            {
                tbArr[i].Text  = AT.AtoAMS(dArr[i]);
            }
        }

        /// <summary>
        /// 在一列textbox控件上打印出数据,不采用度分秒形式，正常打印出来
        /// </summary>
        public void print<T>(int StartIndex,  TextBox[] tbArr, T[] dArr, bool isRemain = true)
        {
            int i;
            for ( i = StartIndex; i < dArr.Length - 1; i++)
            {
                tbArr[i].Text  = dArr[i].ToString();
            }
            if (isRemain)                 //打印出最后一个数据
            {
                tbArr[15].Text = dArr[i].ToString();
            }
            else
            {
                tbArr[i].Text  = dArr[i].ToString();
            }
        }

        /// <summary>
        /// 导线辅助计算结果
        /// </summary>
        /// <returns></returns>
        public string WireAidResult()
        {
            string aidResult;
            if (Math.Abs(fB) > wire.fB_limt)
            {
                aidResult = "\n        fβ = ∑β测 -α始 + α终 - n*180°=" + fB + "″" + "         fβ允 = ±" + wire.fB_limt + "″" + "                  |fβ| > |fβ允|! 角度限差不符合要求！\n";
            }
            else
            { 
                aidResult = "\n        fβ = ∑β测 -α始 + α终 - n*180°=" + fB + "″" + "         fβ允 = ±" + wire.fB_limt + "″" + "                  |fβ| < |fβ允|! 角度限差符合要求！\n";
            }
            aidResult += "\n        fx = " + Math.Round(fx, 4) * 1000 + "mm";
            aidResult += "            fy = " + Math.Round(fy, 4) * 1000 + "mm"; ;
            aidResult += "            f  = " + Math.Round(f, 4) * 1000 + "mm"; ;
            if    (fx * 1000 > wire.fx_limt) { aidResult += "             fx不符合限差要求！"; }
            else                             { aidResult += "             fx符合限差要求！";   }
            if    (fy * 1000 > wire.fy_limt) { aidResult += "   fy不符合限差要求！";           }
            else                             { aidResult += "   fy符合限差要求！";             }
            if    (f * 1000 > wire.f_limt)   { aidResult += "   f不符合限差要求！\n";          }
            else                             { aidResult += "   f符合限差要求！\n";            }
                                               aidResult += "\n        1/K = " + "1/" + K;
            if    (K > wire.K_limt)          { aidResult += "       K值符合限差要求！";        }
            else                             { aidResult += "       K值不符合限差要求！";      }
            return aidResult;
        }
    }
}
