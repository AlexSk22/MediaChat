using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    public class Client : BaseThread
    {
        TcpClient client;
        public event Action<string> OnMessageWritten;
        public event Action OnDisconect;
        public Client(TcpClient client)
        {
            this.client = client;
        }
        public void sendMessage(string msg)
        {
            NetworkStream stream = client.GetStream();
            stream.Write(Encoding.UTF8.GetBytes(msg));
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
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (message.Trim() == "exit")
                {
                    Disconect("User's will");
                    break;
                }

                OnMessageWritten?.Invoke(message.Trim());
                Console.WriteLine("Received: " + message);
            }
        }

        void Disconect(string reason)
        {
            System.Console.WriteLine("Client disconected: " + reason);
            client.Close();
            OnDisconect?.Invoke();
        }
    }
}