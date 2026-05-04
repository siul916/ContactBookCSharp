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
        Assert.True(groups.Any(group => group.Count == 3 && group.All(contact => contact.FirstName == "Hailey")), "The Hailey duplicate group should contain three contacts.");
    }
    public static void MergeAutomaticallyKeepsBestFields()
    {
        var contacts = new List<Contact>
        {
            new("Hailey", "", "", "haileyd@gmail.com"),
            new("Hailey", "Gomez", "782-923-2345", "h.gomez@gmail.com")
        };

        var merged = ContactMerger.MergeAutomatically(contacts);

        Assert.Equal("Hailey", merged.FirstName);
        Assert.Equal("Gomez", merged.LastName);
        Assert.Equal("782-923-2345", merged.Phone);
        Assert.Equal("h.gomez@gmail.com", merged.Email);
    }
}


