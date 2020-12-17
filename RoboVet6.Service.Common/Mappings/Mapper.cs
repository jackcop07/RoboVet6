using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Common.Models.API.Client;
using Animal = RoboVet6.Data.Models.Animal;
using Client = RoboVet6.Data.Models.Client;


namespace RoboVet6.Service.Common.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //Clients
            CreateMap<ClientToInsertDto, Client>();
            CreateMap<Client, ClientToInsertDto>();

            CreateMap<Client, ClientToReturnDto>();
            CreateMap<ClientToReturnDto, Client>();

            //Animals
            CreateMap<AnimalToInsertDto, Animal>();
            CreateMap<Animal, AnimalToInsertDto>();

            CreateMap<Animal, AnimalToReturnDto>();
            CreateMap<AnimalToReturnDto, Animal>();
        }
    }
}
