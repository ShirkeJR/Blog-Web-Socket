using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Server
{
    class PacketAnalyzeService
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
            string messagee;
            switch (packetType)
            {
                case "STATUS": // testowe
                    messagee = await buildStatus(packet);
                    break;
                case "REGISTER":
                    messagee = await buildRegister(packet);
                    break;
                case "LOGIN":
                    {
                        messagee = await buildLogin(packet);
                        if (messagee.Contains("OK"))
                        {
                            string[] paramList = messagee.Split('\t');
                            clientData.Id = Convert.ToInt32(paramList[2]);
                        }
                    }
                    break; 
                case "DISPLAY_BLOGS":
                    messagee = await buildDisplayBlogs(packet);
                    break;
                case "DISPLAY_BLOG":
                    messagee = await buildDisplayBlog(packet);
                    break;
                case "ADD_ENTRY":
                    messagee = await buildAddEntry(packet, clientData);
                    break;
                case "DISPLAY_ENTRY":
                    messagee = await buildDisplayEntry(packet);
                    break;
                case "DELETE_ENTRY":
                    messagee = await buildDeleteEntry(packet, clientData);
                    break;
                case "CHANGE_BLOG_NAME":
                    messagee = await buildChangeBlogName(packet, clientData);
                    break;
                case "THX_BYE":
                    messagee = await buildLogout(packet, clientData);
                    break;
                case "EOT":
                    messagee = await buildExit(packet);
                    break;
                default:
                    messagee = await buildDefault(packet);
                    break;
            }
            messagee = messagee + "\t";
            content = messagee.Length + "\t" + messagee + "/rn/rn/rn$$";
            return content;
        }

        private async Task<string> buildStatus(string[] packet) //testowe
        {
            string type = "OK";
            return type + "\t" + "status is ok";
        }

        private async Task<string> buildRegister(string[] packet)
        {
            string type = "REGISTER";
            string param;
            if (await AccountService.Instance.Register(packet[2], packet[3])) //ok
            {
                param = "OK";
            }
            else
            {
                param = "INVALID";
            }
            return type + "\t" + param;
        }


        private async Task<string> buildLogin(string[] packet)
        {
            string type = "LOGIN";
            string param1 = "", param2= "";
            int id;
            if ((id = await AccountService.Instance.Login(packet[2], packet[3])) > 0) //exist
            {
                param1 = "OK";
                param2 = Convert.ToString(id);
            }
            else if (id < 0) //wrong data
            {
                param1 = "FAILED";
                param2 = "INVALID";
            }
            else if (false) // może kiedyś locked
            {
                param1 = "FAILED";
                param2 = "LOCKED";
            }
            return type + "\t" + param1 + "\t" + param2;
        }

        private async Task<string> buildDisplayBlogs(string[] packet)
        {
            string type = "DISPLAY_BLOGS";
            string paramsList = await BlogService.Instance.DisplayBlogs();
            return type + "\t" + paramsList;
        }
    
        private async Task<string> buildDisplayBlog(string[] packet)
        {
            string type = "DISPLAY_BLOG";
            int id = Convert.ToInt32(packet[2]);
            if (await BlogService.Instance.BlogExists(id))// Jeżeli blog X istnieje
            {
                string paramsList = await BlogService.Instance.DisplayBlogPosts(id); //lista wpisów w blogu
                return type + "\t" + paramsList;
            }
            else
            {
                string param1 = "FAILED";
                return type + "\t" + param1;
            }
        }

        private async Task<string> buildAddEntry(string[] packet, ClientData clientData)
        {
            string type = "ADD_ENTRY";
            string param1;
            if (clientData.Id != -1)// Jak stworzył
            {
                if(await BlogService.Instance.AddEntry(clientData.Id, packet[2], packet[3]))
                {
                    param1 = "OK";
                    string param2 = Convert.ToString(await BlogService.Instance.GetEntryId(clientData.Id, packet[2]));
                    return type + "\t" + param1 + "\t" + param2;
                }
                else if (packet[2].Length > 31) // zły tytuł
                {
                    param1 = "ERR_TITLE";
                    return type + "\t" + param1;
                }
                else if (packet[3].Length > 2047) // zła treść
                {
                    param1 = "ERR_CONTENT";
                    return type + "\t" + param1;
                }
            }
            param1 = "ERR_OWNER";
            return type + "\t" + param1;
        }


        private async Task<string> buildDisplayEntry(string[] packet)
        {
            string type = "DISPLAY_ENTRY";
            int id = Convert.ToInt32(packet[2]);
            string entry = await BlogService.Instance.DisplayEntry(id); //lista wpisów w blogu
            return type + "\t" + entry;
        }

        private async Task<string> buildDeleteEntry(string[] packet, ClientData clientData)
        {
            string type = "DELETE_ENTRY";
            string param1, param2;
            int id = Convert.ToInt32(packet[2]);
            if (clientData.Id != -1) // UDAŁO SIĘ
            {
                if(await BlogService.Instance.DeleteEntry(id, clientData.Id))
                {
                    param1 = "OK";
                    param2 = Convert.ToString(id);
                }
                else
                {
                    param1 = "FAILED ";
                    param2 = "NOTEXIST";
                }
                return type + "\t" + param1 + "\t" + param2;
            }
            param1 = "FAILED";
            param2 = "NOTOWNER";
            return type + "\t" + param1 + "\t" + param2;
        }

        private async Task<string> buildChangeBlogName(string[] packet, ClientData clientData)
        {
            string type = "CHANGE_BLOG_NAME";
            int id = Convert.ToInt32(packet[2]);
            string newTitle = packet[3];
            string param = "";
            if (clientData.Id != id)
            {
                param = "NOTOWNER";
                return type + "\t" + param;
            }
            if (await BlogService.Instance.ChangeBlogName(clientData.Id, id, newTitle)) // UDAŁO SIĘ
            {
                param = "OK";
                return type + "\t" + param;
            }
            param = "FAILED";
            return type + "\t" + param;
        }


        private async Task<string> buildLogout(string[] packet, ClientData clientData)
        {
            string type = "THX_BYE";
            clientData.Id = -1;
            return type;
        }

        private async Task<string> buildDefault(string[] packet)
        {
            string type = "QUE?";
            return type;
        }

        private async Task<string> buildExit(string[] packet)
        {
            string type = "EOT";
            return type;
        }

    }

}
