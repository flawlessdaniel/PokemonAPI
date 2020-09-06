using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanielAPI.Models
{
    public class A001Pokemon
    {

        public int Id { get; set; }

        public A001PokemonName Name { get; set; }

        public string[] Type { get; set; }

        public A001PokemonBase Base { get; set; }

        private string imageurl;
        public string ImageUrl
        {
            ///No me gusta mucho hacer esto, pero por propositos solo de la prueba y datos fijos en json
            get { return Startup.Host + "/MyImages/" + GeneraCodigo(this.Id) + ".png"; }
        }

        public string Codigo
        {
            get { return GeneraCodigo(this.Id); }
        }

        private string GeneraCodigo(int id)
        {
            return (id.ToString().Length == 2) ? string.Concat("0", id.ToString()) : (id.ToString().Length == 1) ? string.Concat("00", id.ToString()) : id.ToString();
        }
    }
}
