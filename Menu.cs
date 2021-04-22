using System;
using System.Collections.Generic;
using ConsoleTables;

namespace MovieLibraryOO
{

    public static class Menu
    {
        static Boolean isFinished = false;

        public static Boolean getIsFinished() 
        {
            return isFinished;
        }

        public static void MainMenu()
        {
            Console.WriteLine(@"█▀▄▀█ █▀▀ █▄░█ █░█________________________________________________________________");
            Console.WriteLine(@"█░▀░█ ██▄ █░▀█ █▄█");
            Console.WriteLine();
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Movie Search");
            Console.WriteLine("2. Add Movie");
            Console.WriteLine("3. Update Movie");
            Console.WriteLine("4. Delete Movie\n");
            Console.Write("Menu Choice: ");
            HandleInput(Console.ReadLine());
        }

        public static void HandleInput(string input)
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
                            DataManipulator.movieSearch();
                            break;
                        case 2:
                            Console.Clear();
                            DataManipulator.addMovie();
                            break;
                        case 3:
                            Console.Clear();    
                            DataManipulator.updateMovie();
                            break;
                        case 4:
                            Console.Clear();
                            DataManipulator.deleteMovie();
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