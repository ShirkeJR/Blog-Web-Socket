#define IMPROVED_PACKET_ENCRYPTION

using Blog.Constants;
using Blog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Client
{
    public sealed class ConnectionService
    {
        #region Singleton
        private static volatile ConnectionService instance = null;
        private static volatile object threadSyncLock = new object();

        private ConnectionService() { }
        public static ConnectionService Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (instance == null) instance = new ConnectionService();
                    return instance;
                }
            }
        }
        #endregion Singleton
        // connection
        public string Host { set; get; }
        public ushort Port { set; get; }
        public IPEndPoint IPEndPoint { set; get; }
        public Socket ConnectionSocket { set; get; }

        

        public bool Connect(string host, ushort port)
        {
            Host = host;
            Port = port;
            try
            {
                IPHostEntry hostEntry = Dns.Resolve(Host);
                foreach (IPAddress address in hostEntry.AddressList)
                {                   
                    IPEndPoint ipEndPoint = new IPEndPoint(address, Port);
                    Socket tempSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                   
                    tempSocket.Connect(ipEndPoint);
                    if (tempSocket.Connected)
                    {
                        ConnectionSocket = tempSocket;
                        IPEndPoint = ipEndPoint;
                        return Ping();
                    }
                     
                    
                }
                return false;
            }
            catch
            {
                return false;
            }
            
            
        }
        public bool Reconnect()
        {
            if (ConnectionSocket != null) Disconnect(); 
            return Connect(Host, Port);
        }
        public bool Connected()
        {
            if (ConnectionSocket != null) return ConnectionSocket.Connected;
            else return false;
        }
        public bool Disconnect()
        {
            if (ConnectionSocket != null)
            {
                if (ConnectionSocket.Connected)
                {
                    Frame request = new Frame(StringConstants.ConnectionClosePacketName, null);
                    SendFrame(request);
                    ConnectionSocket.Close();
                }
                ConnectionSocket = null;
                return true;
            }
            else return false;
        }
        public bool Ping()
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame(StringConstants.PingPacketName, null);
            Frame response = null;

            SendFrame(request);
            response = ReceiveFrame();

            return (!response.CheckError());
        }
    
        public bool SendFrame(Frame frame)
        {
            try
            {
                if (!ConnectionSocket.Connected) return false;

#if IMPROVED_PACKET_ENCRYPTION
                string toSend = frame.EncryptFrame();
#else
                string toSend = frame.ToString();
#endif
                byte[] bytesToSend = Encoding.ASCII.GetBytes(toSend);
                int bytesSent = ConnectionSocket.Send(bytesToSend, bytesToSend.Length, 0);

                if (bytesSent == bytesToSend.Length) return true;
                else return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public Frame ReceiveFrame()
        {
            try
            {
                int receivedBytesCount = 0;
                string response = "";
                byte[] bytesReceived = new byte[1];

                while (!response.EndsWith(StringConstants.PacketEnding))
                {
                    receivedBytesCount += ConnectionSocket.Receive(bytesReceived, bytesReceived.Length, 0);
                    response = response + Encoding.ASCII.GetString(bytesReceived, 0, 1);
                }

#if IMPROVED_PACKET_ENCRYPTION
                return new Frame(response, false);
#endif
                string[] temp = response.Split('\t');
                if (temp.Length == 3) return new Frame(temp[1], null, false, Convert.ToInt32(temp[0]));
                else return new Frame(temp[1], temp.Skip(2).Take(temp.Length - 3).ToArray(), false, Convert.ToInt32(temp[0]));
            }
            catch(Exception ex)
            {
                return new Frame("EMPTY", null, false);
            } 
        }
    }
}
