namespace ZetaLongPaths.UnitTests
{
	using System.Collections;
	using NUnit.Framework;

	public class TestSuite
	{
		[Suite]
		public static IEnumerable Suite
		{
			get
			{
				return
					new ArrayList
						{
							new DirectoryInfoTest(),
							new FileInfoTest(),
							new IOHelperTest(),
							new PathHelperTest()
						};
			}
		}
	}
}