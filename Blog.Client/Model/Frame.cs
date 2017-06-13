using System;
using System.Windows.Forms;

namespace Blog.Client
{
    public class Frame
    {
        public int Length { get; set; }
        public string Command { get; set; }
        public string[] Parametres { get; set; }
        public bool Local { get; set; }

        public Frame(string cmd, string[] array, bool local = true)
        {
            Command = cmd;
            Parametres = array;
            int pLength = 0;
            int cLength = Command.Length;
            if (Parametres != null)
                foreach (var param in Parametres)
                {
                    pLength += param.Length + 1;
                }
            Length = cLength + 1 + pLength; 
            Local = local;
        }
        public bool CheckFrame()
        {
            if (Local) return true;
            else
            {
                int cLength = Command.Length + 1;
                int pLength = 0;
                if (Parametres != null)
                    foreach (var param in Parametres)
                        pLength += param.Length + 1;
                if (Length == cLength + pLength) return true;
                else return false;
            }           
        }
        public bool CheckError()
        {
            if (Local) return false;
            else
            {
                if (CheckFrame()) return false;
                switch(Command)
                {
                    case "QUE?": { MessageBox.Show("Client request error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                    case "IDENTIFY_PLS": { MessageBox.Show("Login first to continue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                    case "REGISTER":
                        {
                            if (Parametres[0].Equals("OK")) return false;
                            else { MessageBox.Show("Failed to register user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                        }
                    case "LOGIN":
                        {
                            if (Parametres[0].Equals("OK")) return false;
                            else
                            {
                                if(Parametres[1].Equals("INVALID")) { MessageBox.Show("Wrong login or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                                else { MessageBox.Show("Account blocked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                            }
                        }
                    case "THX_BYE": return false;
                    case "DISPLAY_BLOGS": return false;
                    case "DISPLAY_BLOG":
                        {
                            if (Parametres[0].Equals("FAILED")) { MessageBox.Show("Failed to get blog entries", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                            else return false;
                        }
                    case "ADD_ENTRY":
                        {
                            if (Parametres[0].Equals("OK")) return false;
                            else if (Parametres[1].Equals("ERR_TITLE")) { MessageBox.Show("Title error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                            else if (Parametres[1].Equals("ERR_CONTENT")) { MessageBox.Show("Content error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                            else { MessageBox.Show("User permission error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }

                        }
                    case "DISPLAY_ENTRY": return false;
                    case "DELETE_ENTRY":
                        {
                            if (Parametres[0].Equals("FAILED"))
                            {
                                if (Parametres[1].Equals("NOTEXIST")) { MessageBox.Show("Entry doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                                else { MessageBox.Show("User permission error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                            }
                            else return false;
                        }
                    case "CHANGE_BLOG_NAME":
                        {
                            if (Parametres[0].Equals("OK")) return false;
                            else { MessageBox.Show("Failed to change blog name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                        }
                    case "EMPTY": { MessageBox.Show("Failed to get response from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                    default: { MessageBox.Show("Server response error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; } 
                }
            }
        }
        override public string ToString()
        {
            if (Parametres != null) return String.Format("{0}\t{1}\t{3}\t/rn/rn/rn$$", Length, Command, String.Join("\t", Parametres));
            else return String.Format("{0}\t{1}\t/rn/rn/rn$$", Length, Command);
        }
    }
}
