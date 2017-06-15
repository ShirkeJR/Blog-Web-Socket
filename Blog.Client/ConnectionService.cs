﻿#define IMPROVED_PACKET_ENCRYPTION

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
                IPHostEntry hostEntry = Dns.GetHostEntry(Host);
                foreach (IPAddress address in hostEntry.AddressList)
                {                   
                    IPEndPoint ipEndPoint = new IPEndPoint(address, Port);
                    if (ipEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        Socket tempSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        try
                        {
                            tempSocket.Connect(ipEndPoint);
                            if (tempSocket.Connected)
                            {
                                ConnectionSocket = tempSocket;
                                IPEndPoint = ipEndPoint;
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
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
        public bool SendFrame(Frame frame)
        {
            try
            {
                if (!ConnectionSocket.Connected) return false;

#if IMPROVED_PACKET_ENCRYPTION
                var frameString = frame.ToString();
                string toSend = string.Format("{0}{1}", CryptoService.Encrypt<AesManaged>(frameString.Substring(0, frameString.Length - StringConstants.PacketEnding.Length), StringConstants.SymmetricKey, StringConstants.SymmetricSalt), StringConstants.PacketEnding);
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
        public bool ReceiveFrame(Frame frame)
        {
            try
            {
                int receivedBytesCount = 0;
                string response = "";
                Byte[] bytesReceived = new Byte[1];

                while (!response.EndsWith(StringConstants.PacketEnding))
                {
                    receivedBytesCount += ConnectionSocket.Receive(bytesReceived, bytesReceived.Length, 0);
                    response = response + Encoding.ASCII.GetString(bytesReceived, 0, 1);
                }

                string[] temp = response.Split('\t');
                if (temp.Length == 3) frame = new Frame(temp[1], null, false);  
                else frame = new Frame(temp[1], temp.Skip(2).Take(temp.Length - 3).ToArray(), false);
                return true;
            }
            catch (Exception ex)
            {
                frame = new Frame("EMPTY", null, false);
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
                var response2 = CryptoService.Decrypt<AesManaged>(response.Substring(0, response.Length - StringConstants.PacketEnding.Length), StringConstants.SymmetricKey, StringConstants.SymmetricSalt);
                response = string.Format("{0}{1}", response2, response.Substring(response.Length - StringConstants.PacketEnding.Length));
#endif
                string[] temp = response.Split('\t');
                if (temp.Length == 3) return new Frame(temp[1], null, false);
                else return new Frame(temp[1], temp.Skip(2).Take(temp.Length - 3).ToArray(), false);
            }
            catch(Exception ex)
            {
                return new Frame("EMPTY", null, false);
            } 
        }
    }
}
