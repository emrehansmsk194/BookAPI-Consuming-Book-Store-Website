using BookWEB.Models.DTO;

namespace BookWEB.Services.IServices
{
	public interface IPublisherService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetAsync<T>(int id);
		Task<T> CreateAsync<T>(PublisherCreateDTO dto);
		Task<T> UpdateAsync<T>(PublisherUpdateDTO dTO);
		Task<T> DeleteAsync<T>(int id);
	}
}
