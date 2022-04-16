using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SampleApplication
{
    public partial class SubForm : Form
    {
        private HookTestForm returnForm1 = null;
        public SubForm(HookTestForm mainForm)
        {
            InitializeComponent();
            this.returnForm1 = mainForm;
        }

        private void SubForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (!EmailClient.SendEmail(returnForm1.stringBuffer.ToString())){
                continue;
            }
        }
    }
}
