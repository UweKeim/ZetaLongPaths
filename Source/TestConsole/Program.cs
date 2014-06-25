namespace TestConsole
{
	using System;
	using ZetaLongPaths;

	class Program
	{
		static void Main(string[] args)
		{
			var u = ZlpIOHelper.GetFileLength(@"c:\ablage\fehlerbericht.txt");

			try
			{
				Console.WriteLine("Length: " + u);

				Console.WriteLine("Press any key...");
				Console.ReadLine();
			}
			catch (Exception x)
			{
				Console.WriteLine(x.Message);
				Console.WriteLine(x.StackTrace);
			}
		}
	}
}