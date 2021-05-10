namespace MovieLibraryOO
{
    public static class DependencyInjection
    {
        //IRetrieve Class
        private static readonly IRetrieve retrieve = new RetrieveData();

        //IUpdate Class
        private static readonly IUpdate update = new UpdateData();

        public static IRetrieve getIRetrieve() 
        {
            return retrieve;
        }

        public static IUpdate getIUpdate()
        {
            return update;
        }
    }
}