using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    public class Server : BaseThread
    {
        private static TcpListener myListener;
        private static int port = 5050;
        private static IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        List<Client> users;

        public Server()
        {
            myListener = new TcpListener(localAddr, port);
            myListener.Start();
            Console.WriteLine($"Web Server Running on {localAddr.ToString()} on port {port}... Press ^C to Stop...");

            users = new();
        }
        void removeUser(Client user)
        {
            users.Remove(user);
        }
        public override void RunThread()
        {
            while (true)
            {
                Client user = new(myListener.AcceptTcpClient());
                user.Start();
                users.Add(user);
            }
        }
    }
}