using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Data;
using PersonalPortfolio.Models;
using System.Net.Http;
using System.Text.Json;

namespace PersonalPortfolio.Pages.Projects
{
    public partial class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public List<Project> Projects { get; set; } = new();

        public async Task OnGetAsync()
        {                                                 
            Projects = await _context.Projects
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            if (Projects.Any())
                return;

            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/users/ruhollahjafari1994/repos");
            request.Headers.UserAgent.ParseAdd("Mozilla/5.0");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {                    
                return;
            }

            var json = await response.Content.ReadAsStringAsync();

            var githubRepos = JsonSerializer.Deserialize<List<GitHubRepoDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var newProjects = githubRepos!.Select(repo => new Project
            {
                Title = repo.Name,
                Description = repo.Description,
                ImageUrl = repo.HtmlUrl,
                CreatedAt = repo.CreatedAt
            }).ToList();

            _context.Projects.AddRange(newProjects);
            await _context.SaveChangesAsync();

            Projects = newProjects;
        }
    }
}
