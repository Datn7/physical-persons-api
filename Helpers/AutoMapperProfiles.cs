using AutoMapper;
using physical_persons_api.DTOs;
using physical_persons_api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace physical_persons_api.Helpers
{
    public class AutoMapperProfiles: Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<PersonDTO, Person>().ReverseMap();
            CreateMap<PersonCreationDTO, Person>().ForMember(x => x.Picture, options => options.Ignore());
        }
    }
}
