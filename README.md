# Nine-point-calibration-
Implement nine-point calibration (plane coordinate transformation) only using C#.  use the least squares algorithm. Verify that the calculation results are consistent with those obtained from Halcon使用C#实现九点标定（平面坐标转换），用C#代码手搓最小二乘算法实现。
#  在不通过Halcon、OpenCV、MATLAB等算法库的情况下完成九点标定
****
###  ==结果==：计算结果与Halcon计算结果完全一致
****
####  一、九点标定算法原理
基本原理参考以下文章

[九点标定基本原理](https://blog.csdn.net/chuoji2469384644/article/details/106996137?ops_request_misc=%25257B%252522request%25255Fid%252522%25253A%252522161098182616780299043233%252522%25252C%252522scm%252522%25253A%25252220140713.130102334..%252522%25257D&request_id=161098182616780299043233&biz_id=0&utm_medium=distribute.pc_search_result.none-task-blog-2~all~top_click~default-1-106996137.pc_search_result_no_baidu_js&utm_term=%E4%B9%9D%E7%82%B9%E6%A0%87%E5%AE%9A)
*****
####  二、九点坐标情况下的最小二乘拟合
当输入有 n 个坐标时，我们的矩阵可以写成以下形式

$$
\left[
 \begin{matrix}
   x1 & y1& 1 \\
   x2 & y2& 1 \\
 :& :& :&  \\
  :& :& :& \\
  x9 & y9& 1 \\
  \end{matrix}
  \right] 
 \left[
 \begin{matrix}
   a1  \\
   b1 \\
  c1  \\
  \end{matrix}
  \right] = \left[
 \begin{matrix}
   X1 \\
   X2 \\
   :   \\
  :    \\
  X9 \\
  \end{matrix}
  \right]
$$

$$
\left[
 \begin{matrix}
   x1 & y1& 1 \\
   x2 & y2& 1 \\
 :& :& :&  \\
  :& :& :& \\
  x9 & y9& 1 \\
  \end{matrix}
  \right] 
 \left[
 \begin{matrix}
   a2  \\
   b2 \\
  c2  \\
  \end{matrix}
  \right] = \left[
 \begin{matrix}
   Y1 \\
   Y2 \\
   :   \\
  :    \\
  Y9 \\
  \end{matrix}
  \right]
$$

算法中间过程：[最小二乘算法详解](https://zhuanlan.zhihu.com/p/87582571)

**化简结果：**

$$
 \left[
 \begin{matrix}
   a  \\
   b \\
  c  \\
  \end{matrix}  
  \right]  = (X^{T}X)^{-1}X^{T}Y\\
  $$
  $$
X=a1\times x+ b1\times y+c1\\
Y=a2\times x+ b2\times y+c2\\
$$
***
####  三、C#算法的实现
**1.首先求出矩阵X的转置，参考往期博文**

[C#实现矩阵求转置](https://blog.csdn.net/qq_35044766/article/details/112397881)

**2.求出转置后的矩阵X’与原矩阵X的乘积**

已知原矩阵为**3×9**的矩阵，因此转置后可得到一个**9×3**的矩阵，两者进行相乘运算得到一个

**3×3**的矩阵

**矩阵相乘代码：**

```csharp
public static double[,] MatMul(double[,] input1, double[,] input2)
{
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
```

**3.求出矩阵X的转置与矩阵X的乘积==XTX==的逆**

[C#实现矩阵求逆](https://blog.csdn.net/qq_35044766/article/details/112795413?spm=1001.2014.3001.5502)

**4.求出逆后再次与矩阵X的转置进行相乘**

**5.最后将上述结果与Y矩阵相乘**
## 完整代码如下

```csharp
           int length = listView1.Items.Count;
            double[,] orgp = new double[length, 3];
            for (int i = 0; i < length; i++)
            {
                orgp[i, 0] = double.Parse(listView1.Items[i].SubItems[1].Text);  //添加像素坐标
                orgp[i, 1] = double.Parse(listView1.Items[i].SubItems[2].Text);
                orgp[i, 2] = 1;
            }
          
            double[,] rx = new double[length, 1];
            double[,] ry = new double[length, 1];
            for (int i = 0; i < length; i++)
            {
                rx[i, 0] = double.Parse(listView1.Items[i].SubItems[3].Text);  //添加机器坐标
                ry[i, 0] = double.Parse(listView1.Items[i].SubItems[4].Text);
            }
    
            double[,] MatT = Matrix.MatrixT(orgp);      //进
            double[,] xtx = Matrix.MatMul(MatT, orgp);  //行
            double[,] inv = Matrix.inv3(xtx);           //矩
            double[,] invxt = Matrix.MatMul(inv, MatT); //阵
            double[,] QX = Matrix.MatMul(invxt, rx);    //运
            double[,] QY = Matrix.MatMul(invxt, ry);    //算

            textBox5.Text = "Qx = " + QX[0,0].ToString("f5")
                     + "X+" + QX[1,0].ToString("f5")
                     + "Y+" + QX[2,0].ToString("f5") + "\r\n" +
                    "Qy = " + QY[0,0].ToString("f5")
                     + "X+" + QY[1,0].ToString("f5")
                     + "Y+" + QY[2,0].ToString("f5");

            prx.Add(QX[0, 0]);
            prx.Add(QX[1, 0]);
            prx.Add(QX[2, 0]);

            pry.Add(QY[0, 0]);
            pry.Add(QY[1, 0]);
            pry.Add(QY[2, 0]);
```

###   最终得到相应的两个平面坐标系的转换关系
**Qx、Qy为被求平面的坐标，x、y为已知平面的坐标**
$$
Qx=Qx[0] \times x+ Qx[1]\times y+Qx[2]\\
Qy=Qy[0] \times x+ Qy[1]\times y+Qy[2]
$$
