namespace ContactBook;

public enum ContactSortField
{
    FirstName,
    LastName,
    Phone,
    Email
}

public sealed class ContactComparer : IComparer<Contact>
{
    private readonly ContactSortField _field;

    public ContactComparer(ContactSortField field = ContactSortField.FirstName)
    {
        _field = field;
    }

    public int Compare(Contact? x, Contact? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (x is null)
        {
            return -1;
        }

        if (y is null)
        {
            return 1;
        }

        var result = _field switch
        {
            ContactSortField.FirstName => CompareText(x.FirstName, y.FirstName),
            ContactSortField.LastName => CompareText(x.LastName, y.LastName),
            ContactSortField.Phone => CompareText(x.Phone, y.Phone),
            ContactSortField.Email => CompareText(x.Email, y.Email),
            _ => CompareText(x.FirstName, y.FirstName)
        };

        if (result != 0)
        {
            return result;
        }

        result = CompareText(x.LastName, y.LastName);
        if (result != 0)
        {
            return result;
        }

        result = CompareText(x.FirstName, y.FirstName);
        if (result != 0)
        {
            return result;
        }

        result = CompareText(x.Phone, y.Phone);
        return result != 0 ? result : CompareText(x.Email, y.Email);
    }

    private static int CompareText(string first, string second)
    {
        return string.Compare(first, second, StringComparison.OrdinalIgnoreCase);
    }
}
