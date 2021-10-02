
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using core_ef_learning.Models;
using System.Threading;

[ApiController]
[Route("[controller]")]
public class BookStoreController : ControllerBase
{
    private readonly ILogger<BookStoreController> logger;
    private readonly BookServiceContext bookServiceContext;

    public BookStoreController(ILogger<BookStoreController> logger, BookServiceContext bookServiceContext)
    {

        this.logger = logger;
        this.bookServiceContext = bookServiceContext;
    }

    [HttpGet]
    [Route("GetBooksList")]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async IAsyncEnumerable<Book> GetBooks()
    {
        foreach (var item in await this.bookServiceContext.Books.ToListAsync())
        {
            logger.LogWarning(item.Author?.Name);
            yield return item;
        }
        // await this.bookServiceContext.Books.ToListAsync();
    }

    [HttpGet]
    [Route("Book/{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await this.bookServiceContext.Books.FindAsync(id);

        if (book == null)
            return NoContent();

        return Ok(book);

    }
    [HttpPost]
    [Route("CreateBook")]
    public async Task<IActionResult> CreateBook(Book model )
    {
        if (model == null)
            return BadRequest();

         this.bookServiceContext.Books.Add(model);

        var updateResult = await bookServiceContext.SaveChangesAsync();

        if (updateResult <= 0)
        {
            return BadRequest();
        }
        return Ok(model);

    }

    [HttpPut]
    [Route("UpdateBook/{id}")]
    public async Task<IActionResult> UpdateBook(int id,Book model )
    {
        var book = await this.bookServiceContext.Books.FindAsync(id);

        if (book == null)
            return NoContent();

        book.Title = model.Title;

        //bookServiceContext.Entry(book).State = EntityState.Modified;

        var updateResult = await bookServiceContext.SaveChangesAsync();
        if (updateResult <= 0)
        {
            return BadRequest();
        }
        return Ok(book);

    }

    [HttpDelete]
    [Route("DeleteBook/{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await this.bookServiceContext.Books.FindAsync(id);

        if (book == null)
            return NoContent();

    
        //bookServiceContext.Entry(book).State = EntityState.Modified;

        bookServiceContext.Books.Remove(book);

        var updateResult = await bookServiceContext.SaveChangesAsync();
        if (updateResult <= 0)
        {
            return BadRequest();
        }
        return Ok(book);

    }

}