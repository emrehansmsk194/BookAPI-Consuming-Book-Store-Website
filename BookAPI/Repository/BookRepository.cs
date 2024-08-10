using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<Book> dbSet;
        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            this.dbSet = db.Set<Book>();
            
        }
        public async Task<Book> UpdateAsync(Book book)
        {
            book.CreatedDate = DateTime.Now;
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
            return book;
           
        }
    }
}
