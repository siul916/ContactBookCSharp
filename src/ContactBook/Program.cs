namespace ContactBook;

public static class Program
{
    public static void Main()
    {
        var contactBook = new ContactBook(ContactSeed.Create());
        contactBook.Run();
    }
}
