using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeepYourEyes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public bool abc = false;
        private void button1_Click(object sender, EventArgs e)
        {
            abc = false;
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(dowork));
            t.Start();
            this.BringToFront();
            this.Focus();
            this.TopMost = true;
            this.Show();
            this.Visible = true;
            this.Text = "Keep your eyes!";
        }
        private void dowork()
        {

            Stopwatch a123 = new Stopwatch();

            a123.Start();
            while (a123.Elapsed.Seconds <20)
            {
                Thread.Sleep(900);
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Text = "Total time:"+a123.Elapsed.Seconds.ToString(); }));
                }
               
            }
            a123.Stop();
            a123.Reset();
            SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.Alarm);
            soundPlayer.Load();
            if (!abc)
            {
soundPlayer.Play();
                abc = true;
            }
            

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            abc = true;
        }
    }
}
