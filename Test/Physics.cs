using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    class Physics
    {
        public struct VectorF
        {
            internal float X;
            internal float Y;

            public VectorF(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        public struct PhysicDetails
        {
            public RectangleF Rect;
            public VectorF Vector;
            public float Mass;

            public PhysicDetails(RectangleF rect, VectorF vector, float mass)
            {
                Vector = vector;
                Rect = rect;
                Mass = mass;
            }
        }

        public static VectorF Collision(PhysicDetails obj1, PhysicDetails obj2)
        {
            VectorF newVectorF = obj1.Vector;
            if (obj1.Rect.IntersectsWith(obj2.Rect))
            {
                newVectorF.X *= -1;
                newVectorF.Y *= -1;
            }
            return newVectorF;
        }
    }
}
