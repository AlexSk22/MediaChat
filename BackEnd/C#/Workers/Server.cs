using System.Net;
using System.Net.Sockets;

namespace ServerSide
{
    public class Server : BaseThread
    {
        private TcpListener myListener;
        private static int port = 5050; // change please :/
        private static IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        List<User> users;
        List<Room> rooms;
        public Server()
        {
            myListener = new TcpListener(localAddr, port);
            myListener.Start();
            Console.WriteLine($"Web Server Running on {localAddr.ToString()} on port {port}... Press ^C to Stop...");

            users = new();
            rooms = new();
            rooms.Add(new Room("test"));
        }
        void removeUser(User user)
        {
            users.Remove(user);
        }
        public override void RunThread()
        {
            while (true)
            {
                Client client = new(myListener.AcceptTcpClient());
                client.Start();
                User user = new User(client);
                users.Add(user);
                rooms[0].AddUser(user);
                System.Console.WriteLine("New user!");
            }
        }

        // API, return rooms
        public void getRooms()
        {
        }
    }
}