using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Entity;
using Api.IService;
using Md.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    /// <summary>
    /// 权限(获取Token)
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class AuthController : ApiController
    {
        private readonly ITokenService _tokenService;
        /// <summary>   
        /// 
        /// </summary>
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public MethodResult GetToken(UserEntity user)
        {
            var token = _tokenService.GetToken(user);
            var response = new
            {
                Status = true,
                Token = token,
                Type = "Bearer"
            };
            return new MethodResult(response);
        }
    }
}