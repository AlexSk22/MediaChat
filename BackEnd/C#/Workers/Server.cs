using System.Net;
using System.Net.Sockets;

namespace ServerSide
{
    public class Server : BaseThread
    {
        private static TcpListener myListener;
        private static int port = 5050; // change please :/
        private static IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        List<User> users;
        //List<Room> rooms;
        public Server()
        {
            myListener = new TcpListener(localAddr, port);
            myListener.Start();
            Console.WriteLine($"Web Server Running on {localAddr.ToString()} on port {port}... Press ^C to Stop...");

            users = new();
        }
        void removeUser(User user)
        {
            users.Remove(user);
        }
        public override void RunThread()
        {
            while (true)
            {
                Client user = new(myListener.AcceptTcpClient());
                user.Start();
                users.Add(new User(user));
                System.Console.WriteLine("New user!");
            }
        }
        public void getRooms()
        {
            // API
        }
    }
}