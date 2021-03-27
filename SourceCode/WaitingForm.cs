using System;
using System.Drawing;
using System.Windows.Forms;
using WiiCommon;

namespace Wii
{
    public partial class WaitingForm : WiiSystemBase
    {
        public WaitingForm()
        {
            InitializeComponent();
            this.AllowTransparency = true;
            this.TransparencyKey = this.BackColor;
        }

        private void WaitingForm_Load(object sender, EventArgs e)
        {
            circularProgressBar.Size = new Size(136, 136);
            this.Size = circularProgressBar.Size;
            circularProgressBar.Dock = DockStyle.Fill;
        }
    }
}
