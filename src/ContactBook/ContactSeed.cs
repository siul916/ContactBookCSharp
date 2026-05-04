namespace ContactBook;

public static class ContactSeed
{
    public static List<Contact> Create()
    {
        return
        [
            new Contact("Hailey", "", "", "haileyd@gmail.com"),
            new Contact("Hailey", "Diaz", "782-923-2345", "haileyd@gmail.com"),
            new Contact("Hailey", "Gomez", "782-923-2345", "h.gomez@gmail.com"),
            new Contact("Ana", "Rivera", "787-555-1000", "ana@example.com"),
            new Contact("Luis", "Santos", "787-555-2000", "luis@example.com"),
            new Contact("Maria", "Lopez", "787-555-3000", "maria@example.com")
        ];
    }
}
