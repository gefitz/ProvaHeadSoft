using AutoMapper;
using Entityframework.Model;

namespace Entityframework.DTO.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Autor, PessoaDTO>();
        }
    }
}
