namespace ServerSide
{
    public class Room
    {
        List<User> users;
        string name;

        public Room(string name)
        {
            this.name = name;
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
        public void BroadCast(User user,string msg)
        {
            foreach (var item in users)
            {
                if (item != user)
                {
                    item.SendMessage($"[{name}] {user.Name} : {msg}");
                }
            }
        }
    }   
}