using AutoMapper;
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Common.Models.API.Client;
using AnimalModel = RoboVet6.Data.Models.RoboVet6.AnimalModel;
using ClientModel = RoboVet6.Data.Models.RoboVet6.ClientModel;


namespace RoboVet6.Service.Common.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //Clients
            CreateMap<ClientToInsertDto, ClientModel>();
            CreateMap<ClientModel, ClientToInsertDto>();

            CreateMap<ClientModel, ClientToReturnDto>();
            CreateMap<ClientToReturnDto, ClientModel>();

            CreateMap<ClientModel, ClientToUpdateDto>();
            CreateMap<ClientToUpdateDto, ClientModel>();

            //Animals
            CreateMap<AnimalToInsertDto, AnimalModel>();
            CreateMap<AnimalModel, AnimalToInsertDto>();

            CreateMap<AnimalModel, AnimalToReturnDto>();
            CreateMap<AnimalToReturnDto, AnimalModel>();

            CreateMap<AnimalModel, AnimalToUpdateDto>();
            CreateMap<AnimalToUpdateDto, AnimalModel>();
        }
    }
}
