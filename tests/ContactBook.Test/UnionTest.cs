using ContactBook;

public static class UnionTest
{
    public static void JoinConnectsTwoSets()
    {
        var union = new Union<int>();
        union.Join(1, 2);

        Assert.True(union.Connected(1, 2), "Union should connect two values.");
    }

    public static void FindUsesTransitiveConnection()
    {
        var union = new Union<int>();
        union.Join(1, 2);
        union.Join(2, 3);

        Assert.True(union.Connected(1, 3), "Find should see transitive connections.");
    }
}
