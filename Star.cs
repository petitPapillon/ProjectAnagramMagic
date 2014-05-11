using domasno.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domasno
{
    class Star
    {
        public Bitmap bmp;
        Point point;

        public Star(Point p)
        {
            bmp = new Bitmap(Resources.goldstar_gif,new Size(50,50));
            point = p;
        }

        public void Draw(Graphics g)
        {
            int x = point.X;
            int y = point.Y;
            for (int i = 0; i < 5; i++)
            {
                g.DrawImageUnscaled(bmp, x, y);
                x += 60;
            }
        }
    }
}
