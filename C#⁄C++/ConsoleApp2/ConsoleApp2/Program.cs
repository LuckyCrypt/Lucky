using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            int b = 4;
            for (int r = 5; r < 100; r++)
            {
                int n = r;
                if (n % 10 == 0) n++; //избегаем делимости на 10
                double h = (b - a) / (double)n;
                double[] x = new double[n + 1];
                double[] s = new double[n + 1];
                double[] x_ch = new double[n + 1];//чебышев
                double[] s_ch = new double[n + 1];
                for (int i = 0; i < n + 1; i++)
                {
                    x[i] = a + i * h;// для первой части задания 
                    // узлы Чебышева
                    x_ch[i] = (b - a) / 2 * Math.Cos(Math.PI * (2 * i + 1) / (2 * n + 2)) + (a + b) / 2;
                    s[i] = Summ(x[i]);
                    s_ch[i] = Summ(x_ch[i]);
                }
                int m = 10;
                double h2 = (b - a) / (double)m;
                double[] x2 = new double[m + 1];//не трогаем
                double[] s2 = new double[m + 1];//не трогаем
                double[] l = new double[m + 1];
                double[] l_ch = new double[m + 1];
                for (int i = 0; i < m + 1; i++)
                {
                    x2[i] = a + i * h2;
                    s2[i] = Summ(x2[i]);
                }
                double err = 0; // макс. погрешность
                double err_ch = 0;// погрешность для чебышева
                for (int i = 0; i < m + 1; i++)
                {
                    l[i] = L(x2[i], x, s, n);
                    if (Math.Abs(s2[i] - l[i]) > err) err = Math.Abs(s2[i] - l[i]);
                    l_ch[i] = L(x2[i], x_ch, s_ch, n);
                    if (Math.Abs(s2[i] - l_ch[i]) > err_ch) err_ch = Math.Abs(s2[i] - l_ch[i]);
                }
                using (StreamWriter sr = new StreamWriter("C:\\Users\\Lucky\\source\\repos\\ConsoleApp2\\ConsoleApp2\\rezult.txt", true))
                {
                    // Для табулирования
                    for (int i = 0; i < m + 1; i++)
                   {
                       sr.Write("{0:0.######} \t", x2[i]);
                       sr.Write("{0:0.######} \t", s2[i]);
                       sr.Write("{0:0.######} \t", l[i]);
                       sr.WriteLine("{0:0.######} \t", Math.Abs(s2[i] - l[i]));
                   }
                    // Для погрешностей
                    sr.Write(n + "\t");
                    sr.WriteLine("{0:0.#########}", err);
                }
                //чеба
                using (StreamWriter sr = new StreamWriter("C:\\Users\\Lucky\\source\\repos\\ConsoleApp2\\ConsoleApp2\\rezult2.txt", true))
                {
                    // Для табулирования
/*                    for (int i = 0; i < m + 1; i++)
                    {
                      sr.Write("{0:0.######} \t", x2[i]);
                      sr.Write("{0:0.######} \t", s2[i]);
                      sr.Write("{0:0.######} \t", l_ch[i]);
                      sr.WriteLine("{0:0.######} \t", Math.Abs(s2[i] - l_ch[i]));
                    }*/
                    // Для погрешностей
                    sr.Write(n + "\t");
                    sr.WriteLine("{0:0.#########}", err_ch);
                }
            }
        }
        public static double Summ(double x)
        {
            const double eps = 0.000001;
            int i = 0;
            double s = 0;
            double ai = x;
            double qn = 0;
            while (Math.Abs(ai) >= eps)
            {
                s = s + ai;
                qn = (-x * x * (2 * i + 1)) / ((2 * i + 3) * (2 * i + 3) * (2 * i + 2));
                ai = ai * qn;
                i++;
            }
            return s;
        }
        public static double L(double x_main, double[] x, double[] sum, int n)
        {
            double l = 0;
            for (int i = 0; i < n + 1; i++)
            {
                double p = 1;
                for (int j = 0; j < n + 1; j++)
                {
                    if (j != i)
                    {
                        p = p * ((x_main - x[j]) / (x[i] - x[j]));
                    }
                }
                l += sum[i] * p;
            }
            return l;
        }
    }
}
