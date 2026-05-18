using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Dtos;
using UdemyCarBook.Application.Features.Mediator.Results.AppUserResults;

namespace UdemyCarBook.Application.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(GetCheckAppUserQueryResult result)
        {
            var claims = new List<Claim>();

            // Rol bilgisini ekliyoruz
            if (!string.IsNullOrWhiteSpace(result.Role))
                claims.Add(new Claim(ClaimTypes.Role, result.Role));

            // --- KRİTİK DÜZELTME: ID BİLGİSİNİ HER İKİ FORMATTA DA EKLİYORUZ ---
            // 1. Standart NameIdentifier (Senin mevcut yapın)
            claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()));

            // 2. JwtRegisteredClaimNames.NameId (WebUI Controller'ın beklediği format)
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, result.Id.ToString()));

            // 3. Manuel "Id" etiketi (En garanti yol)
            claims.Add(new Claim("Id", result.Id.ToString()));
            // ----------------------------------------------------------------

            if (!string.IsNullOrWhiteSpace(result.UserName))
                claims.Add(new Claim("Username", result.UserName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwkTokenDefaults.Key));

            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(JwkTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwkTokenDefaults.ValidIssuer,
                audience: JwkTokenDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return new TokenResponseDto(tokenHandler.WriteToken(token), expireDate);
        }
    }
}