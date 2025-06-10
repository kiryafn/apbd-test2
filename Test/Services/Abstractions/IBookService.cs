using Test.DTOs;
using Test.DTOs.AddBook;

namespace Test.Services.Abstractions;

public interface IBookService
{
    public Task AddBookAsync(CreateBookDTO book);
}