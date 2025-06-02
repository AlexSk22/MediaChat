using System.Text;

namespace ServerSide
{
    public class User
    {
        Client client;
        string name = "";
        public event Action<User, byte[]> OnSentMessageBytes;
        public event Action<User,string> OnSentMessageString;
        public event Action<User> OnUserDisconect;
        int counter = 0;
        public User(Client client)
        {
            this.client = client;
            SendMessage("Enter your name please!\n");
            if (client is TextClient)
            {
                client.OnMessageWrittenString += HandleInput;
            }
            else
            {
                name = counter.ToString();
                counter++;
                System.Console.WriteLine("Voice chat client:" + name);
                client.OnMessageWrittenBytes += HandleInput;
            }
            client.OnDisconect += OnDisconect;
        }
        public string Name
        {
            get { return name; }
        }
        void HandleInput(byte[] input)
        {
            OnSentMessageBytes?.Invoke(this, input);
        }
        void HandleInput(string input)
        {
            string msg = input.Trim();
            if (name == "")
            {
                name = msg.Trim(); // Add Trim() here to remove newline and spaces
                SendMessage("Welcome: " + name + "\n");
            }

            else if (msg == "")
            {
                // nothing
            }
            else
            {
                Console.WriteLine("Input by " + name + ": " + msg);
                OnSentMessageString?.Invoke(this, msg);

            }
        }
        public void SendMessage(string msg)
        {
            client.sendMessage(msg);
        }
        public void SendVoice(byte[] msg)
        {
            client.sendMessage(msg);
        }
        void OnDisconect()
        {
            client.OnMessageWrittenBytes -= HandleInput;
            client.OnMessageWrittenString -= HandleInput;
            client.OnDisconect -= OnDisconect;
            OnUserDisconect?.Invoke(this);
        }
    }
}