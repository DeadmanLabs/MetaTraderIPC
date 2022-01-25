using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading;

using MTIPC.Communications;

namespace MTIPC.NamedPipe
{
    public class MetatraderPipe
    {
        private string pipeAddress;
        public bool Connected { get; private set; }
        private NamedPipeClientStream pipe;
        private Thread thread;
        private bool runThread = true;

        public MetatraderPipe(String pipeFile)
        {
            this.pipeAddress = pipeFile;
            pipe = new NamedPipeClientStream(".", pipeFile, PipeDirection.InOut, PipeOptions.Asynchronous);
            thread = new Thread(MessageThread);
            runThread = true;
        }

        public void Connect()
        {
            pipe.Connect();
            Connected = pipe.IsConnected;
            if (Connected)
            {
                thread.Start();
            }
        }

        public void Stop()
        {
            runThread = false;
            pipe.Close();
            pipe.Dispose();
        }

        private void Reconnect()
        {
            runThread = false;
            lock (pipe)
            {
                pipe.Close();
                pipe.Dispose();
                pipe.Connect();
            }
            Connected = pipe.IsConnected;
            if (Connected)
            {
                thread.Start();
            }
        }

        public void Send(Request mqlRequest)
        {
            if (Connected)
            {
                byte[] message = Encoding.ASCII.GetBytes(mqlRequest.Generate());
                pipe.WriteAsync(message, 0, message.Count());
            }
        }

        private async void MessageThread()
        {
            StreamReader readPipe = new StreamReader(pipe);
            while (runThread)
            {
                if (Connected)
                {
                    char[] stringBuffer = new char[1024 * 16]; //16KB max load
                    int count = await readPipe.ReadAsync(stringBuffer, 0, 1024 * 16);
                    if (count > 0)
                    {
                        string response = new string(stringBuffer, 0, count);
                        //Fire Message
                    }
                }
                else { Reconnect(); }
            }
        }
    }
}
