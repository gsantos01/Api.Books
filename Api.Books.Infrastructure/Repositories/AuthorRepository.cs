using Api.Books.Core.Entities;
using Api.Books.Infrastructure.Context;
using Api.Books.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Api.Books.Infrastructure.Repositories
{
    public class AuthorRepository(BooksDbContext dbContext) : Repository<Author>(dbContext), IAuthorRepository
    {
        private readonly BooksDbContext _dbContext = dbContext;

        public async Task MergeAuthor(Author author)
        {
            var authorToEdit = await _dbContext.Authors
                .FirstOrDefaultAsync(p => p.DocumentNumber == author.DocumentNumber);

            if (authorToEdit is null) {
                await _dbContext.Authors.AddAsync(author);
            }
            else{
                authorToEdit.Name = author.Name;
                authorToEdit.DateOfBirth = author.DateOfBirth;
                authorToEdit.Nickname = author.Nickname;
                _dbContext.Entry(authorToEdit).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
