namespace TestConsole
{
    using ZetaLongPaths;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var f = new ZlpFileInfo(@"C:\Ablage\Lalala.txt");
            f.WriteAllText("lalala.");
            f.MoveFileToRecycleBin();

            var d = new ZlpDirectoryInfo(@"C:\Ablage\LalalaOrdner");
            d.Create();
            d.MoveFileToRecycleBin();
        }
    }
}