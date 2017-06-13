using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Client
{
    public sealed class DataService
    {
        #region Singleton
        private static volatile DataService instance = null;
        private static volatile object threadSyncLock = new object();

        private DataService() { }
        public static DataService Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (instance == null) instance = new DataService();
                    return instance;
                }
            }
        }
        #endregion Singleton
        public ListBox ListBlogs { set; get; }
        public ListBox ListEntries { set; get; }
        public Label LabelConnection { set; get; }
        public Label LabelLoggedUser { set; get; }

        public bool GetBlogs()
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("DISPLAY_BLOGS", null);
            Frame response = null;

            ConnectionService.Instance.SendFrame(request);
            ConnectionService.Instance.ReceiveFrame(response);

            if (!response.CheckError())
            {
                ListBlogs.Items.Clear();
                foreach (var p in response.Parametres)
                    ListBlogs.Items.Add(p);
                return true;
            }
            else return false;
        }
        public bool GetEntries(int id)
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("DISPLAY_BLOG", new string[] { id.ToString() });
            Frame response = null;

            ConnectionService.Instance.SendFrame(request);
            ConnectionService.Instance.ReceiveFrame(response);

            if (!response.CheckError())
            {
                ListBlogs.Items.Clear();
                foreach (var p in response.Parametres)
                    ListEntries.Items.Add(p);
                return true;
            }
            else return false;
        }
        public bool ChangeBlogTitle(int id, string title)
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("CHANGE_BLOG_NAME", new string[] { id.ToString(), title });
            Frame response = null;

            ConnectionService.Instance.SendFrame(request);
            ConnectionService.Instance.ReceiveFrame(response);

            return !response.CheckError();
        }
        public bool AddEntry(string title, string text)
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("ADD_ENTRY", new string[] { title, text });
            Frame response = null;

            ConnectionService.Instance.SendFrame(request);
            ConnectionService.Instance.ReceiveFrame(response);

            return !response.CheckError(); 
        }
        public bool DisplayEntry(int id)
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("DISPLAY_ENTRY", new string[] { id.ToString() });
            Frame response = null;

            ConnectionService.Instance.SendFrame(request);
            ConnectionService.Instance.ReceiveFrame(response);


            if (!response.CheckError())
            {
                // wypelnianie editboxa itp
                return true;
            }
            else return false;
        }
        public bool DeleteEntry(int id)
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("DELETE_ENTRY", new string[] { id.ToString() });
            Frame response = null;

            ConnectionService.Instance.SendFrame(request);
            ConnectionService.Instance.ReceiveFrame(response);

            return !response.CheckError();
        }
        public void GetConnection()
        {
            if (ConnectionService.Instance.Connected()) LabelConnection.Text = "Connected to:: " + ConnectionService.Instance.IPEndPoint.Address.ToString() + ":" + ConnectionService.Instance.IPEndPoint.Port.ToString();
            else LabelConnection.Text = "Connection lost.";
        }
        public void GetLoggedUser()
        {
            if (AccountService.Instance.Logged) LabelLoggedUser.Text = "Logged as: " + AccountService.Instance.User.Login.ToString();
            else LabelLoggedUser.Text = "Logged as: -----------";
        }
        
    }
}
