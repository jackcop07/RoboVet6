using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RoboVet6.Service.Common.Models.API.Client;
using Client = RoboVet6.Data.Models.Client;


namespace RoboVet6.Service.Common.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<ClientToInsertDto, Client>();// means you want to map from ClientToInsertDto to Client
            CreateMap<Client, ClientToInsertDto>();

            CreateMap<Client, ClientToReturnDto>();
            CreateMap<ClientToReturnDto, Client>();
        }
    }
}
