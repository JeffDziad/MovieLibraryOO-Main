using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MovieLibraryOO.DataModels;
using MovieLibraryOO.Context;
using System.Linq;

namespace MovieLibraryOO
{
    public static class DataManipulator
    {
        public static void movieSearch()
        {
            Console.WriteLine(@"█▀ █▀▀ ▄▀█ █▀█ █▀▀ █░█");
            Console.WriteLine(@"▄█ ██▄ █▀█ █▀▄ █▄▄ █▀█");
            Console.WriteLine();
            Movie display = findMovie();
            if(display == null)
            {
                Console.Clear();
                Logging.logX("Exiting...");
            }
        }

        public static void addMovie()
        {
            Console.WriteLine(@"▄▀█ █▀▄ █▀▄");
            Console.WriteLine(@"█▀█ █▄▀ █▄▀");
            Console.WriteLine();
            string title = grabTitle();
            if (title != null)
            {
                DateTime releaseDate = grabReleaseDate();
                var newMovie = new Movie()
                {
                    Title = title,
                    ReleaseDate = releaseDate
                };
                using (var db = new MovieContext())
                {
                    db.Movies.Add(newMovie);
                    db.SaveChanges();
                }
                Console.Clear();
                Console.WriteLine($"Added ['{newMovie.Title}' - ({newMovie.ReleaseDate})] to the Movies table!");
            }
            
           
        }

        public static string grabTitle()
        {
            Console.Write("Movie Title (leave empty to exit): ");
            string movieTitle = Console.ReadLine();
            if(movieTitle == "")
            {
                Console.Clear();
                return null;
            }
            return movieTitle;
        }

        public static DateTime grabReleaseDate()
        {
            int year = 0001;
            int month = 01;
            int day = 01;
            int hour = 00;
            int min = 00;
            int seconds = 00;
            Console.Write("Release Year <YYYY>: ");
            try
            {
                year = Convert.ToInt32(Console.ReadLine());
            }catch(Exception ex)
            {
                Console.Clear();
                Logging.log("Please enter a valid year...", ex);
                return grabReleaseDate();
            }
            Console.Write("Release Month <MM>: ");
            try
            {
                month = Convert.ToInt32(Console.ReadLine());
            }catch(Exception ex)
            {
                Console.Clear();
                Logging.log("Please enter a valid month...", ex);
                return grabReleaseDate();
            }
            Console.Write("Release Day <DD>: ");
            try
            {
                day = Convert.ToInt32(Console.ReadLine());
            }catch(Exception ex)
            {
                Console.Clear();
                Logging.log("Please enter a valid day...", ex);
                return grabReleaseDate();
            }
            Console.Write("Release Hour <HH:00:00>: ");
            try
            {
                hour = Convert.ToInt32(Console.ReadLine());
            }catch(Exception ex)
            {
                Console.Clear();
                Logging.log("Please enter a valid hour...", ex);
                return grabReleaseDate();
            }
            Console.Write("Release Minute <00:MM:00>: ");
            try
            {
                min = Convert.ToInt32(Console.ReadLine());
            }catch(Exception ex)
            {
                Console.Clear();
                Logging.log("Please enter a valid min...", ex);
                return grabReleaseDate();
            }
            Console.Write("Release Seconds <00:00:SS>: ");
            try
            {
                seconds = Convert.ToInt32(Console.ReadLine());
            }catch(Exception ex)
            {
                Console.Clear();
                Logging.log("Please enter a valid seconds...", ex);
                return grabReleaseDate();
            }
        
            return new DateTime(year, month, day, hour, min, seconds);
        }

        public static void updateMovie()
        {
            Console.WriteLine(@"█░█ █▀█ █▀▄ ▄▀█ ▀█▀ █▀▀");
            Console.WriteLine(@"█▄█ █▀▀ █▄▀ █▀█ ░█░ ██▄");
            Console.WriteLine();
            Movie updateMovie = findMovie();
            if(updateMovie != null)
            {
                Console.WriteLine($"Editing: [{updateMovie.Id}] - '{updateMovie.Title}' - ({updateMovie.ReleaseDate})");
                Console.WriteLine("---New Movie Details---");
                string newTitle = grabTitle();
                if(!(String.IsNullOrEmpty(newTitle)))
                {
                    DateTime newReleaseDate = grabReleaseDate();
                    Console.Write($"'{updateMovie.Title}' - ({updateMovie.ReleaseDate}) -> '{newTitle}' - ({newReleaseDate}) [Write Changes? (Yes/No)]: ");
                    string input = Console.ReadLine();
                    if(input.ToUpper().Contains("Y"))
                    {
                        using(var db = new MovieContext())
                        {
                            var query = db.Movies.SingleOrDefault(x=>x.Id == updateMovie.Id);
                            if(query != null)
                            {
                                query.Title = newTitle;
                                query.ReleaseDate = newReleaseDate;
                                db.SaveChanges();
                            }
                        }
                    }
                    Console.Clear();
                }
            }
        }

