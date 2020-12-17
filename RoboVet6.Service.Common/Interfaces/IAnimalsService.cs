﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API;
using RoboVet6.Service.Common.Models.API.Animal;

namespace RoboVet6.Service.Common.Interfaces
{
    public interface IAnimalsService
    {
        Task<List<AnimalToReturnDto>> GetAnimalsByClientId(int clientId);
        Task<AnimalToReturnDto> GetAnimalByAnimalId(int animalId);
        Task<List<AnimalToReturnDto>> GetAllAnimals();
        Task<AnimalToReturnDto> InsertAnimal(AnimalToInsertDto animalToInsert, int clientId);
        Task<bool> AnimalExists(int animalId);
    }
}
