using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    public class AudioClient : Client 
    {
        public AudioClient(TcpClient client) : base(client)
        {

        }
        public override async void RunThread()
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            while (true)
            {

                int bytesRead;
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Disconect("No connection");
                    break;
                }

                MessageWrittenBytes(buffer);
            }
        }

    }
}