namespace ServerSide
{
    public class Room
    {
        List<User> users;
        public string Name { get; private set; }
        public int Port { get; private set; }
        public Room(string name,int port)
        {
            this.Name = name;
            this.Port = port;
            users = new();
        }
        
        public void AddUser(User user)
        {
            users.Add(user);
            user.OnSentMessage += BroadCast;
        }
        public void RemoveUser(User user)
        {
            users.Remove(user);
            user.OnSentMessage -= BroadCast;
        }
        public void BroadCast(User user, string msg)
        {
            foreach (var item in users)
            {
                if (item != user)
                {
                    item.SendMessage($"[{Name}] {user.Name} : {msg}");
                }
            }
        }
    }
}