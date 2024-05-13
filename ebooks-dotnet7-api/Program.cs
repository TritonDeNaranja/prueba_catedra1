using ebooks_dotnet7_api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("ebooks"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var ebooks = app.MapGroup("api");

// TODO: Add more routes
ebooks.MapPost("/ebook", CreateEBookAsync);
ebooks.MapGet("/ebook/getAll",GetAllAvailable);
//ebooks.MapPut("/ebook/{id}",UpdateBook);
//ebooks.MapPut("/ebook/{id}/change-availability)",UpdateAvailability);
ebooks.MapPut("/ebook/{id}/{increment-stock}",IncreaseStock);
//ebooks.MapPost("/ebook/purchase",PurchaseEBook);

ebooks.MapDelete("/ebook/{id}",DeleteEBook); 


app.Run();

// TODO: Add more methods
async Task<IResult> CreateEBookAsync(EBook ebook, DataContext db)
{
    db.EBooks.Add(ebook);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/ebooks/{ebook.Id}", ebook);

}

async Task<IResult> GetAllAvailable(DataContext db)
{
    
    return TypedResults.Ok(await db.EBooks.Where(t => t.IsAvailable).ToListAsync());
}



/* async Task<IResult> UpdateBook(DataContext db)
{
    throw new NotImplementedException();
} */

/* async Task UpdateAvailability(HttpContext context)
{
    throw new NotImplementedException();
}
*/

async Task<IResult> IncreaseStock(DataContext db, int id, EBook inputEbook)
{
    var ebook = await db.EBooks.FindAsync(id);
    if (ebook is null) return TypedResults.NotFound();
    /*ebook.Title = inputEbook.Title;
    ebook.Author = inputEbook.Author;
    ebook.Genre = inputEbook.Genre;
    ebook.Format = inputEbook.Format;
    ebook.IsAvailable = inputEbook.IsAvailable;
    ebook.Price = inputEbook.Price; */
    ebook.Stock = inputEbook.Stock;
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

/* async Task PurchaseEBook(HttpContext context)
{
    throw new NotImplementedException();
} */
 
async  Task<IResult> DeleteEBook(DataContext db, int id)
{
    
    if (await db.EBooks.FindAsync(id) is EBook ebook)
    {
        db.EBooks.Remove(ebook);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
} 

