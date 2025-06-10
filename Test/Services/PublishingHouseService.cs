using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.DTOs;
using Test.Models;
using Test.Services.Abstractions;

namespace Test.Services;

public class PublishingHouseService : IPublishingHouseService
{
    private readonly ApplicationDbContext _context;

    public PublishingHouseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PublishingHouseDTO>> GetAllAsync(string? country, string? city)
    {
        var query = _context.PublishingHouses
            .Include(p => p.Books)
            .ThenInclude(b => b.Genres)
            .Include(p => p.Books)
            .ThenInclude(b => b.Authors)
            .AsQueryable();

        if (!string.IsNullOrEmpty(country))
            query = query.Where(p => p.Country == country);

        if (!string.IsNullOrEmpty(city))
            query = query.Where(p => p.City == city);

        return await query
            .OrderBy(p => p.Country)
            .ThenBy(p => p.Name)
            .Select(p => new PublishingHouseDTO
            {
                PublishingHouseId = p.PublishingHouseId,
                Name = p.Name,
                Country = p.Country,
                City = p.City,
                Books = p.Books.Select(b => new BookDTO
                {
                    Name = b.Name,
                    ReleaseDate = b.ReleaseDate,
                    Genres = b.Genres.Select(g => new GenreDTO
                    { 
                        Name = g.Name
                    }).ToList(),
                    Authors = b.Authors.Select(a => new AuthorDTO
                    {
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList()
                }).ToList()
            })
            .ToListAsync();
    }
}

