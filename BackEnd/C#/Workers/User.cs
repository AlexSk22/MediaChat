namespace ServerSide
{
    public class User
    {
        Client client;
        string name = "";
        public event Action<string> OnSentMessage;
        public User(Client client)
        {
            this.client = client;
            SendMessage("Enter your name please!\n");
            client.OnMessageWritten += HandleInput;
            client.OnDisconect += OnDisconect;
        }
        void HandleInput(string msg)
        {
            if (name == "")
            {
                name = msg;
                SendMessage("Welcome: " + name + "\n");
            }
            if (msg == "")
            {
                // nothing
            }
            else
            {

                System.Console.WriteLine("Input by " + name + "\n");
                OnSentMessage?.Invoke(msg);
            }
        }
        public void SendMessage(string msg)
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