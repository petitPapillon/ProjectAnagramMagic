using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domasno
{
    public class Pie
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Radius { get; set; }
        public Brush brush;
        public int startAngle { get;     set; }
        public int endAngle { get; set; }
        public Pen pen;
        bool flag;

        public Pie(float x, float y)
        {
            this.X = x;
            this.Y = y;
            startAngle = 270;
            endAngle = 0;
            Radius = 50;
            brush = new SolidBrush(Color.BurlyWood);
            pen = new Pen(Color.Peru, 3);
            flag = true;
        }
        public void Move()
        {
            if(endAngle < 360)
            endAngle += 12;
            if (endAngle > 290)
            {
                brush = new SolidBrush(Color.Purple);
                if (flag)
                {
                    Radius += 5;
                }
                else
                {
                    Radius -= 5;
                }
                flag = !flag;
            }
        }
        public void Draw(Graphics g)
        {
            g.FillPie(brush, X - Radius, Y - Radius, 2 * Radius, 2 * Radius, startAngle, endAngle);
            g.DrawEllipse(pen, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
        }
    }
}
