namespace Milestone.Core.Models
{
    public class Issue : GitHubItem
    {
        public int Url { get; set; }
        public string State { get; set; }
    }
}

