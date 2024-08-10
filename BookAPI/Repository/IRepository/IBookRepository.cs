using BookAPI.Models;

namespace BookAPI.Repository.IRepository
{
    public interface IBookRepository : IRepository <Book>
    {
        Task<Book> UpdateAsync(Book book);
    }
}
