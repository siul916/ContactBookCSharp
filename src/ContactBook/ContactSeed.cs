namespace ContactBook;

public static class ContactSeed
{
    private static readonly string[] FirstNames =
    [
        "James", "Emma", "Liam", "Olivia", "Noah", "Ava", "Sophia", "Isabella", "Mason", "Lucas",
        "Mia", "Ethan", "Amelia", "Logan", "Harper", "Jacob", "Evelyn", "Michael", "Abigail", "Daniel"
    ];

    private static readonly string[] LastNames =
    [
        "Wilson", "Johnson", "Brown", "Davis", "Smith", "Clark", "Miller", "Moore", "Taylor", "Anderson",
        "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Lewis"
    ];

    private static readonly string[] Domains = ["gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "example.com"];

    public static List<Contact> Create()
    {
        var contacts = new List<Contact>();

        for (var i = 0; i < 100; i++)
        {
            var firstName = FirstNames[i % FirstNames.Length];
            var lastName = LastNames[i % LastNames.Length];
            var areaCode = 212 + (i * 31 % 700);
            var prefix = 555;
            var line = 1000 + (i * 137 % 9000);
            var phone = $"{areaCode:000}-{prefix:000}-{line:0000}";
            var email = $"{firstName.ToLowerInvariant()}.{lastName.ToLowerInvariant()}{i + 1}@{Domains[i % Domains.Length]}";

            contacts.Add(new Contact(firstName, lastName, phone, email));
        }

        contacts.Add(new Contact("Hailey", "", "", "haileyd@gmail.com"));
        contacts.Add(new Contact("Hailey", "Diaz", "782-923-2345", "haileyd@gmail.com"));
        contacts.Add(new Contact("Hailey", "Gomez", "782-923-2345", "h.gomez@gmail.com"));

        return contacts;
    }
}
