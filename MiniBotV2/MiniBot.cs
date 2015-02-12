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

            test.Connect(parameters[0]);

            e.Result = true;
        }

        private void MiniBot_Load(object sender, EventArgs e)
        {
            Console.SetOut(new TextBoxWriter(txtOutput));

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);

            

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
            string[] parameters = new string[] { "nightblue3" };
            worker.RunWorkerAsync(parameters);
        }


    }

    
}
