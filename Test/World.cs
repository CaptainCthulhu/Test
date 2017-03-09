using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    class World
    {
        int frameTime = 16;
        int Height;
        int Width;
        static Random RandInstance;
        public Boolean stopProgram = false;
        Form1 Form;
        List<Car> cars = new List<Car>();

        public World(Form1 form, int height, int width, Random random)
        {
            Form = form;
            Height = height;
            Width = width;
            RandInstance = random;
        }

        public void AddCar(MouseEventArgs e)
        {
            lock (cars)
            {
                cars.Add(new Car(e, RandInstance));
            }
        }

        public void UpdateWorld()
        {
            Stopwatch frameTimer = new Stopwatch();
            while (!stopProgram)
            {
                frameTimer.Restart();
                if (this != null)
                {
                    try
                    {
                        using (Graphics gr = Form.CreateGraphics())
                        {
                            using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
                            {
                                using (BufferedGraphics bg = bgc.Allocate(gr, Form.DisplayRectangle))
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

    }
}
