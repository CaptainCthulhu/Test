using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        static Random random = new Random();
        int DisplayHeight = 800;
        int DislayWidth = 800;        
        World WorldInstance;

        public Form1()
        {
            InitializeComponent();
            base.Width = DislayWidth;
            base.Height = DisplayHeight;
            WorldInstance = new World(this, base.Width, base.Height, new Random());
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            WorldInstance.AddCar(e);            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Thread renderThread = new Thread(WorldInstance.UpdateWorld);
            renderThread.IsBackground = true;
            renderThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WorldInstance.StopWorld();
        }
    }
}
