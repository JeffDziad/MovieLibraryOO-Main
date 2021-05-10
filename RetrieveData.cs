using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MovieLibraryOO.DataModels;
using MovieLibraryOO.Context;
using System.Linq;

namespace MovieLibraryOO
{
    public class RetrieveData : IRetrieve
    {
        private static IUpdate updater = DependencyInjection.getIUpdate();

        public void movieSearch()
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

        public Movie findMovie()
        {

            Console.WriteLine("Search Movie by:");
            Console.WriteLine("1. ID (More Accurate)");
            Console.WriteLine("2. Keyword Search (Less Specific)");
            Console.Write("Menu Option (0 to exit): ");
            string input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    return findUpdateMovieById();
                case "2":
                    return findUpdateMovieByKeyword();
                case "0":
                    Console.Clear();
                    return null;
                default:
                    Console.Clear();
                    Logging.logX($"'{input}' is not a valid menu option, exiting...");
                    return findMovie();
            }
        }

        public void printAllMovies() 
        {
            using (var db = new MovieContext()) 
            {
                Console.WriteLine("Printing Movies...");
                var query = db.Movies.ToList();
                int count = 0;
                foreach(Movie movie in query) 
                {
                    Console.WriteLine($"[{movie.Id}] - '{movie.Title}' - ({movie.ReleaseDate})");
                    if(count == 999)
                    {
                        Console.Write("Press ENTER to continue printing...");
                        string temp = Console.ReadLine();
                        count = 0;
                    }
                    count++;
                }
            }
        }

        public Movie findUpdateMovieById()
        {
            int ID = 00;
            Movie output = null;
            Console.Write("Enter ID #: ");
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
                }
                using (var db = new MovieContext())
                {
                    Console.WriteLine("Searching...");
                    var query = db.Movies.Where(x => x.Id == ID).FirstOrDefault();
                    if (query != null)
                    {
                        Console.WriteLine("[ID] - 'TITLE' - (RELEASE DATE)");
                        Console.WriteLine($"[{query.Id}] - '{query.Title}' - ({query.ReleaseDate})");
                        Console.Write("Search Again? (Yes/No): ");
                        string inputChoice = Console.ReadLine();
                        if (inputChoice.ToUpper().ToCharArray()[0] == 'N')
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
            Console.WriteLine();
            //Console.Clear();
            return output;
        }

        public Movie findUpdateMovieByKeyword()
        {
            string keyword = "Toy Story";
            Movie output = null;
            Console.Write("Enter Keyword: ");
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
                        Console.Write("Search Again? (Yes/No): ");
                        string input = Console.ReadLine();
                        if (input.ToUpper().ToCharArray()[0] == 'N')
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
            //Console.Clear();
            Console.WriteLine();
            return output;
        }

        public User findUser()
        {
            User foundUser;
            Console.Write("Enter Existing UserID: ");
            string inputStr = Console.ReadLine();
            int inputInt = 0;
            if(inputStr == null || inputStr == " ")
            {
                Console.Clear();
                Logging.logX("UserID cannot be emtpy...");
                return findUser();
            }else 
            {
                try
                {
                    inputInt = Convert.ToInt32(inputStr);
                }catch(Exception e)
                {
                    Console.Clear();
                    Logging.log("Please enter a valid UserID...", e);
                    return findUser();
                }
                using(var db = new MovieContext())
                {
                    var query = db.Users.Where(x => x.Id == inputInt).FirstOrDefault();
                    foundUser = new User()
                    {
                        Id = query.Id,
                        Age = query.Age,
                        Gender = query.Gender,
                        ZipCode = query.ZipCode,
                        Occupation = query.Occupation,
                        UserMovies = query.UserMovies
                    };
                    return foundUser;
                }
            }
        }

        public List<Occupation> getOccupations(long occupationID)
        {
            List<Occupation> occupationOutput = new List<Occupation>();
            if(occupationID == -1)
            {
                using(var db = new MovieContext())
                {
                    occupationOutput = db.Occupations.ToList();
                }
                return occupationOutput;
            }else
            {
                using(var db = new MovieContext())
                {
                    occupationOutput.Add(db.Occupations.Where(x => x.Id == occupationID).FirstOrDefault());
                }
                return occupationOutput;
            }
        }
    }
}