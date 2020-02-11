using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 取色器
{
    class GetColor
    {
        [DllImport("user32.dll")]//取设备场景 
        private static extern IntPtr GetDC(IntPtr hwnd);//返回设备场景句柄 
        [DllImport("gdi32.dll")]//取指定点颜色 
        private static extern int GetPixel(IntPtr hdc, Point p);

        int r, g, b;

        public GetColor(Point p)
        {
            IntPtr hdc = GetDC(new IntPtr(0));//取到设备场景(0就是全屏的设备场景) 
            int c = GetPixel(hdc, p);//取指定点颜色 
            r = (c & 0xFF);//转换R 
            g = (c & 0xFF00) / 256;//转换G 
            b = (c & 0xFF0000) / 65536;//转换B 
        }

        public void ImageColor(PictureBox pb)
        {


            pb.BackColor = Color.FromArgb(r, g, b);

        }

        public void HexColor(TextBox tb)
        {

            string rr = r.ToString("X1");
            string gg = g.ToString("X1");
            string bb = b.ToString("X1");
            tb.Text = "#";
            tb.AppendText(rr + gg + bb);

        }

        public void ObtainData(TextBox tb1, TextBox tb2, TextBox tb3, PictureBox pb, KeyPressEventArgs e)
        {

            char ca = e.KeyChar;
            string st = Convert.ToString(ca);
            if (st == " ")
            {
                tb2.Text = tb1.Text;
                pb.BackColor = Color.FromArgb(r, g, b);
                tb3.Text = r + " , " + g + " , " + b;
            }
        }
    }
}
