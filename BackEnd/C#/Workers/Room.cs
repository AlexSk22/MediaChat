using System.Net;
using System.Net.Sockets;

namespace ServerSide
{
    public class Room 
    {
        List<User> users;
        private VoiceChannel voiceChannel;
        private TextChannel textChannel;
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

            voiceChannel = new(new TcpListener(localAddr, voiceport));
            textChannel = new(new TcpListener(localAddr, textport));


            voiceChannel.Start();
            textChannel.Start();
        }

    }
}