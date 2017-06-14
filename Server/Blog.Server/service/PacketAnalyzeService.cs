using Blog.Constants;
using System;
using System.Threading.Tasks;

namespace Blog.Server
{
    internal class PacketAnalyzeService
    {
        #region Singleton

        private static volatile PacketAnalyzeService _instance = null;
        private static volatile object threadSyncLock = new object();

        private PacketAnalyzeService()
        {
        }

        public static PacketAnalyzeService Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null) _instance = new PacketAnalyzeService();
                    return _instance;
                }
            }
        }

        #endregion Singleton

        public async Task<string> getPacketResponse(string content, ClientData clientData)
        {
            string[] packet = content.Split('\t');
            int packetSize = Convert.ToInt32(packet[0]);
            string packetType = packet[1];
            string returnMessage;
            switch (packetType)
            {
                case StringConstants.RegisterPacketName:
                    returnMessage = await buildRegister(packet);
                    break;

                case StringConstants.LoginPacketName:
                    returnMessage = await buildLogin(packet, clientData);
                    break;

                case StringConstants.DisplayBlogsPacketName:
                    returnMessage = await buildDisplayBlogs(packet);
                    break;

                case StringConstants.DisplayBlogPacketName:
                    returnMessage = await buildDisplayBlog(packet);
                    break;

                case StringConstants.AddEntryPacketName:
                    returnMessage = await buildAddEntry(packet, clientData);
                    break;

                case StringConstants.DisplayEntryPacketName:
                    returnMessage = await buildDisplayEntry(packet);
                    break;

                case StringConstants.DeleteEntryPacketName:
                    returnMessage = await buildDeleteEntry(packet, clientData);
                    break;

                case StringConstants.ChangeBlogNamePacketName:
                    returnMessage = await buildChangeBlogName(packet, clientData);
                    break;

                case StringConstants.LogoutPacketName:
                    returnMessage = buildLogout(packet, clientData);
                    break;

                case StringConstants.ConnectionClosePacketName:
                    returnMessage = buildExit(packet);
                    break;

                case StringConstants.PingPacketName:
                    returnMessage = "PONG";
                    break;

                default:
                    returnMessage = buildDefault(packet);
                    break;
            }
            //returnMessage = returnMessage + "\t";
            //content = returnMessage.Length + "\t" + returnMessage + "/rn/rn/rn$$";
            content = string.Format(StringConstants.GlobalPacketFormat, returnMessage.Length + 1, returnMessage, StringConstants.PacketEnding);
            return content;
        }

        private async Task<string> buildRegister(string[] packet)
        {
            string param = (await AccountService.Instance.Register(packet[2], packet[3])) ? StringConstants.RegisterPacketAnswerOK : StringConstants.RegisterPacketAnswerInvalid;

            return string.Format(StringConstants.RegisterPacketAnswerFormat, StringConstants.RegisterPacketName, param);
        }

        private async Task<string> buildLogin(string[] packet, ClientData clientData)
        {
            string param1 = string.Empty, param2 = string.Empty;
            int id = -1;
            if ((id = await AccountService.Instance.Login(packet[2], packet[3])) > 0) // jeśli istnieje i prawidłowe logowanie
            {
                param1 = StringConstants.LoginPacketAnswerOK;
                param2 = Convert.ToString(id);
                clientData.Id = id;
            }
            else if (id < 0) // nieprawidłowe dane
            {
                param1 = StringConstants.LoginPacketAnswerFailed;
                param2 = StringConstants.LoginPacketAnswerFailedInvalid;
            }
            else if ((await AccountService.Instance.IsLocked(packet[2]))) // zablokowane
            {
                param1 = StringConstants.LoginPacketAnswerFailed;
                param2 = StringConstants.LoginPacketAnswerFailedLocked;
            }
            return string.Format(StringConstants.LoginPacketAnswerFormat, StringConstants.LoginPacketName, param1, param2);
        }

        private async Task<string> buildDisplayBlogs(string[] packet)
        {
            string paramsList = await BlogService.Instance.DisplayBlogs();
            if (paramsList.Length > 2)
                return string.Format(StringConstants.DisplayBlogsPacketAnswerFormat, StringConstants.DisplayBlogsPacketName, paramsList);
            else
                return StringConstants.DisplayBlogsPacketName;
        }

        private async Task<string> buildDisplayBlog(string[] packet)
        {
            int id = Convert.ToInt32(packet[2]);
            if (await BlogService.Instance.Exists(id))// Jeżeli blog X istnieje
            {
                string paramsList = await BlogService.Instance.DisplayBlogPosts(id); //lista wpisów w blogu
                if (paramsList.Length > 2)
                    return string.Format(StringConstants.DisplayBlogPacketAnswerFormat, StringConstants.DisplayBlogPacketName, paramsList);
                else
                    return StringConstants.DisplayBlogPacketName;
            }
            else
                return string.Format(StringConstants.DisplayBlogPacketAnswerFormat, StringConstants.DisplayBlogPacketName, StringConstants.DisplayBlogPacketAnswerFailed);
        }

        private async Task<string> buildAddEntry(string[] packet, ClientData clientData)
        {
            string param1 = string.Empty, param2 = string.Empty;
            if (clientData.Id != -1) // zalogowany
            {
                if (await BlogService.Instance.AddEntry(clientData.Id, packet[2], packet[3]))
                {
                    param1 = StringConstants.AddEntryPacketAnswerOK;
                    param2 = Convert.ToString(await BlogService.Instance.GetEntryId(clientData.Id, packet[2]));
                }
                else if (packet[2].Length > Int16Constants.BlogPostTitleMaxLength) // zły tytuł
                {
                    param1 = StringConstants.AddEntryPacketAnswerInvalid;
                    param2 = StringConstants.AddEntryPacketAnswerInvalidTitle;
                }
                else if (packet[3].Length > Int16Constants.BlogPostContentMaxLength) // zła treść
                {
                    param1 = StringConstants.AddEntryPacketAnswerInvalid;
                    param2 = StringConstants.AddEntryPacketAnswerInvalidContent;
                }
            }
            else return StringConstants.LoginRequiredAnswer;

            return string.Format(StringConstants.AddEntryPacketAnswerFormat, StringConstants.AddEntryPacketName, param1, param2);
        }

        private async Task<string> buildDisplayEntry(string[] packet)
        {
            int id = Convert.ToInt32(packet[2]);
            string entry = await BlogService.Instance.DisplayEntry(id); // wpis w blogu
            return string.Format(StringConstants.DisplayEntryPacketFormat, StringConstants.DisplayEntryPacketName, entry);
        }

        private async Task<string> buildDeleteEntry(string[] packet, ClientData clientData)
        {
            string param1 = StringConstants.DeleteEntryPacketAnswerFailed,
                param2 = StringConstants.DeleteEntryPacketAnswerFailedNotOwner;
            int id = Convert.ToInt32(packet[2]);
            if (clientData.Id != -1) // UDAŁO SIĘ
            {
                if (await BlogService.Instance.DeleteEntry(id, clientData.Id))
                {
                    param1 = StringConstants.DeleteEntryPacketAnswerOK;
                    param2 = Convert.ToString(id);
                }
                else
                {
                    param1 = StringConstants.DeleteEntryPacketAnswerFailed;
                    param2 = StringConstants.DeleteEntryPacketAnswerFailedNotExist;
                }
            }
            return string.Format(StringConstants.DeleteEntryPacketAnswerFormat, StringConstants.DeleteEntryPacketName, param1, param2);
        }

        private async Task<string> buildChangeBlogName(string[] packet, ClientData clientData)
        {
            int id = Convert.ToInt32(packet[2]);
            string newTitle = packet[3];
            string param = StringConstants.ChangeBlogNamePacketAnswerFailed;
            if (clientData.Id != id)
                param = StringConstants.ChangeBlogNamePacketAnswerNotOwner;
            else if (await BlogService.Instance.ChangeBlogName(clientData.Id, id, newTitle))
                param = StringConstants.ChangeBlogNamePacketAnswerOK;

            return string.Format(StringConstants.ChangeBlogNamePacketAnswerFormat, StringConstants.ChangeBlogNamePacketName, param);
        }

        private string buildLogout(string[] packet, ClientData clientData)
        {
            clientData.Id = -1;
            return StringConstants.LogoutPacketName;
        }

        private string buildDefault(string[] packet)
        {
            return StringConstants.UnrecognizedCommandAnswer;
        }

        private string buildExit(string[] packet)
        {
            return StringConstants.ConnectionClosePacketName;
        }
    }
}