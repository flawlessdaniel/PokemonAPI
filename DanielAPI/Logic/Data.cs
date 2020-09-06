using DanielAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DanielAPI.Logic
{
    public static class Data
    {
        public static List<A001Pokemon> getDataBase()
        {
            using (StreamReader r = new StreamReader("pokedex.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<A001Pokemon>>(json);
            }
        }
    }
}
