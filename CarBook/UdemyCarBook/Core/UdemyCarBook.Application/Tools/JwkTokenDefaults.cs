using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.Tools
{
    public class JwkTokenDefaults
    {
        public const string ValidAudience = "https://localhost";
        public const string ValidIssuer = "https://localhost";
        public const string Key = "CarBookJwtKey_EnAz32Karakter_Uzun_Guclu_2026!";
        public const int Expire = 5; // token geçerlilik süresi (gün)
    }

}
