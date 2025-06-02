using System.Net;
using System.Net.Sockets;

namespace ServerSide
{
    public class Server
    {
        List<Room> rooms;
        public Server()
        {
            rooms = new();
            rooms.Add(new Room("Hi room", FreeTcpPort(), FreeTcpPort()));
            rooms.Add(new Room("Meeting room", FreeTcpPort(), FreeTcpPort()));
            rooms.Add(new Room("Onboard room", FreeTcpPort(), FreeTcpPort()));

        }
        public static int FreeTcpPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
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