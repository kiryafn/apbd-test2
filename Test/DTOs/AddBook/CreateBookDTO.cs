namespace Test.DTOs.AddBook;

public class CreateBookDTO
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int PublishingHouseId { get; set; }
    public List<int> AuthorIds { get; set; }
    public List<GenreInputDTO> Genres { get; set; }
}

