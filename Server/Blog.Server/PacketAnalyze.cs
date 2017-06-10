using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Server
{
    class PacketAnalyze
    {
        string type;
        string param1;
        string param2;
        public PacketAnalyze()
        {
        }

        public string getPacketResponse(string content)
        {
            string[] packet = content.Split('\t');
            int packetSize = Convert.ToInt32(packet[0]);
            string packetType = packet[1];
            string messagee;
            int sizeM = 0;
            switch (packetType)
            {
                case "STATUS": // testowe
                    messagee = buildStatus(packet);
                    break;
                case "REGISTER":
                    messagee = buildRegister(packet);
                    break;
                case "LOGIN":
                    messagee = buildLogin(packet);
                    break;
                case "DISPLAY_BLOGS":
                    messagee = buildDisplayBlogs(packet);
                    break;
                case "DISPLAY_BLOG":
                    messagee = buildDisplayBlog(packet);
                    break;
                case "ADD_ENTRY":
                    messagee = buildAddEntry(packet);
                    break;
                case "DISPLAY_ENTRY":
                    messagee = buildDisplayEntry(packet);
                    break;
                case "DELETE_ENTRY":
                    messagee = buildDeleteEntry(packet);
                    break;
                case "CHANGE_BLOG_NAME":
                    messagee = buildChangeBlogName(packet);
                    break;
                case "THX_BYE":
                    messagee = buildLogout(packet);
                    break;
                default:
                    messagee = "";
                    break;
            }
            sizeM = 1 + messagee.Length;
            sizeM += sizeM.ToString().Length;
            content = sizeM + "\t" + messagee;
            return content;
        }

        private string buildStatus(string[] packet)
        {
            type = "OK";
            return type + "\t" + "status is ok";
        }

        private string buildRegister(string[] packet)
        {
            type = "REGISTER";
            if (true) //ok
            {
                param1 = "OK";
            }
            else
            {
                param1 = "INVALID";
            }
            return type + "\t" + param1;
        }


        private string buildLogin(string[] packet)
        {
            type = "LOGIN";
            if (true) //exist
            {
                param1 = "OK";
                param2 = "14";
            }
            else if (false) //wrong data
            {
                param1 = "FAILED";
                param2 = "INVALID";
            }
            else if (false)
            {
                param1 = "FAILED";
                param2 = "LOCKED";
            }
            return type + "\t" + param1 + "\t" + param2;
        }

        private string buildDisplayBlogs(string[] packet)
        {
            type = "DISPLAY_BLOGS";
            List<string> paramListBlog; //lista blogów
            return type;
        }
    
        private string buildDisplayBlog(string[] packet)
        {
            type = "DISPLAY_BLOG";
            if (true)// Jeżeli blog X istnieje
            {
                List<string> paramListBlogAdds; //lista wpisów w blogu
                return type;
            }
            else
            {
                param1 = "FAILED";
                return type + "\t" + param1;
            }
        }

        private string buildAddEntry(string[] packet)
        {
            type = "ADD_ENTRY";
            if (true)// Jak stworzył
            {
                param1 = "OK";
                param2 = "15";
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

        private string buildDisplayEntry(string[] packet)
        {
            type = "DISPLAY_ENTRY";
            param1 = "15";
            param2 = "Tytuł notatki Treść notatki";
            return type + "\t" + param1 + "\t" + param2;
        }

        private string buildDeleteEntry(string[] packet)
        {
            type = "DELETE_ENTRY";
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
        private string buildChangeBlogName(string[] packet)
        {
            type = "CHANGE_BLOG_NAME";
            if (true) // UDAŁO SIĘ
            {
                param1 = "OK";
            }
            else if (false) // coś się zjebało
            {
                param1 = "FAILED ";
            }
            return type + "\t" + param1;
        }


        private string buildLogout(string[] packet)
        {
            type = "THX_BYE";
            return type;
        }

    }

}
