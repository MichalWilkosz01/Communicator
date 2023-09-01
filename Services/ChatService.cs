namespace Communicator.Services
{
    public class ChatService
    {
        //TKey = userID
        //TValue = connectionID
        private static Dictionary<string, string> Users = new Dictionary<string, string>();

        public void AddUser(string userId, string connectionId)
        {
            Users.Add(userId, connectionId);
        }

        

        public string GetUserByConnectionId(string connectionId)
        {
            lock(Users)
            {
                return Users.Where(x => x.Value == connectionId).Select(x => x.Key).FirstOrDefault();
            }
        }

        public bool IsUserOnline(string userId)
        {
            return Users.ContainsKey(userId);
        }

        public void RemoveUserFromList(string user)
        {
            lock (Users)
            {
                if(Users.ContainsKey(user))
                {
                    Users.Remove(user);
                }
            }
        }

        public string[] GetOnlineUsers()
        {
            lock (Users)
            {
                return Users.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
            }
        }

        public string GetConnectionId(string userId)
        {
            lock (Users)
            {
                var result = Users[userId];
                return result;
            }
        }
    }
}
