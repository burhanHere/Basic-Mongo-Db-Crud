using Microsoft.AspNetCore.Mvc;
using MongooCrud.Models;
using MongooCrud.Services;

namespace MongooCrud.Controllers;

[ApiController]
[Route("MongooCrud/[controller]")]
public class BookController(BookService bookService) : ControllerBase
{
    private readonly BookService _bookService = bookService;

    [HttpGet]
    public async Task<List<Book>> Get() =>
           await _bookService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _bookService.GetAsync(id);

        if (book is null)
            return NotFound();

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Book newBook)
    {
        await _bookService.CreateAsync(newBook);
        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _bookService.GetAsync(id);
        if (book is null)
            return NotFound();

        updatedBook.Id = book.Id;
        await _bookService.UpdateAsync(id, updatedBook);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _bookService.GetAsync(id);
        if (book is null)
            return NotFound();

        await _bookService.RemoveAsync(id);
        return NoContent();
    }
}
