using System;
using System.Windows.Forms;
using Memory;
using System.Threading;
using System.Runtime.InteropServices;

namespace homefront
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int a, int b, int c, int d, int damnIwonderifpeopleactuallyreadsthis);

        int leftDown = 0x02;
        int leftUp = 0x04;
        int flag = 0;


        Mem m = new Mem();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int PID = m.GetProcIdFromName("CoDWaW");
            if (PID > 0)
            {
                m.OpenProcess(PID);
                Thread TB = new Thread(triggerbot) { IsBackground = true };
                TB.Start();
            }
        }


        void shoot(int delay)
        {
            mouse_event(leftDown, 0, 0, 0, 0);
            Thread.Sleep(1);
            mouse_event(leftUp, 0, 0, 0, 0);
            Thread.Sleep(delay);
        }

        void triggerbot()
        {
            while (true)
            {
                if (GetAsyncKeyState(Keys.XButton2)<0)
                {
                    flag = m.ReadInt("CoDWaW.exe+0x14ED078");
                    if (flag == 16)
                    {
                        shoot(10);
                        shoot(1);
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
