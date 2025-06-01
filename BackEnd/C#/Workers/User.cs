namespace ServerSide
{
    public class User
    {
        Client client;
        string name = "";
        public event Action<User,string> OnSentMessage;
        public User(Client client)
        {
            this.client = client;
            SendMessage("Enter your name please!\n");
            client.OnMessageWritten += HandleInput;
            client.OnDisconect += OnDisconect;
        }
        public string Name
        {
            get { return name; }
        }
        void HandleInput(string msg)
        {
            if (name == "")
            {
                name = msg;
                SendMessage("Welcome: " + name + "\n");
            }
            else if (msg == "")
            {
                // nothing
            }
            else
            {

                System.Console.WriteLine("Input by " + name + "\n");
                OnSentMessage?.Invoke(this,msg);
            }
        }
        public void SendMessage(string msg)
        {
            client.sendMessage(msg);
        }
        public void SendVoice(string msg)
        {
            client.sendMessage(msg);
        }
        void OnDisconect()
        {
            client.OnMessageWritten -= HandleInput;
            client.OnDisconect -= OnDisconect;
        }
    }
}