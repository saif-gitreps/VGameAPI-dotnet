﻿using Microsoft.EntityFrameworkCore;

namespace VGameAPI.Database
{
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options): DbContext(options)
    {
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(
                new VideoGame
                {
                    ID = 1,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Platform = "Nintendo Switch",
                    Developer = "Nintendo EPD",
                    Publisher = "Nintendo"
                },
                new VideoGame
                {
                    ID = 2,
                    Title = "The Witcher 3: Wild Hunt",
                    Platform = "PC",
                    Developer = "CD Projekt Red",
                    Publisher = "CD Projekt"
                },
                new VideoGame
                {
                    ID = 3,
                    Title = "God of War",
                    Platform = "PlayStation 4",
                    Developer = "Santa Monica Studio",
                    Publisher = "Sony Interactive Entertainment"
                }
                );
        }
    }

}
