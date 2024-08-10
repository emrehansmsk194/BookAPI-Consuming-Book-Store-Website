using BookAPI.Models;

namespace BookAPI.Repository.IRepository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Publisher> UpdateAsync(Publisher entity);
    }
}
