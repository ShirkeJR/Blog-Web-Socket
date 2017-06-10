using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Client
{
    public class User
    {
        public string UserLogin { set; get; }
        public uint UserID { set; get; }

        public User(string login, uint id)
        {
            UserLogin = login;
            UserID = id;
        }
    }
}
