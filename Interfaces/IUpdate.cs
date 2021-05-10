using System;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO
{
    public interface IUpdate
    {
        void addMovie();
        string grabTitle();
        DateTime grabReleaseDate();
        void updateMovie();
        void deleteMovie();
        void rateMovie();
        void addUser();
        void addOccupation();
    }
}