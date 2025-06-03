namespace Domain;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public virtual Author Author { get; set; }
    public int PublisherId { get; set; }
    public virtual Publisher Publisher { get; set; }
    public virtual List<Genre> Genres { get; set; }
}