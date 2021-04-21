using Microsoft.EntityFrameworkCore;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Genre> Genres {get;set;}
        public DbSet<Movie> Movies {get;set;}
        public DbSet<MovieGenre> MovieGenres {get;set;}
        public DbSet<Occupation> Occupations {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<UserMovie> UserMovies {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=bitsql.wctc.edu;Database=jdziadulewicz_22097_movielens;User ID=jdziadulewicz;Password=000554415;");
        }
    }
}