using System;

namespace FileEqualizer
{
    class Program
    {
        static void Main(string[] args)
        {

            FileComparator fileComparator = new FileComparator("D:\\books");
            fileComparator.GetFilesFromDirectory();
            fileComparator.GetFilesHash();

            Console.WriteLine("Files with equal content");
            fileComparator.FindEqualFiles();

            Console.ReadKey();
        }
    }
}
