using AutoMapper;
using BookStore.API.Domain;
using BookStore.Infractructure.Dtos;

namespace BookStore.Infrastructure.Mappers;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDTO>().ReverseMap();
    }
}