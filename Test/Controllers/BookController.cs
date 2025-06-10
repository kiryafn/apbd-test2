using Microsoft.AspNetCore.Mvc;
using Test.DTOs.AddBook;
using Test.Services.Abstractions;

namespace Test.Controllers;

[Controller]
[Route("/api/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookDTO dto)
    {
        try
        {
            await _bookService.AddBookAsync(dto);
            return Created();
        }
        catch (Exceptions.NotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }
}