using System;
using MovieLibraryOO.Context;
using MovieLibraryOO.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibraryOO 
{
    public class UpdateData : IUpdate
    {
        private static IRetrieve retriever = DependencyInjection.getIRetrieve();

        public void addMovie()
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

        public string grabTitle()
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

        public DateTime grabReleaseDate()
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

        public void updateMovie()
        {
            Console.WriteLine(@"█░█ █▀█ █▀▄ ▄▀█ ▀█▀ █▀▀");
            Console.WriteLine(@"█▄█ █▀▀ █▄▀ █▀█ ░█░ ██▄");
            Console.WriteLine();
            Movie updateMovie = retriever.findMovie();
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

        public void deleteMovie()
        {
            Console.WriteLine(@"█▀▄ █▀▀ █░░ █▀▀ ▀█▀ █▀▀");
            Console.WriteLine(@"█▄▀ ██▄ █▄▄ ██▄ ░█░ ██▄");
            Console.WriteLine();
            Movie deleteMovie = retriever.findMovie();
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

        public void rateMovie() 
        {
            Movie ratingMovie;
            User ratingUser;
            Console.WriteLine(@"█▀█ ▄▀█ ▀█▀ █▀▀   █▀▄▀█ █▀█ █░█ █ █▀▀");
            Console.WriteLine(@"█▀▄ █▀█ ░█░ ██▄   █░▀░█ █▄█ ▀▄▀ █ ██▄");
            Console.WriteLine();
            Console.WriteLine("---Select Movie---\n");
            ratingMovie = retriever.findMovie();
            Console.WriteLine("---Select User----");
            Console.Write("Do you have a UserId? (Yes/No): ");
            string makeNewUser = Console.ReadLine();
            char[] makeNewUserArr = makeNewUser.ToUpper().ToCharArray();
            if(makeNewUserArr[0] == 'Y')
            {
                ratingUser = retriever.findUser();
            }else
            {
                Console.Clear();
                Logging.logX("Please navigate to the 'Add User' option in the 'Data Menu' to obtain a UserId...");
                return;
            }
            using(var db = new MovieContext())
            {

                Console.WriteLine($"[{ratingUser.Id}] - [{ratingUser.Age}] - [{ratingUser.Gender}] - [{ratingUser.ZipCode}]");
                Console.WriteLine("Is rating...");
                Console.WriteLine($"[{ratingMovie.Id}] - '{ratingMovie.Title}' - ({ratingMovie.ReleaseDate})");
                UserMovie match = db.UserMovies.Where(x => x.User.Id == ratingUser.Id && x.Movie.Id == ratingMovie.Id).SingleOrDefault();
                if(match != null)
                {
                    Console.WriteLine("\n*Rating Match Found*\n*Please enter new Rating*\n");
                }
                Console.Write("Rating: ");
                string rating = Console.ReadLine();
                long ratingLong = 1;
                try
                {
                    ratingLong = long.Parse(rating);
                }catch(Exception e)
                {
                    Console.Clear();
                    Logging.log("Please enter a number between 1 and 5.", e);
                    rateMovie(); 
                }
                if(ratingLong >= 1 && ratingLong <= 5)
                {
                    if(match == null)
                    {
                        UserMovie newRating = new UserMovie() 
                        {
                            Rating = ratingLong,
                            RatedAt = DateTime.Now,
                            User = db.Users.Where(x => x.Id == ratingUser.Id).SingleOrDefault(),
                            Movie = db.Movies.Where(x => x.Id == ratingMovie.Id).SingleOrDefault()
                        };
                        db.UserMovies.Add(newRating);
                        db.SaveChanges();
                        Console.Clear();
                        Logging.logX($"Added rating {ratingLong}, to '{ratingMovie.Title}'...");
                        return;
                    }else
                    {
                        var userMovie = db.UserMovies.Where(x => x.Id == match.Id).SingleOrDefault();
                        userMovie.Rating = ratingLong;
                        db.SaveChanges();
                        Console.Clear();
                        Logging.logX($"Updated rating to {ratingLong}, for '{ratingMovie.Title}'...");
                        return;
                    }
                }
            }
        }

        public void addUser() 
        {
            Console.WriteLine(@"▄▀█ █▀▄ █▀▄   █░█ █▀ █▀▀ █▀█");
            Console.WriteLine(@"█▀█ █▄▀ █▄▀   █▄█ ▄█ ██▄ █▀▄");
            Console.WriteLine();
            Console.Write("Enter Age: ");
            string ageStr = Console.ReadLine();
            long age = 0;
            try
            {
                age = long.Parse(ageStr);
            }catch(Exception e)
            {
                Console.Clear();
                Logging.log($"'{ageStr}', is not a valid age...", e);
                return;
            }
            Console.Write("Gender (M/F): ");
            string genderStr = Console.ReadLine();
            char[] genderArr = genderStr.ToUpper().ToCharArray();
            if(genderArr[0] != 'F' && genderArr[0] != 'M')
            {
                Console.Clear();
                Logging.logX($"'{genderStr}', is not a valid Gender...");
                return;
            }
            Console.Write("ZipCode: ");
            string zipCodeStr = Console.ReadLine();
            try
            {
                int temp = Convert.ToInt32(zipCodeStr);
            }catch(Exception e)
            {
                Console.Clear();
                Logging.log("'{zipCodeStr}', is not a valid ZipCode...", e);
                return;
            }
            List<Occupation> occupations = retriever.getOccupations(-1);
            foreach(Occupation occupation in occupations)
            {
                Console.WriteLine($"{occupation.Id}. {occupation.Name}");
            }
            Console.Write("Occupation ID: ");
            string occupationChoiceStr = Console.ReadLine();
            int occupationChoiceInt = 1;
            try
            {
                occupationChoiceInt = Convert.ToInt32(occupationChoiceStr);
            }catch(Exception e)
            {
                Console.Clear();
                Logging.log($"'{occupationChoiceStr}', is not a valid occupation ID...", e);
                return;
            }
            List<UserMovie> newUserMovies = new List<UserMovie>();
            User newUser = new User();
            newUser.Age = age;
            newUser.Gender = genderStr;
            newUser.ZipCode = zipCodeStr;
            using(var db = new MovieContext())
            {
                newUser.Occupation = db.Occupations.Where(x => x.Id == occupationChoiceInt).SingleOrDefault();
                db.Users.Add(newUser);
                var maxId = db.Users.Max(table => table.Id);
                Console.Clear();
                Console.WriteLine($"Added User: [{newUser.Age}] - [{newUser.Gender}] - [{newUser.ZipCode}] - [{newUser.Occupation.Name}]");
                Console.WriteLine($"*[{maxId + 1}] - Your UserId for rating movies.*");
                db.SaveChanges();
            }
        }

        public void addOccupation()
        {
            Console.WriteLine(@"▄▀█ █▀▄ █▀▄   █▀█ █▀▀ █▀▀ █░█ █▀█ ▄▀█ ▀█▀ █ █▀█ █▄░█");
            Console.WriteLine(@"█▀█ █▄▀ █▄▀   █▄█ █▄▄ █▄▄ █▄█ █▀▀ █▀█ ░█░ █ █▄█ █░▀█");
            Console.WriteLine();
            Console.Write("Enter new occupation name: ");
            string newOccupation = Console.ReadLine();
            if(newOccupation != null)
            {
                Occupation newOcc = new Occupation();
                newOcc.Name = newOccupation;
                using(var db = new MovieContext())
                {
                    var maxId = db.Occupations.Max(table => table.Id);
                    db.Occupations.Add(newOcc);
                    Console.Clear();
                    Console.WriteLine($"Added New Occupation: [{maxId + 1}] - [{newOcc.Name}]");
                    db.SaveChanges();
                }
            }else
            {
                Console.Clear();
                Logging.logX("Occupation name cannot be empty...");
                return;
            }
        }
    }
}