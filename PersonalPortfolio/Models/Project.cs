﻿namespace PersonalPortfolio.Models
{
 
        public class Project
        {
            public int Id { get; set; }
            public string Title { get; set; } = "";
            public string Description { get; set; } = "";
            public string ImageUrl { get; set; } = "";
            public string GithubLink { get; set; } = "";
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
    }