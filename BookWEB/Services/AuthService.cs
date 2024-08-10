using BookAPI_Utility;
using BookWEB.Models;
using BookWEB.Models.DTO;
using BookWEB.Services.IServices;

namespace BookWEB.Services
{
	public class AuthService : BaseService,IAuthService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string bookUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration):base(clientFactory) 
        {
			_clientFactory = clientFactory;
			bookUrl = configuration.GetValue<string>("ServiceUrls:BookAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO objToCreate)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = objToCreate,
				Url = bookUrl + "api/Users/login"

			});
		}

		public Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = objToCreate,
				Url = bookUrl + "api/Users/Register"
			});
		}
	}
}
