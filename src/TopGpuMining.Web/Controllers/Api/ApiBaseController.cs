using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TopGpuMining.Web.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ApiBaseController : Controller
    {

        protected string GenerateToken(Claim[] claims, DateTime? expireDate = null)
        {
            var keyStr = AuthHelper.Key;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            if (expireDate == null)
                expireDate = DateTime.Now.AddYears(10);


            var jwtToken = new JwtSecurityToken
                (
                    issuer: AuthHelper.Issuer,
                    audience: AuthHelper.Audience,
                    expires: expireDate,
                    claims: claims,
                    signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

    }
}