using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<Category> dbSet;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            this.dbSet = db.Set<Category>();
        }
        public async Task<Category> UpdateAsync(Category category)
        {
           category.UpdatedDate = DateTime.Now;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return category;
        }
    }
}
