using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KeepYourEyes
{
    public partial class Settings : Form
    {
        public static string[] sett = { };
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public static void save()

        {
          
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBox9.PlaceholderText = ($@"C:\users\{Environment.UserName}\desktop\soundfile.wav");
            if (!File.Exists(@"\data\config.data"))
            {
                Directory.CreateDirectory(@"\data");
                File.Create(@"\data\config.data");
                StreamReader streamReader = new StreamReader(@"\data\config.data");
                sett = streamReader.ReadToEnd().Split(':');
                streamReader.Close();
                streamReader.Dispose();
                GC.Collect();
            }
        }
    }
}
