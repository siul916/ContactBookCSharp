using ContactBook;

public static class ContactBookTest
{
    public static void FindContactsReturnsMatches()
    {
        var book = new ContactBook.ContactBook(ContactSeed.Create());
        var results = book.FindContacts("Hailey");

        Assert.Equal(3, results.Count);
    }

    public static void FindDuplicateContactsUsesUnion()
    {
        var book = new ContactBook.ContactBook(ContactSeed.Create());
        var groups = book.FindDuplicateContacts();

        Assert.True(groups.Count >= 1, "At least one duplicate group should be found.");
        Assert.Equal(3, groups[0].Count);
    }
}
