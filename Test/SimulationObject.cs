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
        internal Physics.VectorF LocationF;
        internal Physics.VectorF VelocityF;
        internal Physics.VectorF SizeF;
        internal Color ObjectColour;
        internal Random Rand;
        internal float Rotation;
        internal float Mass;

        public SimulationObject(MouseEventArgs e, Random rand)
        {
            LocationF = new Physics.VectorF(e.X, e.Y);
            this.Rand = rand;
            VelocityF.X = (float)rand.NextDouble() * rand.Next(-1, 2);
            VelocityF.Y = (float)rand.NextDouble() * rand.Next(-1, 2);
            SizeF.X = (float)rand.Next(20, 100);
            SizeF.Y = (float)rand.Next(20, 100);
            Rotation = rand.Next(0, 360);
            Mass = SizeF.Y * SizeF.X;
        }

        public void ChangeVelocity(Physics.VectorF newVector)
        {
            VelocityF.X += newVector.X;
            VelocityF.Y += newVector.Y;
        }

        public Physics.PhysicDetails GetPhysDetails()
        {
            return new Physics.PhysicDetails(
                GetLocationRectangle(),
                VelocityF,
                Mass);
            
        }

        public RectangleF GetLocationRectangle()
        {
            return new RectangleF(
                LocationF.X,
                LocationF.Y,
                SizeF.X,
                SizeF.Y
                );
        }

        public float GetMass()
        {
            return Mass;
        }

        public Physics.VectorF GetVelocity()
        {
            return VelocityF;
        }    

        public Physics.VectorF UpdateLocation(SimulationObject simObj, List<SimulationObject> SimObjs)
        {
            Physics.VectorF newItem = simObj.LocationF;
            return newItem;
        }

        public void Update(Graphics g)
        {
            using (Pen pen = new Pen(ObjectColour))
            {
                LocationF.X += VelocityF.X;
                LocationF.Y += VelocityF.Y;
                RectangleF rect = new RectangleF(LocationF.X, LocationF.Y, SizeF.X, SizeF.Y);
                using (Matrix m = new Matrix())
                {
                    m.RotateAt(Rotation, new PointF(rect.Left + (rect.Width / 2),
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
