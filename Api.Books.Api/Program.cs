using Api.Books.Core.Aplications;
using Api.Books.Core.Interfaces.Aplications;
using Api.Books.Infrastructure.Context;
using Api.Books.Infrastructure.Repositories;
using Api.Books.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<IBookApplication, BookApplication>();

builder.Services.AddDbContext<BooksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BooksDbContext>();
    db.Database.Migrate();
}

app.Run();
