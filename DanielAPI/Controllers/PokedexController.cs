using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DanielAPI.Logic;
using DanielAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DanielAPI.Controllers
{
    [Route("api/pokedex")]
    [ApiController]
    [Authorize]
    public class PokedexController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public string GetPokemons()
        {
            try
            {
                List<A001Pokemon> lst = Data.getDataBase();
                return JsonConvert.SerializeObject(lst);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        [HttpGet]
        [Route("search/{key}")]
        [EnableCors("AllowOrigin")]
        public string GetPokemonsSearch(string key)
        {
            try
            {
                List<A001Pokemon> lst = Data.getDataBase();
                return JsonConvert.SerializeObject(lst.Where(x => x.Name.English.Contains(key) || x.Id.ToString() == key).ToList());
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        [HttpGet]
        [Route("data/{id}")]
        [EnableCors("AllowOrigin")]
        public string GetPokemonById(int id)
        {
            try
            {
                List<A001Pokemon> lst = Data.getDataBase();
                return JsonConvert.SerializeObject(lst.Where(x => x.Id == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}