namespace PWC_Question
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CityData cityData = new CityData();
            foreach (var item in args)
            {
                cityData.Data(item);

            }
        }
    }
}