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

        public async Task<string> getPacketResponse(string content)
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
                    messagee = await buildLogin(packet);
                    break;
                case "DISPLAY_BLOGS":
                    messagee = await buildDisplayBlogs(packet);
                    break;
                case "DISPLAY_BLOG":
                    messagee = await buildDisplayBlog(packet);
                    break;
                case "ADD_ENTRY":
                    messagee = await buildAddEntry(packet);
                    break;
                case "DISPLAY_ENTRY":
                    messagee = await buildDisplayEntry(packet);
                    break;
                case "DELETE_ENTRY":
                    messagee = await buildDeleteEntry(packet);
                    break;
                case "CHANGE_BLOG_NAME":
                    messagee = await buildChangeBlogName(packet);
                    break;
                case "THX_BYE":
                    messagee = await buildLogout(packet);
                    break;
                default:
                    messagee = "";
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

        private async Task<string> buildAddEntry(string[] packet)
        {
            string type = "ADD_ENTRY";
            string param1;
            if (true)// Jak stworzył
            {
                param1 = "OK";
                string param2 = "15";
                return type + "\t" + param1 + "\t" + param2;
            }
            else if(false) // zły tytuł
            {
                param1 = "ERR_TITLE";
                return type + "\t" + param1;
            }
            else if(false) // zła treść
            {
                param1 = "ERR_CONTENT";
                return type + "\t" + param1;
            }
            else if (false) // nie ten właściciel
            {
                param1 = "ERR_OWNER";
                return type + "\t" + param1;
            }
        }

        private async Task<string> buildDisplayEntry(string[] packet)
        {
            string type = "DISPLAY_ENTRY";
            int id = Convert.ToInt32(packet[2]);
            string entry = await BlogService.Instance.DisplayEntry(id); //lista wpisów w blogu
            return type + "\t" + entry;
        }

        private async Task<string> buildDeleteEntry(string[] packet)
        {
            string type = "DELETE_ENTRY";
            string param1, param2;
            if (true) // UDAŁO SIĘ
            {
                param1 = "OK";
                param2 = "15";
            }
            else if(false) // coś się zjebało
            {
                param1 = "FAILED ";
                param2 = "NOTEXIST";
            }
            else if (false) // ty nie owner
            {
                param1 = "FAILED";
                param2 = "NOTOWNER";
            }
            return type + "\t" + param1 + "\t" + param2;
        }

        private async Task<string> buildChangeBlogName(string[] packet)
        {
            string type = "CHANGE_BLOG_NAME";
            int id = Convert.ToInt32(packet[2]);
            string newTitle = packet[3];
            string param = "";
            if (await AccountService.Instance.ChangeBlogName(id, id, newTitle)) // UDAŁO SIĘ
            {
                param = "OK";
            }
            else // coś się zjebało
            {
                param = "FAILED";
            }
            return type + "\t" + param;
        }


        private async Task<string> buildLogout(string[] packet)
        {
            string type = "THX_BYE";
            return type;
        }

    }

}
