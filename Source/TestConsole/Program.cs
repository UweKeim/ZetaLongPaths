namespace TestConsole
{
    using System;
    using ZetaLongPaths;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var f1 = new ZlpFileInfo(@"C:\Ablage\test-only\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Ablage\Lalala.txt");
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