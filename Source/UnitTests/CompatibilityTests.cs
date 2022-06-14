namespace ZetaLongPaths.UnitTests;

/// <summary>
/// Testing compatibility of certain functions compared with System.IO to give same results.
/// </summary>
[TestFixture]
public sealed class CompatibilityTests
{
    /// <summary>
    /// Most paths behave the same and also are intended to behave the same.
    /// </summary>
    [Test]
    public void TestPathCombine1()
    {
        var paths = new List<Tuple<string, string>>
        {
            new(
                @"C:\Users\s.user\AppData\Roaming\SomeUser\SomeUser\Jobs\{e3791896-298f-48a2-9385-03af2e919c1b}\Data\BackupCache\",
                @"C:\Users\s.user\AppData\Roaming\SomeUser\SomeUser\Jobs\{e3791896-298f-48a2-9385-03af2e919c1b}\Data\BackupCache\4723948746642321523.pst"
            ),
            new(
                @"C:\Users\s.user\AppData\Roaming\SomeUser\SomeUser\Jobs\{e3791896-298f-48a2-9385-03af2e919c1b}\Data\BackupCache",
                @"C:\Users\s.user\AppData\Roaming\SomeUser\SomeUser\Jobs\{e3791896-298f-48a2-9385-03af2e919c1b}\Data\BackupCache\4723948746642321523.pst"
            ),
            new(
                @"C:\Users\s.user\AppData\Roaming\SomeUser\SomeUser\Jobs\{e3791896-298f-48a2-9385-03af2e919c1b}\Data\BackupCache\",
                @"4723948746642321523.pst"
            ),
            new(
                @"C:\Users\s.user\AppData\Roaming\SomeUser\SomeUser\Jobs\{e3791896-298f-48a2-9385-03af2e919c1b}\Data\BackupCache",
                @"4723948746642321523.pst"
            ),
            new(
                @"abc",
                @"def"
            ),
            new(
                @"\abc",
                @"def"
            ),
        };

        foreach (var (path1, path2) in paths)
        {
            var result1 = Path.Combine(path1, path2);
            var result2 = ZlpPathHelper.Combine(path1, path2);

            Assert.AreEqual(result1, result2);
        }
    }

    /// <summary>
    /// Some paths behave the different and also are intended to behave different.
    /// </summary>
    [Test]
    public void TestPathCombine2()
    {
        var paths = new List<Tuple<string, string>>
        {
            new(
                @"\abc",
                @"\def"
            ),
            new(
                @"abc",
                @"\def"
            ),
        };

        foreach (var (path1, path2) in paths)
        {
            var result1 = Path.Combine(path1, path2);
            var result2 = ZlpPathHelper.Combine(path1, path2);

            Assert.AreNotEqual(result1, result2);
        }
    }
}