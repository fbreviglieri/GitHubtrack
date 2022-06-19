using System.Collections.Generic;

namespace Swap.GithubTracker.Domain.Model
{
    public class Issue
    {
        public Issue()
        {
        }
        public Issue(string title, string author, List<string> labels)
        {
            this.Author = author;
            this.Labels = labels;
            this.Title = title;
        }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public List<string> Labels { get; private set; }
    }
}
