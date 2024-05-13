using ebooks_dotnet7_api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("ebooks"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var ebooks = app.MapGroup("api/ebook");

// TODO: Add more routes
ebooks.MapPost("/ebook", CreateEBookAsync);
ebooks.MapGet("/ebook?genre={genre}&author={author}&formtat={format}",GetAllAvailable);
ebooks.MapPut("/ebook/{id}",UpdateBook);
ebooks.MapPut("/ebook/{id}/change-availability)",UpdateAvailability);
ebooks.MapPut("/ebook/{id}/increment-stock",IncreaseStock);
ebooks.MapPost("/ebook/purchase",PurchaseEBook);
ebooks.MapDelete("/ebook/{id}",DeleteEBook);


app.Run();

// TODO: Add more methods
async Task<IResult> CreateEBookAsync(EBook ebook, DataContext db)
{
    db.EBooks.Add(ebook);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/todoitems/{ebook.Id}", ebook);

}

async Task<IResult> GetAllAvailable(DataContext db)
{
    return TypedResults.Ok(await db.EBooks.Where(t => t.IsAvailable).ToListAsync());
}



async Task UpdateBook(HttpContext context)
{
    throw new NotImplementedException();
}

async Task UpdateAvailability(HttpContext context)
{
    throw new NotImplementedException();
}

async Task IncreaseStock(HttpContext context)
{
    throw new NotImplementedException();
}

async Task PurchaseEBook(HttpContext context)
{
    throw new NotImplementedException();
}

async Task DeleteEBook(HttpContext context)
{
    throw new NotImplementedException();
}

