using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Test
{
    class World
    {
        int Height;
        int Width;
        static Random RandInstance;
        public Boolean stopProgram = false;

        public World(int height, int width, Random random)
        {
            Height = height;
            Width = width;
            RandInstance = random;
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

    }
}
