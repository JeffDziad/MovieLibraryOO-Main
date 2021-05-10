using System;
using MovieLibraryOO.DataModels;
using System.Collections.Generic;

namespace MovieLibraryOO
{
    public interface IRetrieve 
    {
        void movieSearch();
        Movie findMovie();
        void printAllMovies();
        void viewTopRated();
        User findUser();
        List<Occupation> getOccupations(long occupationID);
    }
}