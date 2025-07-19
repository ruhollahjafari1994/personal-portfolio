namespace PersonalPortfolio.Models
{
    public class Project
    {
        public int Id { get; set; } 
        public long GitHubId { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string GithubLink { get; set; } = string.Empty;       
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}