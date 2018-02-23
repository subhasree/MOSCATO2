using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NotificationWindow;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MOSCATO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            runMouseTracker();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void runMouseTracker()
        {
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("java.exe");
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.RedirectStandardError = false;
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            myProcessStartInfo.Arguments = "-jar MouseTracker.jar";

            Process myProcess = new Process();
            myProcess.StartInfo = myProcessStartInfo;
            myProcess.Start();
        }
    }
}
