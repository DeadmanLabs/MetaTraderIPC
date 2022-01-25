using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using MTIPC;
using MTIPC.Communications;
using MTIPC.NamedPipe;

namespace MetaTraderIPC
{
    public partial class FrmTest : Form
    {
        private MetatraderPipe mt5;
        private string selectedPipe;
        public FrmTest()
        {
            InitializeComponent();
            selectedPipe = "\\\\.\\pipe\\";
        }

        private void log_Update(string param, string end = null)
        {
            if (end == null) { end = Environment.NewLine; }
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke((MethodInvoker)delegate
                {
                    txtLog.Text += param + end;
                    txtLog.SelectionStart = txtLog.TextLength;
                    txtLog.ScrollToCaret();
                });
            }
            else
            {
                txtLog.Text += param + end;
                txtLog.SelectionStart = txtLog.TextLength;
                txtLog.ScrollToCaret();
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            log_Update("Constructing Pipe...", "");
            mt5 = new MetatraderPipe("DemoPipe");

            //mt5.TerminalResponse += terminalResponse_Received;
            log_Update("Done!");
            while (mt5.Connected == false)
            {
                MessageBox.Show("Awaiting connection...", "Not Connected");
            }
            log_Update("Subscribing to BTCUSD...", "");
            Request InitialSubscribe = new Request("BTCUSD", true);
            mt5.Send(InitialSubscribe);
            log_Update("Done!");
            //mt5.TerminalResponse -= terminalResponse_Received;
            mt5.Stop();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            //mt5.TerminalResponse -= terminalResponse_Received;
            mt5.Stop();
        }

        private void terminalResponse_Received(object sender, object e)
        {
            log_Update("Response");
            if (true)
            {
                //Success, Handle Response
            }
            else
            {
                //Error / Fail
            }
        }

        private void pipeBox_DropDown(object sender, EventArgs e)
        {
            string[] pipes = Directory.GetFiles(@"\\.\pipe\");
            pipeBox.Items.Clear();
            foreach (string pipe in pipes)
            {
                pipeBox.Items.Add(pipe);
            }
        }
    }
}
