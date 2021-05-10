using System;
using System.Collections.Generic;
using ConsoleTables;

namespace MovieLibraryOO
{

    public static class Menu
    {
        private static IRetrieve retriever = DependencyInjection.getIRetrieve();
        private static IUpdate updater = DependencyInjection.getIUpdate();
        static Boolean isFinished = false;

        public static Boolean getIsFinished() 
        {
            return isFinished;
        }

        public static void runMainMenu()
        {
            Console.WriteLine(@"█▀▄▀█ █▀▀ █▄░█ █░█________________________________________________________________");
            Console.WriteLine(@"█░▀░█ ██▄ █░▀█ █▄█");
            Console.WriteLine();
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Movie Menu");
            Console.WriteLine("2. Data Menu\n");
            Console.Write("Menu Choice: ");
            handleMainInput(Console.ReadLine());
        }

        public static void handleMainInput(string input)
        {
            int inputNum = 0;
            if(input == null || input == "")
            {
                Console.Clear();
                Logging.logX("Please enter a valid menu option!");
            }else
            {
                try
                {
                    inputNum = Convert.ToInt32(input);
                    switch(inputNum)
                    {
                        case 0:
                            isFinished = true;
                            Console.Clear();
                            break;
                        case 1:
                            Console.Clear();
                            movieMenu();
                            break;
                        case 2:
                            Console.Clear();
                            dataMenu();
                            break;
                        default:
                            Console.Clear();
                            Logging.logX("Please enter a valid menu option!");
                            break;
                    }
                }catch(Exception ex)
                {
                    Console.Clear();
                    Logging.log("Please enter a valid menu option!", ex);
                }
            }
        }

        public static void movieMenu() 
        {
            Console.WriteLine(@"█▀▄▀█ █▀█ █░█ █ █▀▀   █▀▄▀█ █▀▀ █▄░█ █░█");
            Console.WriteLine(@"█░▀░█ █▄█ ▀▄▀ █ ██▄   █░▀░█ ██▄ █░▀█ █▄█");
            Console.WriteLine();
            Console.WriteLine("0. Back");
            Console.WriteLine("1. Search Movie");
            Console.WriteLine("2. Print All Movies");
            Console.Write("Menu Option: ");
            handleMovieInput(Console.ReadLine());
        }

        public static void handleMovieInput(String input)
        {
            int inputNum = 0;
            if(input == null || input == "")
            {
                Console.Clear();
                Logging.logX("Please enter a valid menu option!");
            }else
            {
                try
                {
                    inputNum = Convert.ToInt32(input);
                    switch(inputNum)
                    {
                        case 0:
                            Console.Clear();
                            break;
                        case 1:
                            Console.Clear();
                            retriever.movieSearch();
                            break;
                        case 2:
                            Console.Clear();
                            retriever.printAllMovies();
                            break;
                        default:
                            Console.Clear();
                            Logging.logX("Please enter a valid menu option!");
                            break;
                    }
                }catch(Exception ex)
                {
                    Console.Clear();
                    Logging.log("Please enter a valid menu option!", ex);
                }
            }
        }

        public static void dataMenu() 
        {
            Console.WriteLine(@"█▀▄ ▄▀█ ▀█▀ ▄▀█   █▀▄▀█ █▀▀ █▄░█ █░█");
            Console.WriteLine(@"█▄▀ █▀█ ░█░ █▀█   █░▀░█ ██▄ █░▀█ █▄█");
            Console.WriteLine();
            Console.WriteLine("0. Back");
            Console.WriteLine("1. Add Movie");
            Console.WriteLine("2. Add User");
            Console.WriteLine("3. Add Occupation");
            Console.WriteLine("4. Rate Movie");
            Console.WriteLine("5. Update Movie");
            Console.WriteLine("6. Delete Movie\n");
            Console.Write("Movie Option: ");
            handleDataInput(Console.ReadLine());
        }

        public static void handleDataInput(String input) 
        {
            int inputNum = 0;
            if(input == null || input == "")
            {
                Console.Clear();
                Logging.logX("Please enter a valid menu option!");
            }else
            {
                try
                {
                    inputNum = Convert.ToInt32(input);
                    switch(inputNum)
                    {
                        case 0:
                            Console.Clear();
                            break;
                        case 1:
                            Console.Clear();
                            updater.addMovie();
                            break;
                        case 2:
                            Console.Clear();
                            updater.addUser();
                            break;
                        case 3:
                            Console.Clear();
                            updater.addOccupation();
                            break;
                        case 4:
                            Console.Clear();
                            updater.rateMovie();
                            break;
                        case 5:
                            Console.Clear();
                            updater.updateMovie();
                            break;
                        case 6:
                            Console.Clear();
                            updater.deleteMovie();
                            break;
                        default:
                            Console.Clear();
                            Logging.logX("Please enter a valid menu option!");
                            break;
                    }
                }catch(Exception ex)
                {
                    Console.Clear();
                    Logging.log("Please enter a valid menu option!", ex);
                }
            }
        }
    }
}