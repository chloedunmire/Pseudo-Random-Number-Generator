//Part Two


using System;
using System.Collections.Generic;
using System.Drawing;

namespace PRNGSimulation
{
    public class Particle
    {
        private int eCollisions;
        private double angle;
        private double x;
        private double y;
        public const double xwall1 = -5; // Building the billiard 
        public const double xwall2 = 5;
        public const double ywall1 = -5;
        public const double ywall2 = 5;
        private int particleNum;
        int[] wallCnt;

        public Particle(int eCollisions, double angle, double x, double y, int particleNum)
        {
          
            this.eCollisions = eCollisions;
            this.angle = angle;
            this.x = x;
            this.y = y;
            this.particleNum = particleNum;
            wallCnt = new int[4] { 0, 0, 0, 0 };
        }

        public void teleport()
        {
            if(wallCnt[0] == 6)
            {
                this.x = -1 * this.x + 0.1;
                wallCnt[0] = 0;
                //Console.WriteLine("Wall 0 X - Teleportation!");
            }
            if (wallCnt[2] == 6)
            {
                this.x = -1 * this.x - 0.1;
                wallCnt[2] = 0;
                //Console.WriteLine("Wall 2 X - Teleportation!");
            }

            if (wallCnt[1] == 6)
            {
                this.y = -1 * this.y + 0.1;
                wallCnt[1] = 0;
                //Console.WriteLine("Wall 1 Y - Teleportation!");
            }
            if (wallCnt[3] == 6)
            {
                this.y = -1 * this.y - 0.1;
                wallCnt[3] = 0;
                //Console.WriteLine("Wall 3 Y - Teleportation!");
            }
        }

        public List<double> Move()
        {
            int colliderCnt = 0;
            double tangent; //angle of the tangent

            //Help from Dr. Riley
            double phi; //phi angle between x-axis and tangent line
            double theta; //theta angle between tangent line and particle's slope
            double gamma; //gamma angle between tangent line and particle's new slope


            //First let's calculate the inital slope given the provided initial angle and starting point of the particle
            //This is only for particle 1 -- meaning the particle on the right side of the billiard
            double slope;
            double x1 = this.x;
            double y1 = this.y;
            double x2= 0;
            double y2= 0;
            if(this.particleNum == 1)
            {
                x2 = 5;
                y2 = Math.Tan(this.angle) * (1.5);
                //Console.WriteLine("Particle 1 x2 y2");
                //slope = (y2 - y1) / (x2 - x1);
            }
            if(this.particleNum == 2)
            {
                x2 = -5;
                y2 = Math.Tan(this.angle) * (1.5);
                //Console.WriteLine("Particle 2 x2 y2");
                //slope = (y2 - y1) / (x2 - x1);
            }


            //Console.WriteLine("x1: " + x1 + " x2: " + x2);
            //Console.WriteLine("y1: " + y1 + " y2: " + y2);
            slope = (y2 - y1) / (x2 - x1) % 5;
            //Console.WriteLine("Starting slope = " + slope);

            List <double> collisionPts = new List <double>();

            double xinc = 0.1;
            if (slope < 0)
            {
                xinc = -xinc;
            }
            double yinc = slope * 0.1;
            //check this angle
            double cross = Math.Sqrt(2) / 2;
            double horizontal = Math.Cos(cross) * 2.5;
            double vertical = Math.Sin(cross) * 2.5;

            while (colliderCnt <= eCollisions + 32)
            {
                //Console.WriteLine("Current Slope: " + slope);
                //Console.WriteLine("Collider count = " + colliderCnt);
                // we are moving the x point 0.1 and the y point 0.1 along the slope
                this.x += xinc;
                this.y += yinc;
                //Console.WriteLine("x: " + this.x);
                //Console.WriteLine("y: " + this.y);
                //Console.WriteLine("Current Slope: " + slope);
                //Console.WriteLine("Current Angle: " + angle);

                teleport();


                if (4.8 < x && x < 5.5) //Wall0
                {
                    xinc = -1 * xinc;
                    slope = -slope + 0.1;
                    angle = -angle % (2 * Math.PI);
                    wallCnt[0] += 1;
                }
                if ( 4.8 < y && y < 5.5) //Wall1
                {
                    yinc = -1 * yinc;
                    slope = -slope + 0.1;
                    angle = -angle % (2 * Math.PI);
                    wallCnt[1] += 1;
                }
                if (-4.8 > x && x > -5.55) //Wall2
                {
                    xinc = -1 * xinc;
                    slope = -slope + 0.1;
                    angle = -angle % (2 * Math.PI);
                    wallCnt[2] += 1;
                }
                if (-4.8 > y && y > -5.55) //Wall3
                {
                    yinc = -1 * yinc;
                    slope = -slope+0.1;
                    angle = -angle % (2 * Math.PI);
                    wallCnt[3] += 1;
                }
                if (8.8 < Math.Pow(x, 2) + Math.Pow(y, 2) && Math.Pow(x, 2) + Math.Pow(y, 2) < 9.2)
                {
                    //Console.WriteLine("Hit the circle!");

                    //Help from Dr. Riley
                    //angle between x-axis and tangent line
                    tangent = -x / y; //tangent slope
                    //slope = known slope
                    phi = Math.Atan(-x / y);
                    theta = Math.Atan((slope - tangent) / (1 + slope * tangent));
                    slope = Math.Tan(phi + theta); //New slope
                    //Console.WriteLine("New Slope: " + slope);
                    gamma = Math.Atan((slope - tangent) / (1 + slope * tangent));
                    //Console.WriteLine("Old angle: " + theta + " New angle: " + gamma);

                    //Console.WriteLine("Current collider count: " + colliderCnt);


                    //Circle Collision
                    colliderCnt += 1;
                    double scale = (3 * Math.Sqrt(2)) / 2;
                    if (colliderCnt > eCollisions)
                    {
                        if (y > horizontal || y < -horizontal)
                        {
                            collisionPts.Add(x * scale);
                            //Console.WriteLine("Took the x coordinate");
                        }
                        if (x > vertical || x < -vertical)
                        {
                            collisionPts.Add(y * scale);
                            //Console.WriteLine("Took the y coordinate");
                        }
                    }

                    //reflect x or y increment
                    if (y > horizontal || y < -horizontal)
                    {
                        yinc = -yinc;
                    }
                    if (x > vertical || x < -vertical)
                    {
                        xinc = -xinc;
                    }

                    x += xinc;
                    y += yinc;
                }
            }
            return collisionPts;
        }

    }
}
