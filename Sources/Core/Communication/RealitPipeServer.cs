using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;


namespace RealitSystem_CLI.Communication
{
    public class RealitPipeServer
    {
        private object @lock = new object();
        private bool _requestClosing;
        public bool RequestClosing
        {
            get
            {
                lock (@lock)
                    return _requestClosing;
            }
            set
            {
                lock (@lock)
                    _requestClosing = value;
            }
        }

        public NamedPipeServerStream pipeServer;
        Thread thread;
        
        public RealitPipeServer()
        {
            
        }

        public void Start()
        {
            thread = new Thread(ServerThread);
            thread.Start();
        }
        public void Close()
        {
            RequestClosing = true;
        }

        private void ServerThread(object data)
        {
            pipeServer = new NamedPipeServerStream("RealitPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message);
            int threadId = Thread.CurrentThread.ManagedThreadId;
            // Wait for a client to connect
            pipeServer.WaitForConnection();
            Console.WriteLine($"Client connected on thread[{threadId}].");
            StreamString ss = new StreamString(pipeServer);

            try
            {
                while (!RequestClosing)
                {
                    string s = ss.ReadString();
                    if (!string.IsNullOrEmpty(s))
                    {
                        Console.WriteLine(s);
                    }
                    
                    Thread.Sleep(5);
                }
                pipeServer.Close();
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch(Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
        }
    }
}