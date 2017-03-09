using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        List<Car> cars = new List<Car>();
        public static Random random = new Random();
        SimulationObject.XYValue screenSize = new SimulationObject.XYValue(800, 800);
        int frameTime = 16;
        Boolean stopProgram = false;

        public Form1()
        {
            InitializeComponent();
            Width = (int)screenSize.X;
            Height = (int)screenSize.Y;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            lock (cars)
            {
                cars.Add(new Car(e, random));
            }
        }

        private void UpdateWorld()
        {
            Stopwatch frameTimer = new Stopwatch();
            while (!stopProgram)
            {
                frameTimer.Restart();
                if (this != null)
                {
                    try
                    {
                        using (Graphics gr = CreateGraphics())
                        {
                            using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
                            {
                                using (BufferedGraphics bg = bgc.Allocate(gr, this.DisplayRectangle))
                                {
                                    bg.Graphics.Clear(Color.White);
                                    bg.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                    using (Graphics g = bg.Graphics)
                                    {
                                        lock (cars)

                                            foreach (Car x in cars)
                                                x.Update(g);

                                        if (bg.Graphics != null)
                                            bg.Render();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        break;

                    }
                }
                frameTimer.Stop();
                if (frameTimer.ElapsedMilliseconds < frameTime)
                    Thread.Sleep(frameTime - (int)frameTimer.ElapsedMilliseconds);
                else Thread.Sleep(0);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Thread renderThread = new Thread(UpdateWorld);
            renderThread.IsBackground = true;
            renderThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopProgram = true;
        }
    }
}
