using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    public class TextChannel : BaseThread
    {
        List<User> users;
        TcpListener tcpListener;
        public event Action<User> OnUserConnect;
        public event Action<User> OnUserDisConnect;
        public TextChannel(TcpListener tcpListener)
        {
            users = new();
            this.tcpListener = tcpListener;
        }
        public void AddUser(User user)
        {
            users.Add(user);
            user.OnSentMessageString += BroadCast;
            OnUserConnect?.Invoke(user);
        }
        public void RemoveUser(User user)
        {
            users.Remove(user);
            user.OnSentMessageString -= BroadCast;
            OnUserDisConnect?.Invoke(user);
        }
        public void BroadCast(User user, string msg)
        {
            foreach (var item in users)
            {
                if (item != user)
                {
                    string output = user.Name.Trim() + " : " + msg.Trim();
                    item.SendMessage(output); // send message to other users
                }
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
                TextClient client = new(tcpListener.AcceptTcpClient());
                client.Start();
                User user = new User(client);
                AddUser(user);
                user.OnUserDisconect += RemoveUser;
            }
        }


    }
}