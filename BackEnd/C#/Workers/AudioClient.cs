using System.Net.Sockets;

namespace ServerSide
{
    public class AudioClient : Client
    {
        int BUFFERSIZE = 10240;
        public AudioClient(TcpClient client) : base(client)
        {

        }
        public override async void RunThread()
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[BUFFERSIZE];

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, BUFFERSIZE);
                if (bytesRead == 0)
                {
                    Disconect("Client disconnected");
                    break;
                }

                byte[] received = new byte[bytesRead];
                Array.Copy(buffer, received, bytesRead);
                MessageWrittenBytes(received);
            }
        }


    }
}