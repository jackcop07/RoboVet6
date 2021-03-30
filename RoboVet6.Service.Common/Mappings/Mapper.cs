using AutoMapper;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Common.Models.API.Breed;
using RoboVet6.Service.Common.Models.API.Client;
using RoboVet6.Service.Common.Models.API.Colour;
using RoboVet6.Service.Common.Models.API.Species;
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

            //Species
            CreateMap<SpeciesToInsertDto, SpeciesModel>();
            CreateMap<SpeciesModel, SpeciesToInsertDto>();

            CreateMap<SpeciesModel, SpeciesToReturnDto>();
            CreateMap<SpeciesToReturnDto, SpeciesModel>();

            CreateMap<SpeciesModel, SpeciesToUpdateDto>();
            CreateMap<SpeciesToUpdateDto, SpeciesModel>();

            //Breeds
            CreateMap<BreedToInsertDto, BreedModel>();
            CreateMap<BreedModel, BreedToInsertDto>();

            CreateMap<BreedModel, BreedToReturnDto>();
            CreateMap<BreedToReturnDto, BreedModel>();

            CreateMap<BreedModel, BreedToUpdateDto>();
            CreateMap<BreedToUpdateDto, BreedModel>();

            //Colours
            CreateMap<ColourToInsertDto, ColourModel>();
            CreateMap<ColourModel, ColourToInsertDto>();

            CreateMap<ColourModel, ColourToReturnDto>();
            CreateMap<ColourToReturnDto, ColourModel>();

            CreateMap<ColourModel, ColourToReturnDto>();
            CreateMap<ColourToReturnDto, ColourModel>();


        }
    }
}
