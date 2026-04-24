using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.Dtos
{
    public class TokenResponseDto
    {
        public TokenResponseDto(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }

        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }

}
