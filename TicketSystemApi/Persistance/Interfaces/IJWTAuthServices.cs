using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TicketSystemApi.Persistance.Interfaces
{
    public interface IJWTAuthServices
    {
        JwtSecurityToken BuildToken(IEnumerable<Claim> claims);
       // ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
