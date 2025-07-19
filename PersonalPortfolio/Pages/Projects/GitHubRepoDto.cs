namespace PersonalPortfolio.Pages.Projects
{
    public partial class IndexModel
    {
        public class GitHubRepoDto
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string HtmlUrl { get; set; } = "";
            public DateTime CreatedAt { get; set; }
        }
    }
}
