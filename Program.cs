using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Media;
using System.Drawing.Drawing2D;
using Microsoft.Win32;

namespace KeepYourEyes
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        
        private static Stopwatch Stopwatch1 = new Stopwatch();
        public static bool firsttime = false;
        public static NotifyIcon notificationIcon;
        [STAThread]
        static void Main()
        {
            SetStartup(true);
            const string appName = "KeepYourEyes";
            bool createdNew;

            var mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                //app is already running! Exiting the application  
                MessageBox.Show("Multiple applactions not allowed!");
                Application.ExitThread();
                Environment.Exit(0);
            }
            //int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon
            GC.Collect();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            if (firsttime)
            {
                Application.Run(new Settings());
                firsttime = false;
            }
            else
            {
                Stopwatch1.Start();
                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipText = "Welcome to TutorialsPanel.com!!";
                notifyIcon.BalloonTipTitle = "Welcome Message";
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(2000);
                while (true)
                {
                    GC.Collect();
                    Thread.Sleep(1000);
                    if (Stopwatch1.Elapsed.Minutes == 20)
                    {
                        Thread t = new Thread(new ThreadStart(beep));
                        Application.Run(new Form1());
                        Stopwatch1.Stop();
                        Stopwatch1.Reset();
                        Stopwatch1.Start();
                    }
#if DEBUG
          Debug.WriteLine($"Time so far: "+Stopwatch1.Elapsed + " MEMORY: "+ GC.GetTotalMemory(true));          // debug stuff goes here
#endif   
                }
            }
        }
        private static void SetStartup(bool p)
        {
            string name = "KeepYourEyes";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (p == true)
                rk.SetValue(name, Application.ExecutablePath);
            else
                rk.DeleteValue(name, false);

        }
        public static void beep()
        {
            
            for (int i = 0; i < 5; i++)
            {
                GC.Collect();
                using (var soundPlayer = new SoundPlayer(Properties.Resources.Alarm))
                {
                    soundPlayer.Load();
                    soundPlayer.Play();
                }
                
            }

                GC.Collect();
        }
    }
}
