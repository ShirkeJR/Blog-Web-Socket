namespace Blog.Client
{
    public class User
    {
        public uint ID { set; get; }
        public string Login { set; get; }
        
        public User(string login, uint id)
        {
            Login = login;
            ID = id;
        }
    }
}
