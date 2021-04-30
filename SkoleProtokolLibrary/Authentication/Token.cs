using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Linq;

namespace SkoleProtokolLibrary.Authentication
{
    public class Token
    {
        public IEnumerable<Claim> Claims { get; }
        public string UserId { get; set; }

        public Token(IEnumerable<Claim> claims)
        {
            Claims = claims;
            UserId = GetClaimValue("UserId");
        }

        public string GetClaimValue(string claimToGet)
        {
            return Claims.FirstOrDefault(claim => claim.Type.Equals(claimToGet)).Value;
        }
    }
}
