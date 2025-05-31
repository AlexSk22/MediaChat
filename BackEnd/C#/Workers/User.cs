using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    public class Client : BaseThread
    {
        TcpClient client;
        public Client(TcpClient client)
        {
            this.client = client;
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
                    System.Console.WriteLine("Client disconected, no connection");
                    break;
                }
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: " + message);

                if (message.Trim() == "exit")
                {
                    System.Console.WriteLine("Client disconected");
                    stream.Write(Encoding.UTF8.GetBytes("bye!\n"));
                    client.Close();
                    break;
                }

                stream.Write(Encoding.UTF8.GetBytes("hi!\n"));
            }
        }

        public void Disconect()
        {

        }
    }
}