namespace ContactBook;

public static class ContactMerger
{
    public static Contact Merge(IReadOnlyList<Contact> contacts, int firstNameIndex, int lastNameIndex, int phoneIndex, int emailIndex)
    {
        return new Contact(
            contacts[firstNameIndex].FirstName,
            contacts[lastNameIndex].LastName,
            contacts[phoneIndex].Phone,
            contacts[emailIndex].Email);
    }

    public static Contact MergeAutomatically(IReadOnlyList<Contact> contacts)
    {
        return new Contact(
            PickBest(contacts.Select(contact => contact.FirstName)),
            PickBest(contacts.Select(contact => contact.LastName)),
            PickBest(contacts.Select(contact => contact.Phone)),
            PickBest(contacts.Select(contact => contact.Email)));
    }

    private static string PickBest(IEnumerable<string> values)
    {
        return values
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .OrderByDescending(value => value.Length)
            .ThenBy(value => value, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault() ?? string.Empty;
    }
}
