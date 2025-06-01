using System.Net;
using System.Net.Sockets;

namespace ServerSide
{
    public class Room : BaseThread
    {
        List<User> users;
        private TcpListener myListener;

        private static IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        public string Name { get; private set; }
        public int VoicePort { get; private set; }
        public int TextPort { get; private set; }
        public Room(string name, int textport, int voiceport)
        {
            System.Console.WriteLine($"{Name} room was opened. TextPort -  {TextPort}. VoicePort - {VoicePort} ");
            this.Name = name;
            this.TextPort = textport;
            this.VoicePort = voiceport;
            users = new();


            myListener = new TcpListener(localAddr, TextPort);
            myListener.Start();
        }

        public void AddUser(User user)
        {
            users.Add(user);
            user.OnSentMessage += BroadCastMessage;
        }
        public void RemoveUser(User user)
        {
            users.Remove(user);
            user.OnSentMessage -= BroadCastMessage;
        }
        public void BroadCastMessage(User user, string msg)
        {
            foreach (var item in users)
            {
                if (item != user)
                {
                    item.SendMessage($"[{Name}] {user.Name} : {msg}");
                }
            }
        }
        public void BroadCastVoice(User user, string msg)
        {
            foreach (var item in users)
            {
                if (item != user)
                {
                    item.SendMessage($"[{Name}] {user.Name} : {msg}");
                }
            }
        }
        public override void RunThread()
        {
            while (true)
            {
                Client client = new(myListener.AcceptTcpClient());
                client.Start();
                User user = new User(client);
                users.Add(user);
                System.Console.WriteLine("New user!");
            }
        }
    }
}