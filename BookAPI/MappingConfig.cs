using AutoMapper;
using BookAPI.Models;
using BookAPI.Models.DTO;

namespace BookAPI
{
    public class MappingConfig : Profile
    {
       public MappingConfig() { 
            CreateMap<Book,BookDTO>().ReverseMap();
            CreateMap<Book,BookCreateDTO>().ReverseMap();
            CreateMap<Book,BookUpdateDTO>().ReverseMap(); 
            CreateMap<Publisher,PublisherDTO>().ReverseMap();
            CreateMap<Publisher,PublisherCreateDTO>().ReverseMap();
            CreateMap<Publisher,PublisherUpdateDTO>().ReverseMap();
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Category,CategoryUpdateDTO>().ReverseMap();
            CreateMap<Category,CategoryCreateDTO>().ReverseMap();
            CreateMap<ApplicationUser,UserDTO>().ReverseMap();
        }

    }
}
