namespace ContactBook;

public sealed class ContactBook
{
    private const int PageSize = 5;

    private readonly List<Contact> _contacts;
    private int _pageIndex;
    private ContactSortField _sortField = ContactSortField.FirstName;

    public ContactBook(IEnumerable<Contact> contacts)
    {
        _contacts = contacts.ToList();
    }

    public IReadOnlyList<Contact> Contacts => _contacts;

    public void Run()
    {
        var exit = false;
        while (!exit)
        {
            ShowWelcomeScreen();
            ShowContacts(_contacts);
            ShowInputOptions();

            var option = Console.ReadLine()?.Trim().ToUpperInvariant();
            exit = HandleOption(option);
        }
    }

    public List<Contact> FindContacts(string query)
    {
        return _contacts.FindAll(contact =>
            contact.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
            || contact.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
            || contact.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)
            || contact.Email.Contains(query, StringComparison.OrdinalIgnoreCase));
    }

    public List<List<Contact>> FindDuplicateContacts()
    {
        var union = new Union<int>();
        for (var i = 0; i < _contacts.Count; i++)
        {
            union.MakeSet(i);
        }

        for (var i = 0; i < _contacts.Count; i++)
        {
            for (var j = i + 1; j < _contacts.Count; j++)
            {
                if (_contacts[i].Equals(_contacts[j]))
                {
                    union.Join(i, j);
                }
            }
        }

        return Enumerable.Range(0, _contacts.Count)
            .GroupBy(union.Find)
            .Select(group => group.Select(index => _contacts[index]).ToList())
            .Where(group => group.Count > 1)
            .ToList();
    }

    private bool HandleOption(string? option)
    {
        switch (option)
        {
            case "N":
                NextPage();
                break;
            case "P":
                PreviousPage();
                break;
            case "C":
                CreateContact();
                break;
            case "R":
                ReviewContact();
                break;
            case "F":
                FindContactsScreen();
                break;
            case "O":
                OrderContacts();
                break;
            case "D":
                DeduplicateContacts();
                break;
            case "X":
                ShowExitScreen();
                return true;
            default:
                Pause("Invalid option.");
                break;
        }

        return false;
    }

    private void ShowWelcomeScreen()
    {
        Clear();
        WriteTitle("Contact Book");
    }

    private void ShowInputOptions()
    {
        Console.WriteLine("[N] Next Page    [P] Previous Page");
        Console.WriteLine("[C] Create       [R] Review");
        Console.WriteLine("[F] Find         [O] Order");
        Console.WriteLine("[D] Deduplicate  [X] Exit");
        Console.Write("Option: ");
    }

    private void ShowExitScreen()
    {
        Clear();
        WriteTitle("Goodbye");
    }

    private void ShowContacts(IReadOnlyList<Contact> contacts, string title = "Contacts")
    {
        var ordered = contacts.Order(new ContactComparer(_sortField)).ToList();
        var totalPages = Math.Max(1, (int)Math.Ceiling(ordered.Count / (double)PageSize));
        _pageIndex = Math.Clamp(_pageIndex, 0, totalPages - 1);
        var page = ordered.Skip(_pageIndex * PageSize).Take(PageSize).ToList();

        WriteTitle(title);
        WriteContactTable(page, _pageIndex * PageSize + 1);

        var first = ordered.Count == 0 ? 0 : _pageIndex * PageSize + 1;
        var last = Math.Min((_pageIndex + 1) * PageSize, ordered.Count);
        Console.WriteLine($"Page {_pageIndex + 1} of {totalPages} ({first}-{last} of {ordered.Count})");
        Console.WriteLine();
    }

    private void NextPage()
    {
        _pageIndex++;
    }

    private void PreviousPage()
    {
        _pageIndex = Math.Max(0, _pageIndex - 1);
    }

    private void CreateContact()
    {
        Clear();
        WriteTitle("Create Contact");

        var contact = new Contact(
            ReadRequired("First name: "),
            ReadRequired("Last name: "),
            ReadRequired("Phone: "),
            ReadRequired("Email: "));

        _contacts.Add(contact);
        Pause("Contact created.");
    }

    private void ReviewContact()
    {
        Clear();
        ShowContacts(_contacts, "Review Contact");
        var index = ReadIndex("Enter contact index", _contacts.Count) - 1;
        var contact = _contacts[index];

        Clear();
        WriteTitle("Selected Contact");
        WriteContactTable([contact], index + 1);

        if (Confirm("Do you want to delete this contact?"))
        {
            _contacts.RemoveAt(index);
            Pause("Contact deleted.");
            return;
        }

        Pause("No changes made.");
    }

    private void FindContactsScreen()
    {
        Clear();
        var query = ReadRequired("Search: ");
        var results = FindContacts(query);
        Clear();
        ShowContacts(results, "Search Results");
        Pause();
    }

    private void OrderContacts()
    {
        Clear();
        WriteTitle("Order Contacts");
        Console.WriteLine("1. First Name");
        Console.WriteLine("2. Last Name");
        Console.WriteLine("3. Phone");
        Console.WriteLine("4. Email");

        _sortField = ReadIndex("Choose order", 4) switch
        {
            1 => ContactSortField.FirstName,
            2 => ContactSortField.LastName,
            3 => ContactSortField.Phone,
            4 => ContactSortField.Email,
            _ => ContactSortField.FirstName
        };
    }

    private void DeduplicateContacts()
    {
        Clear();
        var groups = FindDuplicateContacts();
        if (groups.Count == 0)
        {
            Pause("No duplicate contacts were found.");
            return;
        }

        foreach (var group in groups)
        {
            Clear();
            WriteTitle("Duplicate Contacts");
            WriteContactTable(group, 1);
            Console.WriteLine($"Page 1 of 1 (1-{group.Count} of {group.Count})");

            var firstNameIndex = ReadIndex("Enter first name index", group.Count) - 1;
            var lastNameIndex = ReadIndex("Enter last name index", group.Count) - 1;
            var phoneIndex = ReadIndex("Enter phone index", group.Count) - 1;
            var emailIndex = ReadIndex("Enter email index", group.Count) - 1;
            var mergedContact = ContactMerger.Merge(group, firstNameIndex, lastNameIndex, phoneIndex, emailIndex);

            Console.WriteLine();
            WriteTitle("Merged Contact");
            WriteContactTable([mergedContact], 1);
            Console.WriteLine("Page 1 of 1 (1-1 of 1)");

            if (!Confirm("Do you want to merge these contacts?"))
            {
                Pause("Merge cancelled.");
                continue;
            }

            foreach (var contact in group)
            {
                _contacts.Remove(contact);
            }

            _contacts.Add(mergedContact);
            Pause("Contacts merged.");
        }
    }

    private static void WriteTitle(string title)
    {
        Console.WriteLine(new string('#', 80));
        Console.WriteLine(title);
        Console.WriteLine(new string('#', 80));
    }

    private static void WriteContactTable(IReadOnlyList<Contact> contacts, int startIndex)
    {
        Console.WriteLine("#  First Name   Last Name    Phone           Email");
        Console.WriteLine("---------------------------------------------------------------");

        for (var i = 0; i < contacts.Count; i++)
        {
            var contact = contacts[i];
            Console.WriteLine($"{startIndex + i,-2} {contact.FirstName,-12} {contact.LastName,-12} {contact.Phone,-15} {contact.Email}");
        }

        Console.WriteLine();
    }

    private static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine("This value is required.");
        }
    }

    private static int ReadIndex(string prompt, int maximum)
    {
        while (true)
        {
            Console.Write($"{prompt} [1-{maximum}] ");
            if (int.TryParse(Console.ReadLine(), out var index) && index >= 1 && index <= maximum)
            {
                return index;
            }

            Console.WriteLine("Invalid index.");
        }
    }

    private static bool Confirm(string prompt)
    {
        Console.Write($"{prompt} [Y/N] (N) ");
        var input = Console.ReadLine()?.Trim().ToUpperInvariant();
        return input is "Y" or "YES";
    }

    private static void Pause(string message = "")
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            Console.WriteLine(message);
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void Clear()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
        }
    }
}
