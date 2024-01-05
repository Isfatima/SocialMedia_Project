using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace socialmedia
{
    public class User
    {
        private string Username;
        private string Password;
        private List<Post> Posts;
        private List<User> Request;
        private List<User> Friends;

        public User()
        {

            Friends = new List<User>();
            Request = new List<User>();
            Posts = new List<Post>();
        }
        public User(string username) : this()
        {
            Username = username;
        }
        public User(string username, string password) : this()
        {
            Username = username;
            Password = password;
        }
        public string GetPassword()
        {
            return Password;
        }
        public string GetUsername()
        {
            return Username;
        }
        public List<Post> GetPosts()
        {
            return Posts;
        }
        public List<User> GetFriends()
        {
            return Friends;
        }
        public List<User> GetRequest()
        {
            return Request;
        }


    }
}
