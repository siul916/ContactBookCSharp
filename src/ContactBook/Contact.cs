namespace ContactBook;

public sealed class Contact : IEquatable<Contact>
{
    public Contact(string firstName = "", string lastName = "", string phone = "", string email = "")
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Phone = phone.Trim();
        Email = email.Trim();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public bool IsEmpty => string.IsNullOrWhiteSpace(FirstName)
        && string.IsNullOrWhiteSpace(LastName)
        && string.IsNullOrWhiteSpace(Phone)
        && string.IsNullOrWhiteSpace(Email);

    public override string ToString()
    {
        return $"{FirstName} {LastName} | {Phone} | {Email}".Trim();
    }

    public bool Equals(Contact? other)
    {
        if (other is null)
        {
            return false;
        }

        return SameEmail(other) || SamePhone(other) || SameFullName(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Contact contact && Equals(contact);
    }

    public override int GetHashCode()
    {
        if (!string.IsNullOrWhiteSpace(Email))
        {
            return Normalize(Email).GetHashCode(StringComparison.OrdinalIgnoreCase);
        }

        if (!string.IsNullOrWhiteSpace(Phone))
        {
            return DigitsOnly(Phone).GetHashCode(StringComparison.OrdinalIgnoreCase);
        }

        return Normalize($"{FirstName} {LastName}").GetHashCode(StringComparison.OrdinalIgnoreCase);
    }

    public static bool operator ==(Contact? left, Contact? right)
    {
        return EqualityComparer<Contact>.Default.Equals(left, right);
    }

    public static bool operator !=(Contact? left, Contact? right)
    {
        return !(left == right);
    }

    public bool SameEmail(Contact other)
    {
        return !string.IsNullOrWhiteSpace(Email)
            && !string.IsNullOrWhiteSpace(other.Email)
            && string.Equals(Normalize(Email), Normalize(other.Email), StringComparison.OrdinalIgnoreCase);
    }

    public bool SamePhone(Contact other)
    {
        return !string.IsNullOrWhiteSpace(Phone)
            && !string.IsNullOrWhiteSpace(other.Phone)
            && DigitsOnly(Phone) == DigitsOnly(other.Phone);
    }

    public bool SameFullName(Contact other)
    {
        return !string.IsNullOrWhiteSpace(FirstName)
            && !string.IsNullOrWhiteSpace(LastName)
            && string.Equals(Normalize(FirstName), Normalize(other.FirstName), StringComparison.OrdinalIgnoreCase)
            && string.Equals(Normalize(LastName), Normalize(other.LastName), StringComparison.OrdinalIgnoreCase);
    }

    private static string Normalize(string value)
    {
        return value.Trim().ToLowerInvariant();
    }

    private static string DigitsOnly(string value)
    {
        return new string(value.Where(char.IsDigit).ToArray());
    }
}
