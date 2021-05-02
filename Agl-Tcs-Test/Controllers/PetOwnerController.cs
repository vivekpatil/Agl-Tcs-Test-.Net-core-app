using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agl_Tcs_Test.Interfaces;
using Agl_Tcs_Test.Models;
using Agl_Tcs_Test.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace Agl_Tcs_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetOwnerController : ControllerBase
    {
        private readonly IDataSource _dataSource;
        

        public PetOwnerController(IDataSource dataSource)
        {
            _dataSource = dataSource;
            
        }

        
        [HttpGet]
        public async Task<string> GetresultAsync()
        {
            var returnValue = string.Empty;
            var genderPets = new List<GenderBasedPets>();

            returnValue = await _dataSource.GetDataAsync();
            if (!returnValue.Equals(string.Empty))
            {
                var apiData = JsonConvert.DeserializeObject<List<Owner>>(returnValue);

                var genders = GetDistinctGenders(apiData);

                foreach (var gender in genders)
                {
                    var petownerList = apiData.Where(i => i.Gender == gender).Select(i => i.Pets);
                    var pets = new List<string>();
                    foreach (var petOwner in petownerList)
                    {
                        if (petOwner != null)
                        {
                            var pet = GetSelectedPetsList(petOwner, "Cat");
                            pets.AddRange(pet);
                        }
                    }

                    var gbp = new GenderBasedPets
                    {
                        Gender = gender,
                        PetName = pets.OrderBy(i => i).ToList()
                    };
                    genderPets.Add(gbp);
                }
            }

            return JsonConvert.SerializeObject(genderPets);
                
        }

        private static List<string> GetSelectedPetsList(List<Pet> petOwner, string petType)
        {
            var pet = petOwner.Where(i => string.Equals(i.Type, petType, StringComparison.CurrentCultureIgnoreCase)).Select(i => i.Name).ToList();
            return pet;
        }

        public List<string> GetDistinctGenders(List<Owner> apiData)
        {
            var genders = apiData.Select(i => i.Gender).Distinct().ToList();
            return genders;
        }
    }
}
