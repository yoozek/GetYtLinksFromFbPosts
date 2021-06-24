using System;
using System.Collections.Generic;

namespace YtVideosFromFb
{
    public class Feed
    {
        public List<Post> Data { get; set; }
        public Paging Paging { get; set; }
    }
    
    public class Post
    {
        public string id { get; set; }
        public string link { get; set; }

        public DateTime created_time { get; set; }
    }

    public class Paging
    {
        public string previous { get; set; }
        public string next { get; set; }
    }
}