using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSurvey.界面
{
    class Matrix
    {
        int n;//行-从1开始计数
        int m;//列-从1开始计数
        public int N
        {
            get
            {
                return n;
            }
        }//只读
        public int M
        {
            get
            {
                return m;
            }
        }//只读
        //矩阵元素
        double[,] element;//不能赋值，字段初始值不能引用非静态值

        /*
         * 索引器-索引 二维数组element
         */
        public double this[int i, int j]
        {
            get { return element[i, j]; }
            set { element[i, j] = value; }
        }
        /*
         * 构造函数，形参为二维矩阵，设定矩阵基本信息
         */
        public Matrix(double[,] element)
        {
            n = element.GetLength(0);
            m = element.GetLength(1);
            this.element = (double[,])element;
        }
        /*
        * 基本构造函数-无形参
        */
        public Matrix()
        {
            n = 1;
            m = 1;
            element = new double[n, m];
        }

        /*
         * 构造函数-方便构造目标矩阵，形参为（ 行，列）
         * 调用- Matrix BT = B.转置(); 
         * 原理-声明一个返回类型为Matrix方法，在方法开头写入: Matrix trans = new Matrix(行，列);
         *      这样获取了B的行、列作为形参，以此初始化矩阵元素二维数组，这样可以将原B矩阵元素二维数组写入BT元素二维数组中。
         *      返回 BT给方法，这样就达到效果：不会改变矩阵对像B的实例成员，使之成为变为BT。
         */
        public Matrix(int Row, int Col)
        {
            n = Row;
            m = Col;
            element = new double[n, m];
        }

        /*
         * 运算符重载-矩阵加法
         */
        public static Matrix operator +(Matrix A, Matrix B)
        {
            //先进行判断 A,B 是否匹配
            if (A.n != B.n || A.m != B.m)
            {
                throw new Exception("矩阵加法-行列式不匹配！");
            }
            else
            {
                Matrix C = new Matrix(A.n, A.m);//本地变量要赋值才能使用
                for (int i = 0; i < C.n; i++)
                    for (int j = 0; j < C.m; j++)
                    {
                        // A,B 作为形参虽然也为本地变量，但无需实例化也可以使用，因为实参必须赋值
                        C[i, j] = A[i, j] + B[i, j];//索引器
                    }
                return C;
            }
        }
        /*
         * 运算符重载-矩阵减法
         */
        public static Matrix operator -(Matrix A, Matrix B)
        {
            //先进行判断 A,B 是否匹配
            if (A.n != B.n || A.m != B.m)
            {
                throw new Exception("矩阵减法-行列式不匹配！");
            }
            else
            {
                Matrix C = new Matrix(A.n, A.m);//本地变量要赋值才能使用
                for (int i = 0; i < C.n; i++)
                    for (int j = 0; j < C.m; j++)
                    {
                        // A,B 作为形参虽然也为本地变量，但无需实例化也可以使用，因为实参必须赋值
                        C[i, j] = A[i, j] - B[i, j];
                    }
                return C;
            }
        }
        /*
         * 运算符重载-矩阵乘法
         */
        public static Matrix operator *(Matrix A, Matrix B)
        {
            //先进行判断 A,B 是否匹配
            if (A.m != B.n)
            {
                throw new Exception("矩阵乘法-行列式不匹配！");
            }
            else
            {
                Matrix C = new Matrix(A.n, B.m);//本地变量要赋值才能使用
                for (int i = 0; i < C.n; i++)
                    for (int j = 0; j < C.m; j++)
                        for (int k = 0; k < A.m; k++)
                        {
                            C[i, j] += A[i, k] * B[k, j];
                        }

                return C;
            }
        }

        /*
         * 矩阵-下三角行列式求值(形参 - 矩阵类型)
         * 调用- Matrix.det(矩阵)
         * 思路- 多个循环镶嵌
         *       先做最内层循环将第一行元素第一个数化为0，且第一行所有数依次变化
         *       再做第二层循环将将第一行一直到第n行所有第一数化为0
         *       接着做最外层循环开始从第二行第二个数依次全化为0
         */
        public static double det(Matrix M)
        {
            //先进行判断 矩阵是否为方阵
            if (M.n != M.m)
            {
                throw new Exception("矩阵行列式-矩阵并非方阵！");
            }
            else
            {
                //这里是形参，但任需构造目标矩阵，因为Matrix是一个类类型它是一个引用类型
                Matrix medium = new Matrix(M.element);//这里是静态方法，所以调用实例成员需指定类名
                int n = medium.n;
                for (int j = 1; j < n; j++)
                    for (int i = j; i < n; i++)
                    {
                        double diagNum = medium[j - 1, j - 1];//获取对角线元素
                        double a = medium[i, j - 1] / diagNum;
                        for (int k = 0; k < n; k++)
                        {

                            medium[i, k] = medium[i, k] - a * medium[j - 1, k];
                        }
                    }
                double result = 1;
                for (int i = 0; i < n; i++)
                {
                    result *= medium[i, i];
                }
                return result;
            }
        }

        /*
         * 方法重载-行列式,求二维数组行列式的值
         * 调用- Matrix.det（二维数组）
         * 矩阵-下三角行列式求值(形参 - 二维数组)
         */
        public static double det(double[,] dArr2)
        {
            //先进行判断 矩阵是否为方阵
            int n = dArr2.GetLength(0);
            int m = dArr2.GetLength(1);
            if (n != m)
            {
                throw new Exception("矩阵行列式_重载1-二维数组维数不同！");
            }
            else
            {
                //这里是形参，但任需构造目标矩阵，因为M数组是它是一个引用类型
                double[,] m_dArr2 = (double[,])dArr2.Clone();
                for (int j = 1; j < n; j++)
                    for (int i = j; i < n; i++)
                    {
                        double diagNum = m_dArr2[j - 1, j - 1];//获取对角线元素
                        double a = m_dArr2[i, j - 1] / diagNum;
                        for (int k = 0; k < n; k++)
                        {
                            m_dArr2[i, k] = m_dArr2[i, k] - a * m_dArr2[j - 1, k];
                        }
                    }
                double result = 1;
                for (int i = 0; i < n; i++)
                {
                    result *= m_dArr2[i, i];
                }
                return result;
            }
        }

        /*
         * 方法-获取任意i行j列余子式
         * 返回-Matrix类型
         * 调用-Matrix m = new Matrix(); m.getM(行，列)
         * 思路- 对余任意矩阵/行列式 
         *      ① 先将从第一行开始，第j列起 将第j+1列赋值给第j列，依次将最后 第m列值赋值给 第m-1列
         *      ② 然后从第二行一直到第n行 重复上述第①步
         *      ③ 效果1：此时达到"消除"第"j"列，矩阵任然保持 n*m 列但前m-1列为余子式（除第i行）
         *      ④ 下面开始"消除" 第i行（本质是将第i行以后行往上移动一行）
         *      ⑤ 同上，从第i行开始依次将第 i+1行 赋值给第i行，直至，第n行赋值给第n-1行
         *      ⑥ 效果2：此时达到前 n-1行m-1列为余子式，第n行，第m列为原矩阵/行列式 最后一行/列
         *      ⑦ 如此之后，开始写入循环将获取此矩阵"变形"之后前n行与前m列（保持次序）
         */

        public Matrix getM(int Mi, int Mj)//minors-余子式
        {
            /*思考一个问题：做循环获取矩阵对象 m 的余子式，方法退出后m的实例成员-二维数组是否每一次都被改变？
            * 答案 是的。但是矩阵类中成员并未改变，所以每一次循环都需要重新获取对象 Matrix m = new Matrix();
            * 也可像下面一样构造目标矩阵（新对像，矩阵元素二维数组被初始化为 原对像一致,但二维数组值为null，待写入）
            */

            //获取目标矩阵
            int n = this.n;
            int m = this.m;
            Matrix medium = new Matrix(n, m);

            //开始进行第①②步
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m - 1; j++)
                {
                    if (j < Mj)
                    {
                        medium[i, j] = element[i, j];
                    }
                    else
                    {
                        medium[i, j] = element[i, j + 1];
                    }
                }
            //达到效果③开始进行第④⑤步
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < m; j++)
                {
                    if (i < Mi)
                    {
                        medium[i, j] = medium[i, j];//注意这里:medium[i, j] = element[i, j];→medium[i, j] = medium[i, j];下同
                    }
                    else
                    {
                        medium[i, j] = medium[i + 1, j];
                    }
                }
            //达到效果⑥开始开始进行第⑦步
            double[,] elementM = new double[n - 1, m - 1];
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < m - 1; j++)
                {
                    elementM[i, j] = medium[i, j];
                }
            //返回余子式M
            Matrix M = new Matrix(elementM);
            return M;
        }

        /* 矩阵- 转置
         * 调用- Matrix 转置M = new matrix(); 转置M = M.transpose();
         * 原理- ①构造目标矩阵将转置后行列 m*n 写入
         *       ②做循环，将转置后的矩阵元素二维数组写入目标矩阵
         *       ③返回目标矩阵
         */

        public Matrix transpose()
        {
            //构造目标矩阵
            Matrix medium = new Matrix(m, n);
            //写入元素
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    medium[j, i] = element[i, j];
                }
            return medium;
        }

        /* 矩阵- 求逆
         * 调用- Matrix 求逆M = new matrix(); 求逆M = M.inverse();
         * 原理- ①构造目标矩阵将原矩阵行，列写入
         *       ②调用方法-获取原矩阵行列式值
         *       ③调用方法-获取余子式，并做循环将所有 余子式/行列式值，写入目标矩阵二维数组元素
         *       ④调用方法-矩阵转置，得出最后的求逆结果
         *       ⑤返回目标矩阵
         */

        public Matrix inverse()
        {
            //先进行判断是否是方阵
            if (n != m)
            {
                throw new Exception("矩阵求逆-矩阵不是方阵！");
            }
            else
            {
                //构造目标矩阵
                Matrix medium = new Matrix(n, m);
                //
                double det = Matrix.det(element);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {

                        Matrix newM = new Matrix(element);//每次循环重新获取原矩阵新对象，如不如此（放在循环外），下面一行代码每循环一次会改变矩阵对象元素
                        medium[i, j] = Math.Pow(-1, i + j) * Matrix.det(newM.getM(i, j)) / det;
                    }
                return medium.transpose();
            }
        }

        /* 矩阵- 输出 ,指定保留位数
         * 调用- Matrix 输出M = new matrix(); Console.WriteLine(输出M.print())
         * 原理- ①返回string类型
         */

        public string Print(int n = 5, int m = 14)
        {
            string s = "";
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    s += (string.Format(Convert.ToString(Math.Round(element[i, j], n)))).PadRight(m);
                }
                s = s + "\n";
            }
            return s;
        }
    }
}
