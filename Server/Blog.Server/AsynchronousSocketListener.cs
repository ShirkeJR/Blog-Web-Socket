using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Blog.Server
{
    class AsynchronousSocketListener
    {
        #region Singleton

        private static volatile AsynchronousSocketListener _instance = null;
        private static volatile object threadSyncLock = new object();


        private AsynchronousSocketListener()
        {
        }

        public static AsynchronousSocketListener Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null) _instance = new AsynchronousSocketListener();
                    return _instance;
                }
            }
        }

        #endregion Singleton

        // Thread signal.  
        private static ManualResetEvent allDone = new ManualResetEvent(false);
        public ListBox logBox { set; get; }
        public ListBox clientBox { set; get; }

        public void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();
                    
                    // Start an asynchronous socket to listen for connections.  
                    logBox.Invoke((MethodInvoker)delegate { logBox.Items.Add("Waiting for a connection..."); });
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            clientBox.Invoke((MethodInvoker)delegate { clientBox.Items.Add(((IPEndPoint)(handler.RemoteEndPoint)).Address.ToString()); });

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public async void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();
                logBox.Invoke((MethodInvoker)delegate { logBox.Items.Add("Read " + content.Length + " bytes from socket. \n Data : " + content); });
                content = await PacketAnalyzeService.Instance.getPacketResponse(content); // ustawić na dostęp z kilku wątków
                Send(handler, content);

                //if (packetSize == content.Length)
                //{
                //    // All the data has been read from the   
                //    // client. Display it on the console.  
                //    logBox.Invoke((MethodInvoker)delegate { logBox.Items.Add("Read " + content.Length + " bytes from socket. \n Data : " + content); });
                //    // Echo the data back to the client.  
                //    Send(handler, content);
                //}
                //else
                //{
                //    // Not all data received. Get more.  
                //    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                //    new AsyncCallback(ReadCallback), state);
                //}
            }
        }

        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                logBox.Invoke((MethodInvoker)delegate { logBox.Items.Add("Sent " + bytesSent + " bytes to client."); });
                clientBox.Invoke((MethodInvoker)delegate { clientBox.Items.Remove(((IPEndPoint)(handler.RemoteEndPoint)).Address.ToString()); });
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}