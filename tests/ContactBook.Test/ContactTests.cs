using ContactBook;

public static class ContactTests
{
    public static void EqualContactsShareEmailOrPhone()
    {
        var first = new Contact("Hailey", "Diaz", "782-923-2345", "haileyd@gmail.com");
        var second = new Contact("Hailey", "Gomez", "7829232345", "h.gomez@gmail.com");

        Assert.True(first == second, "Contacts with the same phone should be equal.");
    }

    public static void ToStringContainsContactData()
    {
        var contact = new Contact("Ana", "Rivera", "787-555-1000", "ana@example.com");
        var text = contact.ToString();

        Assert.True(text.Contains("Ana", StringComparison.OrdinalIgnoreCase), "ToString should include first name.");
        Assert.True(text.Contains("ana@example.com", StringComparison.OrdinalIgnoreCase), "ToString should include email.");
    }
}
