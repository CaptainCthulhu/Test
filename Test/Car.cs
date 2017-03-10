using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    class Car : SimulationObject
    {             

        public Car(MouseEventArgs e, Random rand): base(e, rand)
        {
            this.ObjectColour = Color.FromArgb(
                rand.Next(255),
                rand.Next(0,255),
                rand.Next(0,255),
                rand.Next(0,255)
                );
        }
        

    }
}
