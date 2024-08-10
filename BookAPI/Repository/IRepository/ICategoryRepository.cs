using BookAPI.Models;

namespace BookAPI.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> UpdateAsync(Category category);
    }
}
