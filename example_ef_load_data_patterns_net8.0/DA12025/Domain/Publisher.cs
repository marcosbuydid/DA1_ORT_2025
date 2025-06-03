namespace Domain;

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<Book> Books { get; set; }
}