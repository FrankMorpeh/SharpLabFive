using System.IO;

namespace SharpLabFive.Directories
{
    public static class DirectoriesChecker
    {
        public static void CreateDirectoriesIfNeeded()
        {
            if (!Directory.Exists(MainWindow.initialLocation + "\\Loggers"))
                Directory.CreateDirectory(MainWindow.initialLocation + "\\Loggers");
        }
    }
}