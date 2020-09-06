using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanielAPI.Models
{
    public partial class A001Auth
    {
        public int Idusuario { get; set; }
        public string Combinacion { get; set; }
        public string Email { get; set; }
        public bool? Valido { get; set; }
        public string Fecexpira { get; set; }
        public string Horaexpira { get; set; }
    }
}
