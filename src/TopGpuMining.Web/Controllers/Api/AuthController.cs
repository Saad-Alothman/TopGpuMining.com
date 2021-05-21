using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TopGpuMining.Web.Controllers.Api
{
    public class AuthController : ApiBaseController
    {
        [Route("get/token")]
        public IActionResult GetToken()
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name,"Neom"),
            };

            var token = GenerateToken(claims);

            return Ok(token);
        }

        [Route("test")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Test()
        {
            return Ok("You are authenitcated");
        }

    }
}