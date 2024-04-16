using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostName { get; set; }

        public Post(int postId, string postName)
        {
            PostId = postId;
            PostName = postName;
        }
    }
}
