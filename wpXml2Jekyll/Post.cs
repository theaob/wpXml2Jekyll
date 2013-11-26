using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpXml2Jekyll
{
    public class Post
    {
        public String title;
        public DateTime date;
        public String content;
        public String url;
        public String author;

        public Post(String postTitle, String postDate, String postContent, String postURL, String postAuthor)
        {
            title = postTitle;
            date = DateTime.Parse(postDate);
            content = postContent;
            url = postURL;
            author = postAuthor;
        }
    }
}
