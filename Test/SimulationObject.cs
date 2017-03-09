using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Test
{
    class SimulationObject
    {
        internal XYValue location;
        internal XYValue vector;
        internal XYValue size;
        internal Color objectColour;
        internal Random rand;
        internal float rotation;


        public struct XYValue
        {
            internal float X;
            internal float Y;

            public XYValue(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        public SimulationObject(MouseEventArgs e, Random rand)
        {
            location = new XYValue(e.X, e.Y);
            this.rand = rand;
            vector.X = (float)rand.NextDouble() * rand.Next(-1, 2);
            vector.Y = (float)rand.NextDouble() * rand.Next(-1, 2);
            size.X = (float)rand.Next(20, 100);
            size.Y = (float)rand.Next(20, 100);
            rotation = rand.Next(0, 360);
        }

        public void ChangeVector(XYValue newVector)
        {
            vector.X += newVector.X;
            vector.Y += newVector.Y;
        }

        public RectangleF GetLocationRectangle()
        {
            return new RectangleF(
                location.X,
                location.Y,
                size.X,
                size.Y
                );
        }

        public XYValue GetVelocity()
        {
            return vector;
        }

        public XYValue UpdateLocation(SimulationObject simObj, List<SimulationObject> SimObjs)
        {
            XYValue newItem = simObj.location;            
            return newItem;
        }

        public void Update(Graphics g)
        {
            using (Pen pen = new Pen(objectColour))
            {
                location.X += vector.X;
                location.Y += vector.Y;
                RectangleF rect = new RectangleF(location.X, location.Y, size.X, size.Y);
                using (Matrix m = new Matrix())
                {
                    m.RotateAt(rotation, new PointF(rect.Left + (rect.Width / 2),
                                              rect.Top + (rect.Height / 2)));
                    g.Transform = m;
                    g.DrawRectangle(pen, new Rectangle(
                        (int)rect.X,
                        (int)rect.Y,
                        (int)rect.Size.Width,
                        (int)rect.Size.Height)
                    );
                    g.ResetTransform();
                }
            }
        }
    }
}
