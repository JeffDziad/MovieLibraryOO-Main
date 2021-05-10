using System;
using Microsoft.Extensions.DependencyInjection;

namespace MovieLibraryOO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine(@"   __  ___         _       __                  ___       __       __               ");
            Console.WriteLine(@"  /  |/  /__ _  __(_)__   / /  ___ ___  ___   / _ \___ _/ /____ _/ /  ___ ____ ___ ");
            Console.WriteLine(@" / /|_/ / _ \ |/ / / -_) / /__/ -_) _ \/_ /  / // / _ `/ __/ _ `/ _ \/ _ `(_-</ -_)");
            Console.WriteLine(@"/_/  /_/\___/___/_/\__/ /____/\__/_//_//__/ /____/\_,_/\__/\_,_/_.__/\_,_/___/\__/ ");
            Console.WriteLine(@"                                                                                   ");
            do
            {
                Menu.runMainMenu();
            }while(!(Menu.getIsFinished()));
        }
    }
}