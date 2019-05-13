// Part Three


using System;
using System.Collections.Generic;
using System.Text;

namespace PRNGSimulation
{
    public class Random
    {
        //private List<double> collisionpoints1;
        //private List<double> collisionpoints2;
        public List<string> binarybits1 = new List<string>();
        public List<string> binarybits2 = new List<string>();
        public string bits;
        public string rev;
        public int i=0;
        public int j = 0;
        public int a = 0;
        public int b = 0;
        public string randomsequence;
        public string pseudorandom;

        public Random(List<double> collisionpoints1, List<double> collisionpoints2)
        {
            while (a < 32)
            {
                //Console.WriteLine("a: " + a);
                bits = ToBinary(collisionpoints1[a]);
                //Console.WriteLine("Binary number: " + bits);
                binarybits1.Add(bits); //Error 1
                a += 1;
            }
            while (b < 32)
            {
                bits = ToBinary(collisionpoints2[b]);
                rev = Reverse(bits);
                binarybits2.Add(rev); //Error 1
                b += 1;
            }

            //Console.WriteLine("Binary Bits 1");
            //foreach (object r in binarybits1)
            //{
                //Console.WriteLine(r);
            //}

            //Console.WriteLine("Binary Bits 2");
            //foreach (object s in binarybits2)
            //{
                //Console.WriteLine(s);
            //}

            //Console.WriteLine("binary bits 1: " + binarybits1);
            //Console.WriteLine("binary bits 2: " + binarybits2);

            string first;
            string second;
            while (i < 32)
            {
                first = binarybits1[i];
                //Console.WriteLine("First: " + first);
                second = binarybits2[i];
                //Console.WriteLine("Second: " + second);

                //Bitwise comparison
                j = 0;
                while (j < 62)
                {
                    //Console.WriteLine("First at " + i + "is " + first[j]);
                    //Console.WriteLine("Second at " + i + "is " + second[j]);
                    if (first[j] == second[j])
                    {
                        pseudorandom += "0";
                    }
                    else
                    {
                        pseudorandom += "1";
                    }
                    j += 1;
                }
                i += 1;
            }
            Console.WriteLine("Your random sequence is: " + pseudorandom);
        }

        private static string ToBinary(double binaryNum)
        {
            long m = BitConverter.DoubleToInt64Bits(binaryNum);
            string myStr = Convert.ToString(m, 2);
            //Console.WriteLine("Binary number: " + myStr);
            return myStr;
        }

        private static string Reverse(string binaryNum)
        {
            string reversed = "";
            int len;
            len = binaryNum.Length - 1;
            while(len >= 0)
            {
                reversed = reversed + binaryNum[len];
                len--;
            }
            return reversed;
        }
    }
}
