//Part 1

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace PRNGSimulation
{
    class Program
    {

        public static double Angle1;
        public static double Angle2;
        public static int TotalECollisons;
        public static double initialAx = 3.5;
        public static double initialAy = 0;
        public static double initialBx = -3.5;
        public static double initialBy = 0;

        static void Main(string[] args)
        {

            List<string> words = new List<string>();

            words.Add("galvanized");
            words.Add("garnishing");
            words.Add("gasholders");
            words.Add("generation");
            words.Add("geographic");
            words.Add("geographer");
            words.Add("ghostwrite");
            words.Add("gingerroot");
            words.Add("girlfriend");
            words.Add("glamorizes");
            words.Add("glasshouse");
            words.Add("glimmering");
            words.Add("glistening");
            words.Add("glorifying");
            words.Add("goalkeeper");
            words.Add("gossipping");
            words.Add("government");
            words.Add("graduating");
            words.Add("graspingly");
            words.Add("greasiness");
            words.Add("gymnastics");
            words.Add("grievously");
            words.Add("grogginess");
            words.Add("groundless");
            words.Add("groundhogs");
            words.Add("grumpiness");
            words.Add("guardrails");
            words.Add("gunpowders");
            words.Add("gymnasium");

            foreach (string fruit in words)
            {
                string userPass;
                string binaryPass;
                //Console.Write("Enter your Password: ");
                //userPass = Console.ReadLine();
                userPass = fruit;
                Console.WriteLine("User Password: " + userPass);
                binaryPass = ToBinary(userPass); //returns the sb.ToString()
                //Console.WriteLine("Binary Password Check");
                InitialAngles(binaryPass);
                //Console.WriteLine("Initial Angles Check");
                ECollisions(binaryPass);
                //Console.WriteLine("Collision Requirement Check");
                List<double> collisionpoints1;
                List<double> collisionpoints2;

                Particle one = new Particle(TotalECollisons, Angle1, initialAx, initialAy, 1);
                collisionpoints1 = one.Move();
                Particle two = new Particle(TotalECollisons, Angle2, initialBx, initialBy, 2);
                collisionpoints2 = two.Move();
                Random sequence = new Random(collisionpoints1, collisionpoints2);
            }
        }

        private static string ToBinary(string binaryNum)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char L in binaryNum)
            {
                sb.Append(System.Convert.ToString(L, 2).PadLeft(8, '0'));
            }
            //Console.WriteLine("Your password in binary is: " + sb);
            return sb.ToString();
        }

        public static void ECollisions(string binaryStr)
        {
            string sub;
            int counter;
            int value;
            int number;
            int total = 0;
            float power;
            sub = binaryStr.Substring(0, 8);
            int[] eArray = new int[8];
            for (counter = 0; counter < 8; counter++)
            {
                power = (float)Math.Pow(2, counter);
                value = (int)(Convert.ToInt32(sub.Substring(counter, 1)));
                number = (int)(value * power);
                total = total + number;
            }
            TotalECollisons = total;
            //Console.WriteLine("Total # of E Collisions: " + TotalECollisons);
        }

        public static void InitialAngles(string binaryStr)
        {
            int length = binaryStr.Length;
            int n = length / 8;

            string[] bytes;
            bytes = new string[n];

            int cnt = 0;
            int start = 0;
            int len = 8;
            int even = 0;
            int odd = 0;
            while (cnt < n)
            {
                bytes[cnt] = binaryStr.Substring(start, len);
                start = start + len;
                if (cnt % 2 == 0)
                {
                    even = even + 1;
                }
                else
                {
                    odd = odd + 1;
                }
                cnt++;
            }
            int part1Bits = even * 8;
            int part2Bits = odd * 8;

            string[] particle1 = new string[even];
            string[] particle2 = new string[odd];
            int p1 = 0, p2 = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i % 2 == 0)
                {
                    particle1[p1] = bytes[i];
                    p1++;
                }
                else
                {
                    particle2[p2] = bytes[i];
                    p2++;
                }
            }



            //particle1:
            float total1 = 0;
            string pullEvenOut;
            string part1integers;
            int strToint1;
            int incpow1 = 0;
            int times1;
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < even; i++)
                {
                    pullEvenOut = particle1[i];
                    part1integers = pullEvenOut.Substring(j, 1);
                    strToint1 = Convert.ToInt32(part1integers);
                    times1 = (int)Math.Pow(2, incpow1);
                    total1 = total1 + strToint1 * times1;
                    incpow1 = incpow1 + 1;
                }
            }

            //particle2:
            float total2 = 0;
            string pullOddOut;
            string part2integers;
            int strToint2;
            int incpow2 = 0;
            int times2;
            for (int k = 0; k < 8; k++)
            {
                for (int l = 0; l < odd; l++)
                {
                    pullOddOut = particle2[l];
                    part2integers = pullOddOut.Substring(k, 1);
                    strToint2 = Convert.ToInt32(part2integers);
                    times2 = (int)Math.Pow(2, incpow2);
                    total2 = total2 + strToint2 * times2;
                    incpow2 = incpow2 + 1;
                }
            }

            //Console.WriteLine("Total1: " + total1);
            //Console.WriteLine("Total2: " + total2);

            float pi;
            pi = ((float)(2 * System.Math.PI));

            float part1Num;
            float divisor1 = (float)Math.Pow(2, part1Bits);
            part1Num = total1 / divisor1;
            Angle1 = part1Num * pi;

            float part2Num;
            float divisor2 = (float)Math.Pow(2, part2Bits);
            part2Num = total2 / divisor2;
            Angle2 = -part2Num * pi;


            //Console.WriteLine("The first initial angle is: " + Angle1 + " The second initial angle is: " + Angle2);
        }
    }
}
