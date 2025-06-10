using Test.Models;

namespace Test.DTOs;

public class PublishingHouseDTO
{
    public int PublishingHouseId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public ICollection<BookDTO> Books { get; set; }
}