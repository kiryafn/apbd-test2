using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.DTOs;
using Test.DTOs.AddBook;
using Test.Models;
using Test.Services.Abstractions;

namespace Test.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddBookAsync(CreateBookDTO dto)
    {
        var publishingHouse = await _context.PublishingHouses.FindAsync(dto.PublishingHouseId);
        if (publishingHouse == null) throw new Exceptions.NotFoundException("Publishing house not found");

        var authors = await _context.Authors.Where(a => dto.AuthorIds.Contains(a.AuthorId)).ToListAsync();
        if (authors.Count != dto.AuthorIds.Count) throw new Exceptions.NotFoundException("One or more authors not found");

        var genres = new List<Genre>();

        foreach (var g in dto.Genres)
        {
            Genre? genre = null;

            if (g.GenreId != null)
            {
                genre = await _context.Genres.FindAsync(g.GenreId.Value);
                if (genre == null) throw new Exceptions.NotFoundException($"Genre ID {g.GenreId} not found");
            }
            else
            {
                genre = await _context.Genres.FirstOrDefaultAsync(x => x.Name == g.GenreName);
                if (genre == null)
                {
                    genre = new Genre { Name = g.GenreName };
                    _context.Genres.Add(genre);
                    await _context.SaveChangesAsync();
                }
            }

            genres.Add(genre);
        }

        var book = new Book
        {
            Name = dto.Name,
            ReleaseDate = dto.ReleaseDate,
            PublishingHouseId = publishingHouse.PublishingHouseId,
            PublishingHouse = publishingHouse,
            Authors = authors,
            Genres = genres
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }
}