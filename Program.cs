using PrivateOS.Business;

namespace PrivateOS
{
    class Bootstraper
    {
        public static void Main()
        {
            try
            {
                OS operatingSystem = new OS();
                operatingSystem.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happend, message: \n" + e.Message +"\n" + e.StackTrace);
            }
        }
    }
}