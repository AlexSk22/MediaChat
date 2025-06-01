using System.Net;
using System.Net.Sockets;

namespace ServerSide
{
    public class Server 
    {
        private TcpListener myListener;
        private static int port = FreeTcpPort();

        List<Room> rooms;
        public Server()
        {
            rooms = new();
            rooms.Add(new Room("Hi room", FreeTcpPort(), FreeTcpPort()));
            rooms.Add(new Room("Meeting room", FreeTcpPort(), FreeTcpPort()));
            rooms.Add(new Room("Onboard room", FreeTcpPort(), FreeTcpPort()));

            foreach (var item in rooms)
            {
                item.Start(); 
            }
        }
        static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
        // API, return rooms
        // return JSON
        public List<Room> GetRooms()
        {
            return rooms;
        }
    }
}