using Test.Models;

namespace Test.DTOs;

public class BookDTO
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    public ICollection<GenreDTO> Genres { get; set; }
    public ICollection<AuthorDTO> Authors { get; set; }
}