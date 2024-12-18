using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLINHKIENDT
{

    public class RoundedButton : Button
    {
        public int BorderRadius { get; set; } = 20; // Độ bo tròn

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            // Graphics để vẽ
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Định dạng bo tròn
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, BorderRadius, BorderRadius, 180, 90); // Góc trên trái
            path.AddArc(rect.X + rect.Width - BorderRadius, rect.Y, BorderRadius, BorderRadius, 270, 90); // Góc trên phải
            path.AddArc(rect.X + rect.Width - BorderRadius, rect.Y + rect.Height - BorderRadius, BorderRadius, BorderRadius, 0, 90); // Góc dưới phải
            path.AddArc(rect.X, rect.Y + rect.Height - BorderRadius, BorderRadius, BorderRadius, 90, 90); // Góc dưới trái
            path.CloseAllFigures();

            // Đặt vùng nút
            this.Region = new Region(path);

            // Vẽ nền và viền
            using (Brush brush = new SolidBrush(this.BackColor))
                g.FillPath(brush, path);
            using (Pen pen = new Pen(this.ForeColor, 1))
                g.DrawPath(pen, path);

            // Vẽ text
            TextRenderer.DrawText(g, this.Text, this.Font, rect, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
