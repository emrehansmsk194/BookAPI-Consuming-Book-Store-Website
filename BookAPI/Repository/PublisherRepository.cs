using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<Publisher> dbSet;
        public PublisherRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
            this.dbSet = db.Set<Publisher>();
        }
        public async Task<Publisher> UpdateAsync(Publisher entity)
        {
            entity.CreatedDate = DateTime.Now;
            _db.Publishers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

           
        }
    }
}
