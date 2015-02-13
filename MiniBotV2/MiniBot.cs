using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MiniBotV2
{
    public partial class MiniBot : Form
    {
        BackgroundWorker worker = new BackgroundWorker();


        IRC test = new IRC();

        public MiniBot()
        {
            InitializeComponent();
            

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] parameters = e.Argument as string[];
            List<string> output = new List<string>();
            if (!test.HasConnected)
                test.Connect(parameters[0],worker);

            if (Config.shouldRun)
            {
                output = test.Loop();
                foreach (string i in output.ToArray())
                    worker.ReportProgress(10, i);
            }
            worker.RunWorkerAsync(e);
            e.Result = true;
        }

        private void MiniBot_Load(object sender, EventArgs e)
        {
            Config.Console = new TextBox();

            Console.SetOut(new TextBoxWriter(txtOutput));
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);

            

        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                Console.WriteLine(e.UserState);
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 1000; i++)
            {
                Console.WriteLine(i);
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] parameters = new string[] { "minijackb" };
            worker.RunWorkerAsync(parameters);
        }

        private void tmrConsoleUpdate_Tick(object sender, EventArgs e)
        {

        }


    }

    
}
