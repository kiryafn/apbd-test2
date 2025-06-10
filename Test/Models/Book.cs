namespace Test.Models;

public class Book
{
    public int BookId { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    public int PublishingHouseId { get; set; }
    public PublishingHouse PublishingHouse { get; set; }
    
    public ICollection<Author> Authors { get; set; }
    public ICollection<Genre> Genres { get; set; }
}