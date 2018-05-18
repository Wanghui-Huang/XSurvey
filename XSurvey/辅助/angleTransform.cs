using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSurvey
{
    class angleTransform
    {
        public string AMS { get; set; }     //度分秒形式，如，118.1122 ---->118°11′22″
        public double Angel { get; set; }   //度形式，如，118.1122 --->118.1122°

        /// <summary>
        /// 将度分秒形式(string)表达的角度转换成度(double)表达形式
        /// </summary>
        /// <param name="AMS">度分秒</param>
        /// <returns></returns>
        public double AMStoA(string AMS)
        {
            double ds = Convert.ToDouble(AMS);
            int angle = (int)ds;
            double dMin = (ds - angle) * 100;
            int iMin = (int)dMin;
            double dsecond = (dMin - iMin) * 100;
            return angle + iMin / 60.0 + dsecond / 3600.0;
        }
        
        /// <summary>
        /// 度(double)转换成度分秒(string)
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        public string AtoAMS(double Angle)
        {
            int iAngle = (int)Angle;
            double dMin = (Angle - iAngle) * 60;
            int iMin = (int)dMin;
            double dsecond = (dMin - iMin) * 60;
            double AMS = Math.Round(iAngle + iMin / 100.0 + dsecond / 10000.0, 4);
            string s = Convert.ToString(AMS);
            return s;
        }

        /// <summary>
        /// 度转换成弧度(Radian)
        /// </summary>
        /// <param name="Angle">度形式</param>
        /// <returns></returns>
        public double AtoR(double Angle)
        {
            return Angle / 180 * Math.PI;
        }
    }
}