        public static Movie findMovie()
        {
            Console.WriteLine("Search by:");
            Console.WriteLine("1. ID (More Accurate)");
            Console.WriteLine("2. Keyword Search (Less Specific)");
            Console.WriteLine("3. Print all Movies (May take awhile)\n"); //Added print all movies function...
            Console.Write("Menu Option (0 to exit): ");
            string input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    return findUpdateMovieById();
                case "2":
                    return findUpdateMovieByKeyword();
                case "3":
                    //Added this method - prints all movies... | Possibly fix this method by not returning a movie.
                    printAllMovies();
                    Console.Write("Press any key to exit...");
                    Console.Read();
                    return null;
                case "0":
                    Console.Clear();
                    return null;
                default:
                    Console.Clear();
                    Logging.logX($"'{input}' is not a valid menu option, exiting...");
                    updateMovie();
                    break;
            }
            return null;
        }

        public static void printAllMovies() 
        {
            using (var db = new MovieContext()) 
            {
                Console.WriteLine("Printing Movies...");
                var query = db.Movies.ToList();
                foreach(Movie movie in query) 
                {
                    Console.WriteLine($"[{movie.Id}] - '{movie.Title}' - ({movie.ReleaseDate})");
                }
                
            }
        }

        public static Movie findUpdateMovieById()
        {
            int ID = 00;
            Movie output = null;
            Console.Write("Enter ID # (ENTER to exit): ");
            string input = Console.ReadLine();
            if (!(String.IsNullOrEmpty(input)))
            {
                try
                {
                    ID = Convert.ToInt32(input);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Logging.log("Not a valid Id, exiting...", ex);
                    updateMovie();
                }
                using (var db = new MovieContext())
                {
                    Console.WriteLine("Searching...");
                    var query = db.Movies.Where(x => x.Id == ID).FirstOrDefault();
                    if (query != null)
                    {
                        Console.WriteLine("[ID] - 'TITLE' - (RELEASE DATE)");
                        Console.WriteLine($"[{query.Id}] - '{query.Title}' - ({query.ReleaseDate})");
                        Console.Write("Continue? (Yes/No): ");
                        string inputChoice = Console.ReadLine();
                        if (inputChoice.ToUpper() == "YES" || inputChoice.ToUpper() == "Y")
                        {
                            output = new Movie()
                            {
                                Id = query.Id,
                                Title = query.Title,
                                ReleaseDate = query.ReleaseDate
                            };
                        }
                        else
                        {
                            Logging.logX("Try again...");
                            return findUpdateMovieById();
                        }
                    }
                    else
                    {
                        Logging.logX("Could not find movie...");
                        return findUpdateMovieById();
                    }
                }
            }
            Console.Clear();
            return output;
        }

        public static Movie findUpdateMovieByKeyword()
        {
            string keyword = "Toy Story";
            Movie output = null;
            Console.Write("Enter Keyword (ENTER to exit): ");
            keyword = Console.ReadLine();
            if (!(keyword == null || keyword == ""))
            {
                using (var db = new MovieContext())
                {
                    Console.WriteLine("Searching...");
                    var query = db.Movies.Where(x => x.Title.Contains(keyword)).FirstOrDefault();
                    if (query != null)
                    {
                        Console.WriteLine("ID - TITLE - RELEASE DATE");
                        Console.WriteLine($"{query.Id} - {query.Title} - {query.ReleaseDate}");
                        Console.Write("Continue? (Yes/No): ");
                        string input = Console.ReadLine();
                        if (input.ToUpper() == "YES" || input.ToUpper() == "Y")
                        {
                            output = new Movie()
                            {
                                Id = query.Id,
                                Title = query.Title,
                                ReleaseDate = query.ReleaseDate
                            };
                        }
                        else
                        {
                            Logging.logX("Try again...");
                            return findUpdateMovieByKeyword();
                        }
                    }
                    else
                    {
                        Logging.logX("Could not find movie...");
                        return findUpdateMovieByKeyword();
                    }
                }
            }
            Console.Clear();
            return output;
        }

        public static void deleteMovie()
        {
            Console.WriteLine(@"█▀▄ █▀▀ █░░ █▀▀ ▀█▀ █▀▀");
            Console.WriteLine(@"█▄▀ ██▄ █▄▄ ██▄ ░█░ ██▄");
            Console.WriteLine();
            Movie deleteMovie = findMovie();
            using(var db = new MovieContext())
            {
                var query = db.Movies.Where(x=>x.Id == deleteMovie.Id).First();
                Console.Write($"[{query.Id}] - '{query.Title}' - ({query.ReleaseDate}) Delete? (Yes/No): ");
                string input = Console.ReadLine();
                if(input.ToUpper().Contains("Y"))
                {
                    db.Movies.Remove(query);
                    db.SaveChanges();
                    Console.Clear();
                    Logging.logX("Record Deleted Successfully...");
                }else
                {
                    Logging.logX("Exiting...");
                }
            }
        }

        


        


    }
}