using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DanielAPI.Logic;
using DanielAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DanielAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        [EnableCors("AllowOrigin")]
        /// <summary>
        /// Metodo para authenticarse en el server
        /// valida si los datos de usuario y password son correctos
        /// </summary>
        /// <param name="usuario">Soporta todas las propiedades de un usuario</param>
        /// <returns>Devuelve JSON token valido por 5 minutos, si los datos son incorrectos devuelve vacio</returns>
        public string Login([FromBody] A001Usuario usuario)
        {
            try
            {
                string result = String.Empty;
                A001Usuario objValidUser = new AuthLogic().ValidaUsuario(usuario);
                if (objValidUser != null)
                {
                    var claims = new[]
                    {
                        new Claim("UserData", JsonConvert.SerializeObject(new A001Auth()
                        {
                            Idusuario = objValidUser.Id,
                            Combinacion = Encryption.EncryptString(objValidUser.Id + DateTime.Now.ToString("yyyyMMddHHmmtt"), _configuration["EncryptionKey"]),
                            Email = objValidUser.Email,
                            Valido = true
                        }))
                    };

                    var token = new JwtSecurityToken
                    (
                        issuer: _configuration["ApiAuth:Issuer"],
                        audience: _configuration["ApiAuth:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(5),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"])), SecurityAlgorithms.HmacSha256)
                    );

                    result = JsonConvert.SerializeObject(new JwtSecurityTokenHandler().WriteToken(token));
                }

                return result;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        [HttpPost]
        [Route("Refresh")]
        [EnableCors("AllowOrigin")]
        /// <summary>
        /// Metodo verifica el estado del token enviado para luego refrescarlo
        /// y enviar el token actualizado por 5 minutos mas
        /// </summary>
        /// <returns>Devuelve el mismo JSON token con nueva expiracion, si hay error devuelve vacio</returns>
        public string RefreshToken()
        {
            try
            {
                string tokenresult = String.Empty;
                A001Auth objUserdata = JsonConvert.DeserializeObject<A001Auth>(User.Claims.Where(x => x.Type.Equals("UserData")).FirstOrDefault().Value);
                if (new AuthLogic().UsuarioValido(objUserdata))
                {
                    var claims = new[]
                    {
                        new Claim("UserData", JsonConvert.SerializeObject(new A001Auth()
                        {
                            Idusuario = objUserdata.Idusuario,
                            Combinacion = objUserdata.Combinacion,
                            Email = objUserdata.Email
                        }))
                    };

                    var newToken = new JwtSecurityToken
                    (
                        issuer: _configuration["ApiAuth:Issuer"],
                        audience: _configuration["ApiAuth:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(5),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"])), SecurityAlgorithms.HmacSha256)
                    );

                    tokenresult = JsonConvert.SerializeObject(new JwtSecurityTokenHandler().WriteToken(newToken));
                }

                return tokenresult;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
    }
}