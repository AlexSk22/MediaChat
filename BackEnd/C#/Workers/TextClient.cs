using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    public class TextClient : Client 
    {
        public TextClient(TcpClient client) : base(client)
        {

        }
        public override async void RunThread()
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[10240];
            while (true)
            {

                int bytesRead;
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Disconect("No connection");
                    break;
                }

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                if (message == "exit")
                {
                    Disconect("User's will");
                    break;
                }

                MessageWrittenString(message);
                if (message != "")
                {
                    Console.WriteLine("Received: " + message);
                }
            }
        }

    }
}