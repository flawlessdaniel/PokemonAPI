using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanielAPI.Models
{
    public partial class A001Usuario
    {
        public A001Usuario()
        {
           
        }

        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
