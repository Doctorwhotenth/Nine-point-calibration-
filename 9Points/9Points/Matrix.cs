using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9Points
{
    class Matrix
    {
        public static double[,] MatrixT(double[,] input)
        {
            //matrix transpose
            int x = input.GetLength(0);
            int y = input.GetLength(1);

            double[,] inputT = new double[y, x];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    inputT[j, i] = input[i, j];
                }
            }
            return inputT;
        
        }

        public static double[,] inv3(double[,] input)
        {
            //求逆矩阵
            //Inverse matrix
            double[,] output = new double[3, 3];

            output[0, 0] = input[2, 2] * input[1, 1] - input[2, 1] * input[1, 2];
            output[0, 1] = input[2, 1] * input[0, 2] - input[0, 1] * input[2, 2];
            output[0, 2] = input[0, 1] * input[1, 2] - input[0, 2] * input[1, 1];

            output[1, 0] = input[1, 2] * input[2, 0] - input[2, 2] * input[1, 0];
            output[1, 1] = input[2, 2] * input[0, 0] - input[0, 2] * input[2, 0];
            output[1, 2] = input[0, 2] * input[1, 0] - input[0, 0] * input[1, 2];

            output[2, 0] = input[1, 0] * input[2, 1] - input[2, 0] * input[1, 1];
            output[2, 1] = input[2, 0] * input[0, 1] - input[0, 0] * input[2, 1];
            output[2, 2] = input[0, 0] * input[1, 1] - input[1, 0] * input[0, 1];

            double Avalue = input[0, 0] * input[1, 1] * input[2, 2]
                + input[0, 1] * input[1, 2] * input[2, 0]
               + input[0, 2] * input[1, 0] * input[2, 1]
               - input[0, 2] * input[1, 1] * input[2, 0]
               - input[0, 1] * input[1, 0] * input[2, 2]
               - input[0, 0] * input[1, 2] * input[2, 1];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    output[i, j] = output[i, j] / Avalue;
                }
            }
            return output;

        }
        public static double[,] MatMul(double[,] input1, double[,] input2)
        {
            //matrix multiplication
            int y = input1.GetLength(0);
            int x = input2.GetLength(1);
            int len = input2.GetLength(0);
            double[,] ansxtx = new double[y, x];

            for (int i = 0; i < y; i++)           //矩阵xt的行数
            {
                for (int j = 0; j < x; j++)        //矩阵x的列数
                {
                    double a = 0;                  //单个元素的乘积
                    for (int k = 0; k < len; k++)
                    {
                        a = a + input1[i, k] * input2[k, j];   //矩阵xt的第i行k列与矩阵x的第k行j列的乘积
                    }
                    ansxtx[i, j] = a;            //将矩阵xt的i行与矩阵x的j列的乘积放入新矩阵的i行j列
                }
            }
            return ansxtx;

        }
    }
}
