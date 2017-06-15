using Blog.Constants;
using Blog.Utils;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Blog.Client
{
    public class Frame
    {
        public int Length { get; set; }
        public string Command { get; set; }
        public string[] Parametres { get; set; }
        public bool Local { get; set; }

        public Frame(string cipher, bool local = true)
        {
            Local = local;
            string temp = "";
            try
            {
                temp = CryptoService.Decrypt<AesManaged>(cipher.Substring(0, cipher.Length - StringConstants.PacketEnding.Length), StringConstants.SymmetricKey, StringConstants.SymmetricSalt);
            }
            catch
            {
                temp = "EMPTY";
            }
            temp += StringConstants.PacketEnding;
            string[] arr = temp.Split('\t');

            if (arr.Length < 3) { Command = "EMPTY"; Length = 0; Parametres = null; }
            if (arr.Length == 3)
            {
                Command = arr[1];
                Parametres = null;
                Length = Command.Length + 1;

            }
            if (arr.Length > 3)
            {
                Command = arr[1];
                Parametres = arr.Skip(2).Take(arr.Length - 3).ToArray();
                int pLength = 0;
                int cLength = Command.Length;
                if (Parametres != null)
                    foreach (var param in Parametres)
                    {
                        pLength += param.Length + 1;
                    }
                Length = cLength + 1 + pLength;
            }
            if(!local && arr.Length >= 3) Length = Convert.ToInt32(arr[0]);
        }
        public Frame(string cmd, string[] array, bool local = true, int length = 0)
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
            if (!local) Length = length;
        }
        public bool CheckLength()
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
                if (!CheckLength()) return true;
                switch(Command)
                {
                    case StringConstants.UnrecognizedCommandAnswer: { MessageBox.Show("Client request error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                    case StringConstants.LoginRequiredAnswer: { MessageBox.Show("Login first to continue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                    case StringConstants.RegisterPacketName:
                        {
                            if (StringConstants.RegisterPacketAnswerOK.Equals(Parametres[0])) return false;
                            else { MessageBox.Show("Failed to register user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                        }
                    case StringConstants.LoginPacketName:
                        {
                            if (StringConstants.LoginPacketAnswerOK.Equals(Parametres[0])) return false;
                            else
                            {
                                if(StringConstants.LoginPacketAnswerFailedInvalid.Equals(Parametres[1])) { MessageBox.Show("Wrong login or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                                else if(StringConstants.LoginPacketAnswerFailedLogged.Equals(Parametres[1])) { MessageBox.Show("Someone is already logged on this account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                                else { MessageBox.Show("Account blocked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                            }
                        }
                    case StringConstants.LogoutPacketName: return false;
                    case StringConstants.DisplayBlogsPacketName: return false;
                    case StringConstants.DisplayBlogPacketName:
                        {
                            if (StringConstants.DisplayBlogPacketAnswerFailed.Equals(Parametres[0])) { /*MessageBox.Show("Failed to get blog entries", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);*/ return true; }
                            else return false;
                        }
                    case StringConstants.AddEntryPacketName:
                        {
                            if (StringConstants.AddEntryPacketAnswerOK.Equals(Parametres[0])) return false;
                            else
                            {
                                if (StringConstants.AddEntryPacketAnswerInvalidTitle.Equals(Parametres[2])) { MessageBox.Show("Title error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                                else if (StringConstants.AddEntryPacketAnswerInvalidContent.Equals(Parametres[2])) { MessageBox.Show("Content error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                                else { MessageBox.Show("User permission error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                            }
                        }
                    case StringConstants.DisplayEntryPacketName:
                        {
                            if (StringConstants.DisplayEntryPacketAnswerFailed.Equals(Parametres[1])) return true;
                            else return false;
                        }
                    case StringConstants.DeleteEntryPacketName:
                        {
                            if (StringConstants.DeleteEntryPacketAnswerFailed.Equals(Parametres[0]))
                            {
                                if (StringConstants.DeleteEntryPacketAnswerFailedNotExist.Equals(Parametres[1])) { MessageBox.Show("Entry doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                                else { MessageBox.Show("User permission error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                            }
                            else return false;
                        }
                    case StringConstants.ChangeBlogNamePacketName:
                        {
                            if (StringConstants.ChangeBlogNamePacketAnswerOK.Equals(Parametres[0])) return false;
                            else { MessageBox.Show("Failed to change blog name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                        }
                    case StringConstants.PingPacketAnswer: return false;
                    case "EMPTY": { MessageBox.Show("Failed to get response from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; }
                    default: { MessageBox.Show("Server response error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return true; } 
                }
            }
        }
        public string EncryptFrame()
        {
            string temp = "";
            if (Parametres != null) temp = string.Format(string.Format("{0}\t", StringConstants.GlobalPacketFormat), Length, Command, string.Join("\t", Parametres));
            else temp = string.Format("{0}\t{1}\t", Length, Command);

            return string.Format("{0}{1}", CryptoService.Encrypt<AesManaged>(temp, StringConstants.SymmetricKey, StringConstants.SymmetricSalt), StringConstants.PacketEnding);
        }
        override public string ToString()
        {
            if (Parametres != null) return string.Format(string.Format("{0}\t{1}", StringConstants.GlobalPacketFormat, StringConstants.PacketEnding), Length, Command, string.Join("\t", Parametres));
            else return string.Format("{0}\t{1}\t{2}", Length, Command, StringConstants.PacketEnding);
        }
    }
}
