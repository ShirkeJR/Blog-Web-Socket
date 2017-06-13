using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool GetBlogsList()
        {
            if (ConnectionService.Instance.ConnectionSocket.Connected == false)
                return false;
            Frame request = new Frame("DISPLAY_BLOGS", null);
            Frame response;

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

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
                        ListBlogs.Items.Clear();
                        foreach (var p in response.Parametres)
                            ListBlogs.Items.Add(p);
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

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

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

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

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

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

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
            Frame request = new Frame("DELETE_ENTRY", new string[] { id.ToString() });
            Frame response;

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

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
