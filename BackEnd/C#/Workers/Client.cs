using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    abstract public class Client : BaseThread
    {
        protected TcpClient client;
        public event Action<byte[]> OnMessageWrittenBytes;
        public event Action<string> OnMessageWrittenString;
        public event Action OnDisconect;
        public Client(TcpClient client)
        {
            this.client = client;
        }
        public void sendMessage(string msg)
        {
            NetworkStream stream = client.GetStream();
            byte[] bytes = Encoding.UTF8.GetBytes(msg);
            stream.Write(bytes, 0, bytes.Length);
        }
        public void sendMessage(byte[] msg)
        {
            NetworkStream stream = client.GetStream();
            stream.Write(msg, 0, msg.Length);
        }

        protected void Disconect(string reason)
        {
            System.Console.WriteLine("Client disconected: " + reason);
            client.Close();
            OnDisconect?.Invoke();
        }
        protected void MessageWrittenBytes(byte[] msg)
        {
            OnMessageWrittenBytes?.Invoke(msg);
        }
        protected void MessageWrittenString(string msg)
        {
            OnMessageWrittenString?.Invoke(msg);
        }
    }
}