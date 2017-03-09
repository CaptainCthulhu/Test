using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public static Random random = new Random();
        SimulationObject.XYValue screenSize = new SimulationObject.XYValue(800, 800);
        World world;

        public Form1()
        {
            InitializeComponent();
            world = new World(this, (int)screenSize.X, (int)screenSize.Y, new Random());
            Width = (int)screenSize.X;
            Height = (int)screenSize.Y;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            world.AddCar(e);
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Thread renderThread = new Thread(world.UpdateWorld);
            renderThread.IsBackground = true;
            renderThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            world.stopProgram = true;
        }
    }
}
