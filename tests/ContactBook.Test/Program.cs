using ContactBook;

var tests = new List<(string Name, Action Test)>
{
    ("Contact equality", ContactTests.EqualContactsShareEmailOrPhone),
    ("Contact ToString", ContactTests.ToStringContainsContactData),
    ("ContactBook find contacts", ContactBookTest.FindContactsReturnsMatches),
    ("ContactBook duplicate groups", ContactBookTest.FindDuplicateContactsUsesUnion),
    ("Union joins sets", UnionTest.JoinConnectsTwoSets),
    ("Union chains sets", UnionTest.FindUsesTransitiveConnection)
};

var passed = 0;
foreach (var (name, test) in tests)
{
    try
    {
        test();
        Console.WriteLine($"PASS: {name}");
        passed++;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"FAIL: {name}");
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine($"{passed}/{tests.Count} tests passed.");
Environment.ExitCode = passed == tests.Count ? 0 : 1;

internal static class Assert
{
    public static void True(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    public static void Equal<T>(T expected, T actual)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new InvalidOperationException($"Expected {expected}, got {actual}.");
        }
    }
}
