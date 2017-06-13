﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Client
{
    public sealed class ServerConnection
    {
        #region Singleton
        private static volatile ServerConnection instance = null;
        private static volatile object threadSyncLock = new object();

        private ServerConnection() { }
        public static ServerConnection Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (instance == null) instance = new ServerConnection();
                    return instance;
                }
            }
        }
        #endregion Singleton
        // connection
        public string Host { set; get; }
        public ushort Port { set; get; }
        public Socket ConnectionSocket { set; get; }
        public User LoggedUser { set; get; }
        // view
        public ListBox listBlogs { set; get; }
        public ListBox listEntries { set; get; }

        public bool Connect(string host, ushort port)
        {
            Host = host;
            Port = port;
            IPHostEntry hostEntry = Dns.GetHostEntry(Host);
            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipEndPoint = new IPEndPoint(address, Port);
                Socket tempSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    tempSocket.Connect(ipEndPoint);
                    if (tempSocket.Connected)
                    {
                        ConnectionSocket = tempSocket;
                        return true;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }
        public void Disconnect()
        {
            if(ConnectionSocket != null)
            {
                Frame request = new Frame("END", null);
                Frame response;

                SendFrame(request);
                response = ReceiveFrame();

                ConnectionSocket.Close();
                ConnectionSocket = null;
            }
        }
        public bool SendFrame(Frame frame)
        {
            try
            {
                if (!ConnectionSocket.Connected)
                    return false;

                Byte[] bytesToSend = Encoding.ASCII.GetBytes(frame.ToString());
                int bytesSent = ConnectionSocket.Send(bytesToSend, bytesToSend.Length, 0);

                if (bytesSent == bytesToSend.Length)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
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
                Byte[] bytesRecevived = new Byte[1];

                while (!response.EndsWith("/rn/rn/rn$$"))
                {
                    receivedBytesCount += ConnectionSocket.Receive(bytesRecevived, bytesRecevived.Length, 0);
                    response = response + Encoding.ASCII.GetString(bytesRecevived, 0, 1);
                }
                

                string[] temp = response.Split('\t');
                if (temp.Length == 3)
                    return new Frame(temp[1], null);
                else
                    return new Frame(temp[1], temp.Skip(2).Take(temp.Length - 3).ToArray());
            }
            catch(Exception ex)
            {
                return new Frame("ERROR", null);
            }
            
        }

        public string RegisterUser(string login, string password)
        {
            Frame request = new Frame("REGISTER", new string[] { login, password });
            Frame response;

            SendFrame(request);
            response = ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "REGISTER":
                    {
                        return response.Parametres[0];
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
        public string LoginUser(string login,string password)
        {
            Frame request = new Frame("LOGIN", new string[] { login, password });
            Frame response;

            SendFrame(request);
            response = ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "LOGIN":
                    {
                        if (response.Parametres[0].Equals("OK"))
                        {
                            LoggedUser = new User(login, Convert.ToUInt32(response.Parametres[1]));
                            return response.Parametres[1];
                        }
                        else
                            return response.Parametres[0];
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
        public string LogoutUser()
        {
            Frame request = new Frame("THX_BYE", null);
            Frame response;

            SendFrame(request);
            response = ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "THX_BYE":
                    {
                        LoggedUser = null;
                        return String.Format("THX_BYE");
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
        public bool GetBlogsList()
        {
            if (ConnectionSocket.Connected == false)
                return false;
            Frame request = new Frame("DISPLAY_BLOGS", null);
            Frame response;

            SendFrame(request);
            response = ReceiveFrame();
            
            switch (response.Command)
            {
                case "QUE?":
                    {
                        return false;
                    }
                case "IDENTIFY_PLS":
                    {
                        return false;
                    }
                case "DISPLAY_BLOGS":
                    {
                        listBlogs.Items.Clear();
                        foreach (var p in response.Parametres)
                            listBlogs.Items.Add(p);
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
        public List<String> GetBlog(int id)
        {
            Frame request = new Frame("DISPLAY_BLOG", new string[] { id.ToString() });
            Frame response;

            SendFrame(request);
            response = ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return new List<String>() { "QUE?" };
                    }
                case "IDENTIFY_PLS":
                    {
                        return new List<String>() { "IDENTIFY_PLS" };
                    }
                case "DISPLAY_BLOG":
                    {
                        if (response.Parametres[0].Equals("FAILED"))
                            return new List<String>() { "FAILED" };
                        else
                            return response.Parametres.ToList();
                    }
                default:
                    {
                        return new List<String>() { "ERROR" };
                    }
            }
        }
        public string AddEntry(string title, string text)
        {
            Frame request = new Frame("ADD_ENTRY", new string[] { title, text });
            Frame response;

            SendFrame(request);
            response = ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "ADD_ENTRY":
                    {
                        if (response.Parametres[0].Equals("OK"))
                            return response.Parametres[1];
                        else
                            return response.Parametres[0];
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
        public string DisplayEntry(int id)
        {
            Frame request = new Frame("ADD_ENTRY", new string[] { id.ToString() });
            Frame response;

            SendFrame(request);
            response = ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "DISPLAY_ENTRY":
                    {
                        return String.Join("\t", response.Parametres);
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
        public string DeleteEntry(int id)
        {
            Frame request = new Frame("ADD_ENTRY", new string[] { id.ToString() });
            Frame response;

            SendFrame(request);
            response = ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "DELETE_ENTRY":
                    {
                        if (response.Parametres[0].Equals("OK"))
                            return response.Parametres[1];
                        else
                            return response.Parametres[0];
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
    }
}
