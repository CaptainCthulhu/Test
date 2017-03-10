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
        Stopwatch frameTimer = new Stopwatch();

        public World(Form1 form, int height, int width, Random random)
        {
            Form = form;
            Height = height;
            Width = width;
            RandInstance = random;
        }

        public void UpdateWorld()
        {
            while (!stopProgram)
            {
                frameTimer.Restart();
                if (Form != null)
                {
                    try
                    {
                        using (BufferedGraphics bg = GetGraphics())
                        {
                            bg.Graphics.Clear(Color.White);
                            bg.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            using (Graphics g = bg.Graphics)
                            {
                                lock (cars)

                                    foreach (Car x in cars)
                                    {
                                        foreach (Car y in cars)
                                        {
                                            if (x != y)
                                                x.ChangeVelocity(Physics.Collision(x.GetPhysDetails(), y.GetPhysDetails()));
                                            else
                                                Console.WriteLine("Same Object");
                                        }
                                        x.Update(g);
                                    }

                                if (bg.Graphics != null)
                                    bg.Render();
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        break;
                    }
                }
                if (frameTimer.ElapsedMilliseconds < frameTime)
                    System.GC.Collect();
                frameTimer.Stop();
                if (frameTimer.ElapsedMilliseconds < frameTime)
                    Thread.Sleep(frameTime - (int)frameTimer.ElapsedMilliseconds);
                else
                    Thread.Sleep(0);
            }
        }

        public void AddCar(MouseEventArgs e)
        {
            lock (cars)
            {
                cars.Add(new Car(e, RandInstance));
            }
        }

        public void StopWorld()
        {
            stopProgram = true;
        }

        BufferedGraphics GetGraphics()
        {
                    return new BufferedGraphicsContext().Allocate(Form.CreateGraphics(), Form.DisplayRectangle);                            
        }
    }
}
