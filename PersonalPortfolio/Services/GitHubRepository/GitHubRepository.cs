using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Data;
using PersonalPortfolio.Dto.GitHubRepo;
using PersonalPortfolio.Models;
using System.Text.Json;

namespace PersonalPortfolio.Services.GitHubRepository
{
    public class GitHubRepository
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private const string GitHubUsername = "ruhollahjafari1994";

        public GitHubRepository(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<List<Project>> GetOrSyncProjectsAsync()
        {
            // بررسی وجود داده در دیتابیس
            var existingProjects = await _context.Projects.ToListAsync();
            if (existingProjects.Any())
                return existingProjects;

            // اگر دیتابیس خالی بود، داده‌ها از گیتهاب گرفته بشن
            var apiUrl = $"https://api.github.com/users/{GitHubUsername}/repos";

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.UserAgent.ParseAdd("Mozilla/5.0"); // GitHub API requires User-Agent

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var githubRepos = JsonSerializer.Deserialize<List<GitHubRepoDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var projects = githubRepos.Select(repo => new Project
            {
                Title = repo.Name,
                Description = repo.Description,
                ImageUrl = repo.HtmlUrl,
                CreatedAt = repo.CreatedAt
            }).ToList();

            // ذخیره در دیتابیس
            _context.Projects.AddRange(projects);
            await _context.SaveChangesAsync();

            return projects;
        }
    }

}
