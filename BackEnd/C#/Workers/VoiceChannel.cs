using System.Net.Sockets;

namespace ServerSide
{
    public class VoiceChannel : BaseThread
    {
        List<User> users;
        TcpListener tcpListener;
        public event Action<User> OnUserConnect;
        public event Action<User> OnUserDisConnect;
        public VoiceChannel(TcpListener tcpListener)
        {
            users = new();
            this.tcpListener = tcpListener;
            tcpListener.Start();
        }
        public void AddUser(User user)
        {
            users.Add(user);
            user.OnSentMessageBytes += BroadCast;
            OnUserConnect?.Invoke(user);
        }
        public void RemoveUser(User user)
        {
            users.Remove(user);
            user.OnSentMessageBytes -= BroadCast;
            OnUserDisConnect?.Invoke(user);
        }
        public void BroadCast(User user, byte[] msg)
        {
            foreach (var item in users)
            {
                item.SendVoice(msg);
            }
        }
        public override void RunThread()
        {
            if (!tcpListener.Server.IsBound)
            {
                tcpListener.Start();
            }

            while (true)
            {
                AudioClient client = new(tcpListener.AcceptTcpClient());
                client.Start();
                User user = new User(client);
                AddUser(user);
                user.OnUserDisconect += RemoveUser;
            }
        }

    }
}