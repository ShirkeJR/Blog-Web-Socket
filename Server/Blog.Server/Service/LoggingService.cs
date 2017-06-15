using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Blog.Server
{
    internal class LoggingService
    {
        #region Singleton

        private static LoggingService _instance = null;
        private static volatile object _threadSyncLock = new object();
        private static bool _logginEnabled = true;
        private static string _logDirectory = "logs";

        private static string _logFileName = Path.Combine(_logDirectory, string.Format("log_{0}_{1}_{2}_{3}_{4}.txt",
                                                                                            DateTime.Now.Year,
                                                                                            DateTime.Now.Month < 10 ? string.Format("0{0}", DateTime.Now.Month) : DateTime.Now.Month.ToString(),
                                                                                            DateTime.Now.Day < 10 ? string.Format("0{0}", DateTime.Now.Day) : DateTime.Now.Day.ToString(),
                                                                                            DateTime.Now.Hour < 10 ? string.Format("0{0}", DateTime.Now.Hour) : DateTime.Now.Hour.ToString(),
                                                                                            DateTime.Now.Minute < 10 ? string.Format("0{0}", DateTime.Now.Minute) : DateTime.Now.Minute.ToString()));

        private LoggingService()
        {
            string directoryPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), _logDirectory);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        public static LoggingService Instance
        {
            get
            {
                lock (_threadSyncLock)
                {
                    if (_instance == null)
                        _instance = new LoggingService();
                    return _instance;
                }
            }
        }

        #endregion Singleton

        private ListBox _logBox = null;
        private ListBox _clientBox = null;

        #region Metody

        public void Initialize(ListBox logBox, ListBox clientBox)
        {
            _logBox = logBox;
            _clientBox = clientBox;
        }

        public void AddLog(string message)
        {
            _logBox.Invoke((MethodInvoker)delegate
            {
                _logBox.Items.Add(message);
            });
            WriteText(string.Format("[{0}]\t{1}", DateTime.Now, message));
        }

        public void AddClient(ClientData clientData)
        {
            _clientBox.Invoke((MethodInvoker)delegate
            {
                _clientBox.Items.Add(clientData.ToString());
            });
            WriteText(clientData.ToString() + " connected");
        }

        public void RemoveClient(ClientData clientData)
        {
            _clientBox.Invoke((MethodInvoker)delegate
            {
                _clientBox.Items.Remove(clientData.ToString());
            });
            WriteText(clientData.ToString() + " disconnected.");
        }

        public void WriteException(Exception ex)
        {
            string line = string.Format("[{0}]\t{1}:\t{2}\t{3}\t{4}\n", DateTime.Now.ToLocalTime(), (new StackTrace()).GetFrame(1).GetMethod().Name, ex.Message, ex.Source, ex.InnerException);
            WriteText(line);
        }

        public void WriteText(string text)
        {
            if (!_logginEnabled) return;
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), _logFileName);
            using (StreamWriter sw = new StreamWriter(path, true))
                sw.WriteLine(text);
        }

        #endregion Metody
    }
}