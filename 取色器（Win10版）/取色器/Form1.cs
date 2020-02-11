using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 取色器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("shcore.dll")]
        static extern int SetProcessDpiAwareness(_Process_DPI_Awareness value);
        enum _Process_DPI_Awareness
        {
            Process_DPI_Unaware = 0,           //该种方式是告诉系统，我的程序不支持DPI aware,请通过DWM虚拟化帮我们实现
            Process_System_DPI_Aware = 1,      //该方式下告诉系统，我的程序会在启动的显示器上自己支持DPI aware,所以不需要对我进行DWM 虚拟化。但是当我的程序被拖动到其他DPI不一样的显示器时，请对我们先进行system DWM虚拟化缩放。
            Process_Per_Monitor_DPI_Aware = 2  //该方式是告诉系统，请永远不要对我进行DWM虚拟化，我会自己针对不同的Monitor的DPi缩放比率进行缩放
        }

        bool KaiGuan = false;
        private GetColor gc;

        private void button1_Click(object sender, EventArgs e)
        {
            //系统DPI识别。此应用程序无法扩展DPI更改。它将查询DPI一次，并在应用程序的生命周期中使用该值。如果DPI发生变化，则应用程序将不会调整为新的DPI值。当DPI从系统值更改时，系统将自动按比例放大或缩小。
            SetProcessDpiAwareness(_Process_DPI_Awareness.Process_System_DPI_Aware);

            KaiGuan = true;
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            gc.ObtainData(tb1, tb2, tb3, pb2, e);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            KaiGuan = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //系统DPI识别。此应用程序无法扩展DPI更改。它将查询DPI一次，并在应用程序的生命周期中使用该值。如果DPI发生变化，则应用程序将不会调整为新的DPI值。当DPI从系统值更改时，系统将自动按比例放大或缩小。
            SetProcessDpiAwareness(_Process_DPI_Awareness.Process_System_DPI_Aware);


            Timer tim = new Timer();
            tim.Interval = 1;
            tim.Tick += delegate
            {
                if (KaiGuan == true)
                {
                    Point p = new Point(MousePosition.X, MousePosition.Y);//取置鼠标实时坐标 
                    gc = new GetColor(p);
                    gc.ImageColor(pb1);
                    gc.HexColor(tb1);
                }
            };
            tim.Start();

        }
    }
}
