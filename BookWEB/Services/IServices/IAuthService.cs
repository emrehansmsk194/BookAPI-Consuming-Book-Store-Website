using BookWEB.Models.DTO;

namespace BookWEB.Services.IServices
{
	public interface IAuthService
	{
		Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
		Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate);
	}
}
