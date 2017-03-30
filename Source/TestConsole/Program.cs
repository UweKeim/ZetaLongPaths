namespace TestConsole
{
    using System;
    using System.IO;
    using ZetaLongPaths;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string name = @"D:\SomeStuff\Name Space\More.Stuff\Test";

            var dirInfo5 = new ZlpDirectoryInfo(name);
            Console.WriteLine($@"'{dirInfo5.Name}'.");

            var dirInfo6 = new DirectoryInfo(name);
            Console.WriteLine($@"'{dirInfo6.Name}'.");

            if (dirInfo5.Name != dirInfo6.Name) throw new Exception(@"5-6");

            // --

            var dirInfo1 = new ZlpDirectoryInfo(@"C:\Foo\Bar");
            Console.WriteLine(dirInfo1.Name); //"Bar"
            var dirInfo2 = new ZlpDirectoryInfo(@"C:\Foo\Bar\");
            Console.WriteLine(dirInfo2.Name); //"", an empty string

            var dirInfo3 = new DirectoryInfo(@"C:\Foo\Bar");
            Console.WriteLine(dirInfo1.Name);
            var dirInfo4 = new DirectoryInfo(@"C:\Foo\Bar\");
            Console.WriteLine(dirInfo2.Name);

            if (dirInfo1.Name != dirInfo3.Name) throw new Exception(@"1-3");
            if (dirInfo2.Name != dirInfo4.Name) throw new Exception(@"2-4");

            // --

            var f1 = new ZlpFileInfo(
                @"C:\Ablage\test-only\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Lalala.txt");
            f1.Directory.Create();
            f1.WriteAllText("lalala.");
            Console.WriteLine(f1.Length);

            new ZlpDirectoryInfo(@"C:\Ablage\test-only\").Delete(true);
            //f1.MoveToRecycleBin();


            var f = new ZlpFileInfo(@"C:\Ablage\Lalala.txt");
            f.WriteAllText("lalala.");
            f.MoveToRecycleBin();

            var d = new ZlpDirectoryInfo(@"C:\Ablage\LalalaOrdner");
            d.Create();
            d.MoveToRecycleBin();
        }
    }
}