﻿using BookWEB.Models.DTO;

namespace BookWEB.Services.IServices
{
    public interface IBookService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T> (int id, string token);
        Task<T> CreateAsync<T>(BookCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(BookUpdateDTO dTO, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> GetByCategoryAsync<T>(int categoryId, string token);   
        Task<T> GetByPublisherAsync<T>(int publisherId, string token);
    }
}
