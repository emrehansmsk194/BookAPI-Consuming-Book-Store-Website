using AutoMapper;
using BookWEB.Models.DTO;

namespace BookAPI
{
    public class MappingConfig : Profile
    {
       public MappingConfig() { 
           
            CreateMap<BookDTO,BookCreateDTO>().ReverseMap();
            CreateMap<BookDTO,BookUpdateDTO>().ReverseMap(); 
            CreateMap<PublisherDTO,PublisherCreateDTO>().ReverseMap();
            CreateMap<PublisherDTO,PublisherUpdateDTO>().ReverseMap();
            CreateMap<CategoryDTO,CategoryUpdateDTO>().ReverseMap();
            CreateMap<CategoryDTO,CategoryCreateDTO>().ReverseMap();
        }

    }
}
