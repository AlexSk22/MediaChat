namespace ServerSide
{
    public class User
    {
        Client client;
        string name = null;
        public event Action<string> OnSentMessage;
        public User(Client client)
        {
            this.client = client;
            SendMessage("Give your name please!");
            client.OnMessageWritten += HandleInput;
            client.OnDisconect += OnDisconect;
        }
        void HandleInput(string msg)
        {
            if (name == null)
            {
                name = msg;
                SendMessage("Welcome: " + name);
            }
            else
            {
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