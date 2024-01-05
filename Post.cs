using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialmedia
{
    public class Post
    {
        private string Content;
        private DateTime Timestamp;
        private List<User> Likes;
        Dictionary<User, List<string>> comments;
        public Post()
        {
            Likes = new List<User>();
        }
        public Post(string content, DateTime timestamp) : this()
        {
            Content = content;
            Timestamp = timestamp;
        }
        public string GetContent()
        {
            return Content;
        }
        public DateTime GetTimestamp()
        {
            return Timestamp;
        }
        public List<User> GetLikes()
        {
            return Likes;
        }
        public Dictionary<User, List<string>> Getcomments()
        {
            if (comments == null)
            {
                comments = new Dictionary<User, List<string>>();
            }
            return comments;
        }

    }
}
